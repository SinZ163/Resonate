using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resonate.Protocol.Packet;

namespace Resonate.Protocol
{
    class LoginHandler : ProtocolHandler
    {
        protected Connection _connection;
        public override Connection connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public LoginHandler(Connection connection)
        {
            this.connection = connection;
        }

        public override void Receive(IPacket packet)
        {
            connection.Kick("You must be logged in to use that packet.");
            Console.WriteLine("[Login] unhandled: " + packet.ID);
        }
        public override void Receive(Packet002Handshake packet)
        {
            Console.WriteLine("[Login] Received sane Packet 2 Handshake");
            packet.ReadPacket(connection.stream);
        }
        public override void Receive(Packet254GetInfo packet)
        {
            int ProtocolVersion = 61;
            String TargetVersion = "1.5.2";
            String MOTD = "A Custom Minecraft Server";
            int OnlinePlayers = 1337;
            int MaxPlayers = 9001;
            Console.WriteLine("[Login] Received Packet254GetInfo");
            packet.ReadPacket(connection.stream);
            connection.Kick("§1\0" + ProtocolVersion + "\0" + TargetVersion + "\0" + MOTD + "\0" + OnlinePlayers +"\0" + MaxPlayers);
        }
    }
}
