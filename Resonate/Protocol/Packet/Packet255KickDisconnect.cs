using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Resonate.Protocol.Packet
{
    class Packet255KickDisconnect : IPacket
    {
        public byte ID
        {
            get { return 255; }
        }
        public string message;

        public Packet255KickDisconnect(string message)
        {
            this.message = message;
        }

        public Packet255KickDisconnect(){}


        public void ReadPacket(NetworkStream stream)
        {
            throw new NotImplementedException();
        }

        public void WritePacket(NetworkStream stream)
        {
            stream.WriteByte(255);
            PacketReader.WriteString(stream, message);
        }
        public void Received(ProtocolHandler handler)
        {
            handler.Receive(this);
        }
    }
}
