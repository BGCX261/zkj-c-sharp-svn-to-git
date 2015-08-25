using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpClient client = new TcpClient();
            string data = null;

            NetworkStream ns = null;

            try
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), 9000);

                ns = client.GetStream();
                BinaryReader reader = new BinaryReader(ns, Encoding.ASCII);
                BinaryWriter writer = new BinaryWriter(ns, Encoding.ASCII);

                Console.WriteLine(reader.ReadString());

                while (true)
                {
                    try
                    {
                        string msg = Console.ReadLine();
                        writer.Write(msg);
                        data = reader.ReadString();
                        Console.WriteLine(data);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        writer.Close();
                        reader.Close();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            ns.Close();
            client.Close();
        }
    }
}
