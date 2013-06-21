using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Resonate.Protocol;
using Resonate.Protocol.Packet;

namespace Resonate
{
    class Server
    {
        public String address;
        public int port;
        public TcpListener server;
        public Thread networkThread;

        List<Connection> users = new List<Connection>();

        public Server(string address, int port)
        {
            this.address = address;
            this.port = port;

            server = new TcpListener(IPAddress.Parse(address), port);
            server.Start();
            server.BeginAcceptTcpClient(OnClientConnect,null);

            networkThread = new Thread(NetworkWorker);
            networkThread.Start();
        }

        private void OnClientConnect(IAsyncResult async)
        {
            try
            {
                TcpClient clientSocket = server.EndAcceptTcpClient(async);
                Connection newClient = new Connection(clientSocket);
                users.Add(newClient);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unknown Exception in Server.OnClientConnect");
                Console.WriteLine(e.StackTrace);
            }
            server.BeginAcceptTcpClient(OnClientConnect, null);
        }
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 25565);
        }

        private void NetworkWorker(object obj)
        {
            while (true)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    Connection conn = users[i];
                    bool disconnect = false;
                    while (conn.protocol.out_queue.Count != 0)
                    {
                        conn.protocol.out_queue.First.Value.WritePacket(conn.stream);
                        disconnect = true;
                        break;
                    }
                    if (disconnect)
                    {
                        users.Remove(conn);
                        continue;
                    }
                    else
                    {
                        DateTime readTimeout = DateTime.Now.AddMilliseconds(10);
                        while (conn.clientSocket.GetStream().DataAvailable && DateTime.Now < readTimeout)
                        {
                            var packet = PacketReader.ReadPacket(conn.stream);
                            packet.Received(conn.protocolHandler);
                        }
                    }
                }
            }
        }
    }
}