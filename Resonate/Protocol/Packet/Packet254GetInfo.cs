using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Resonate.Protocol.Packet
{
    class Packet254GetInfo : IPacket
    {
        public byte ID
        {
            get { return 254; }
        }

        public byte magic;

        public void ReadPacket(NetworkStream stream)
        {
            magic = (byte)stream.ReadByte();
        }

        public void WritePacket(NetworkStream stream)
        {
            throw new NotImplementedException();
        }

        public void Received(ProtocolHandler handler)
        {
            handler.Receive(this);
        }
    }
}
