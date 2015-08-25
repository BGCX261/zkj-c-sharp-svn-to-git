using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        static Socket serverSocket;
        private static byte[] data;
        static void Main(string[] args)
        {
            
            IPEndPoint ipend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);  //服务器ip和端口号
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //服务器Socket

            serverSocket.Bind(ipend); //绑定ip和端口
            serverSocket.Listen(10); //设定最多10个排队请求

            //处理客户端连接
            Thread thread = new Thread(ListenClientConnect);
            thread.Start();
         }


        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("Ok"));

                IPEndPoint clientip = (IPEndPoint)clientSocket.RemoteEndPoint;
                Console.WriteLine("connect with client:" + clientip.Address + " at port:" + clientip.Port);

                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket); 

            }
               
                
        }
        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param> 
            private static void ReceiveMessage(object clientSocket)
            {
                 while (true)
                {
                    Socket myClientSocket = (Socket)clientSocket;  
                    try 
                    {
                    data = new byte[8];
                    myClientSocket.Receive(data);
                    string recstr = Encoding.ASCII.GetString(data);
                    Console.WriteLine(recstr);

                    myClientSocket.Send(Encoding.ASCII.GetBytes(recstr.ToUpper()));
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        myClientSocket.Shutdown(SocketShutdown.Both);
                        myClientSocket.Close();
                        break;
                    }
                }
            }
        }
}
