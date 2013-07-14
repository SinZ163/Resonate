using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            packet.ReadPacket(connection.stream);
            Console.WriteLine("[Login] Received sane Packet 2 Handshake");

            //connection.protocol.send(new Packet253EncryptionRequest("ServerID", 
        }
        public override void Receive(Packet254GetInfo packet)
        {
            packet.ReadPacket(connection.stream);
            Console.WriteLine("[Login] Received Packet254GetInfo");

            int ProtocolVersion = 61;
            String TargetVersion = "1.5.2";
            String MOTD = "§8A §4Custom§8 Minecraft Server";
            int OnlinePlayers = 9001;
            int MaxPlayers = 1337;
            connection.Kick("§1\0" + ProtocolVersion + "\0" + TargetVersion + "\0" + MOTD + "\0" + OnlinePlayers +"\0" + MaxPlayers);
        }
    }
}
