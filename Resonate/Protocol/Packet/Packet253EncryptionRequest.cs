using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resonate.Protocol.Packet
{
    class Packet253EncryptionRequest : IPacket
    {
        public byte ID
        {
            get { return 253; }
        }
        public String serverID;
        public byte[] publicKey;
        public byte[] verifyToken;

        public Packet253EncryptionRequest()
        {
        }
        public Packet253EncryptionRequest(String serverID,byte[] publicKey, byte[] verifyToken)
        {
            this.serverID = serverID;
            this.publicKey = publicKey;
            this.verifyToken = verifyToken;
        }
        public void ReadPacket(System.Net.Sockets.NetworkStream stream)
        {
            Console.WriteLine("[Packet253] DAFUQ.");
        }

        public void WritePacket(System.Net.Sockets.NetworkStream stream)
        {
            stream.WriteByte(ID);
            PacketReader.WriteString(stream, serverID); //TODO: Use random ones

            PacketReader.WriteShort(stream, (short)publicKey.Length);
            PacketReader.WriteByteArray(stream, publicKey);

            PacketReader.WriteShort(stream, (short)verifyToken.Length);
            PacketReader.WriteByteArray(stream, verifyToken);
        }

        public void Received(ProtocolHandler handler)
        {
            handler.Receive(this);
        }
    }
}
