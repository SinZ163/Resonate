using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resonate.Protocol.Packet;

namespace Resonate.Protocol
{
    class Protocol
    {
        private Connection connection;
        public LinkedList<IPacket> out_queue = new LinkedList<IPacket>();

        public Protocol(Connection connection)
        {
            // TODO: Complete member initialization
            this.connection = connection;
        }

        public void send(IPacket packet)
        {
            out_queue.AddLast(packet);
        }
    }
}
