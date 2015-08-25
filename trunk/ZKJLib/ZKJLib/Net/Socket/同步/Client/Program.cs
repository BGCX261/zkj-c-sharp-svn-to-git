using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class Program
    {
        private static byte[] data;
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
            try 
            {
                clientSocket.Connect(ipend);
                Console.WriteLine("连接服务器成功");  
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！"  );
                return;
            }

            //通过clientSocket接收数据  
            data = new byte[8];  
            int receiveLength = clientSocket.Receive(data);
            Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(data, 0, receiveLength));

            while (true)
            {
                try
                {
                    data = new byte[8];  
                    string input = Console.ReadLine();
                    clientSocket.Send(Encoding.ASCII.GetBytes(input));
                    Thread.Sleep(100);
                    clientSocket.Receive(data);
                    Console.WriteLine(Encoding.ASCII.GetString(data));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
        }
    }
}
