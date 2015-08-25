using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;

namespace MyTcpListener
{
    class Program
    {

        static IPAddress ip = IPAddress.Any;
        static TcpListener listener = null;

        static byte[] bytes = new byte[256];
        static String data = null;

        static BinaryReader reader = null;
        static BinaryWriter writer = null;
        static NetworkStream stream = null;

        static TcpClient client = null;
        static void Main(string[] args)
        {
            listener = new TcpListener(ip, 9000);

            listener.Start();

            //处理客户端连接
            Thread thread = new Thread(ListenClientConnect);
            thread.Start();


        }

        private static void ListenClientConnect()
        {
            while (true)
            {
                //开始接受客户端连接
              
                    client = listener.AcceptTcpClient();
                    IPEndPoint clientip = (IPEndPoint)client.Client.RemoteEndPoint;

                    Console.WriteLine("Connected: " + clientip.Address.ToString() + ":" + clientip.Port.ToString());

                    stream = client.GetStream();
                    writer = new BinaryWriter(stream, Encoding.ASCII);
                    reader = new BinaryReader(stream, Encoding.ASCII);

                    writer.Write("Welcome!");

                    Thread thread = new Thread(communicate);
                    thread.Start();
            }
        }

        private static void communicate()
        {

            
            while (true)
            {
                try
                {
                    data = reader.ReadString();
                    Console.WriteLine(data);
                    writer.Write(data.ToUpper());
                }
                catch(Exception e )
                {
                    Console.WriteLine(e.Message);
                    writer.Close();
                    reader.Close();
                    break;
                }
            }
        }


    }
}
