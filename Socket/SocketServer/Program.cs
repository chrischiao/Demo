using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    class Program
    {
        static IList<Socket> _connectedClients = new List<Socket>();

        static void Main(string[] args)
        {
#if true
            StartSocketServer();
#else
            StartTcpListener();
#endif
        }

        static void StartSocketServer()
        {
            Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 50001));
            socketServer.Listen(10);

            // wait for client
            Task.Run(() => { Accept(socketServer); });

            // broadcast to connected clients
            while (true)
            {
                string msg = Console.ReadLine();
                foreach (var client in _connectedClients)
                    client.Send(Encoding.UTF8.GetBytes(msg));
            }
        }

        static void Accept(Socket socket)
        {
            Console.WriteLine("socket server started");
            while (true)
            {
                Socket newSocket = socket.Accept();
                Console.WriteLine($"{newSocket.RemoteEndPoint} connected");
                _connectedClients.Add(newSocket);

                // receive msg from client
                Task.Run(() => { Receive(newSocket); });
            }
        }

        static void Receive(Socket socket)
        {
            while (true)
            {
                byte[] data = new byte[1024 * 1024];
                int readLen = socket.Receive(data, 0, data.Length, SocketFlags.None);
                if (readLen == 0)
                {
                    Console.WriteLine($"{socket.RemoteEndPoint} disconneted");
                    _connectedClients.Remove(socket);
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    break;
                }

                var msg = Encoding.UTF8.GetString(data, 0, readLen);
                Console.WriteLine($"{socket.RemoteEndPoint} : {msg}");
            }
        }

        #region TcpListener

        static void StartTcpListener()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            tcpListener.Start(10);
            Task.Run(() => { Accept(tcpListener); });

            Console.ReadKey();
        }

        static void Accept(TcpListener tcpListener)
        {
            Console.WriteLine("tcp server started");
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine($"{tcpClient.Client.RemoteEndPoint} connected");

                NetworkStream networkStream = tcpClient.GetStream();
                Task.Run(() => { Read(networkStream, tcpClient); });
            }
        }

        static void Read(NetworkStream networkStream, TcpClient tcpClient)
        {
            while (true)
            {
                byte[] buffer = new byte[1024 * 1024];
                var readLen = networkStream.Read(buffer, 0, buffer.Length);
                if (readLen == 0)
                {
                    Console.WriteLine($"{tcpClient.Client.RemoteEndPoint} disconneted");
                    networkStream.Close();
                    tcpClient.Close();
                    break;
                }
                var msg = Encoding.UTF8.GetString(buffer, 0, readLen);
                Console.WriteLine($"{tcpClient.Client.RemoteEndPoint} : {msg}");
            }
        }

        #endregion
    }
}
