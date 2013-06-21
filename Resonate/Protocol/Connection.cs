using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Resonate.Protocol.Packet;

namespace Resonate.Protocol
{
    class Connection
    {
        public TcpClient clientSocket;
        public ProtocolHandler protocolHandler;
        public Protocol protocol;
        public NetworkStream stream;

        //public String name;

        public Connection(TcpClient clientSocket)
        {
            this.clientSocket = clientSocket;
            this.stream = clientSocket.GetStream();
            this.protocolHandler = new LoginHandler(this);
            this.protocol = new Protocol(this);
        }

        public void Kick()
        {
            Kick("You have been kicked.");
        }
        public void Kick(String message)
        {
            protocol.send(new Packet255KickDisconnect(message));
            clientSocket.Close();
        }
    }
}
