using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resonate.Protocol.Packet
{
    class Packet002Handshake : IPacket
    {
        public byte ID
        {
            get { return 2; }
        }
        public byte protocolVersion;
        public string username;
        public string serverHost;
        public int port;

        public void ReadPacket(System.Net.Sockets.NetworkStream stream)
        {
            protocolVersion = (byte)stream.ReadByte();
            username = PacketReader.ReadString(stream);
            serverHost = PacketReader.ReadString(stream);
            port = PacketReader.ReadInt(stream);
        }

        public void WritePacket(System.Net.Sockets.NetworkStream stream)
        {
            throw new NotImplementedException();
        }


        public void Received(ProtocolHandler handler)
        {
            handler.Receive(this);
        }
    }
}
