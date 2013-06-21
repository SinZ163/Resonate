using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Resonate.Protocol.Packet;

namespace Resonate.Protocol
{
    abstract class ProtocolHandler
    {
        public abstract Connection connection { get; set;}

        public abstract void Receive(IPacket packet);

        public virtual void Receive(Packet002Handshake packet)
        {
            Receive((IPacket)packet);
        }
        public virtual void Receive(Packet254GetInfo packet)
        {
            Receive((IPacket)packet);
        }
        public virtual void Receive(Packet255KickDisconnect packet)
        {
            Receive((IPacket)packet);
        }
    }
}
