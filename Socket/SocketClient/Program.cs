using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
#if true
            StartSocketClient();
#else
            StartTcpClient();
#endif
        }

        static void StartSocketClient()
        {
            // connect to server
            var socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 50001));
            Console.WriteLine("connect to server: 127.0.0.1:50001");
             
            // receive msg from server
            Task.Run(() => { Receive(socketClient); });

            // send msg to server
            string msg = "hello world";
            byte[] bytes = Encoding.UTF8.GetBytes(msg);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"to server : {msg}");
                socketClient.Send(bytes);
                Thread.Sleep(5000);
            }

            // close
            socketClient.Shutdown(SocketShutdown.Both);
            socketClient.Close();
            Console.WriteLine("client closed");

            Console.ReadKey();
        }

        static void Receive(Socket socket)
        {
            if (!socket.Connected)
                return;

            while (true)
            {
                byte[] data = new byte[1024 * 1024];
                int readLen = socket.Receive(data, 0, data.Length, SocketFlags.None);
                if (readLen == 0)
                {
                    Console.WriteLine("server disconneted");
                    break;
                }

                var msg = Encoding.UTF8.GetString(data, 0, readLen);
                Console.WriteLine($"from server : {msg}");
            }
        }

        #region TcpClient

        static void StartTcpClient()
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 9999);
            Console.WriteLine("connect to server: 127.0.0.1:9999");

            NetworkStream networkStream = tcpClient.GetStream();
            string msg = "hello world";
            byte[] bytes = Encoding.UTF8.GetBytes(msg);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(msg);
                networkStream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(2000);
            }

            networkStream.Close();
            tcpClient.Close();
            Console.WriteLine("TcpClient closed");

            Console.ReadKey();
        }

        #endregion
    }
}
