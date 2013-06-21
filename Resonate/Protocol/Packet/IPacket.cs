using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Resonate.Protocol.Packet
{
    interface IPacket
    {
        byte ID { get; }
        void ReadPacket(NetworkStream stream);
        void WritePacket(NetworkStream stream);
        void Received(ProtocolHandler handler);

    }
}
