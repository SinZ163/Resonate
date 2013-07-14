using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Resonate.Protocol.Packet;

namespace Resonate.Protocol
{
    class PacketReader
    {
        public static Dictionary<int, IPacket> Packets = new Dictionary<int, IPacket>
        {
            {2, new Packet002Handshake()},
            {254, new Packet254GetInfo()},
            {255, new Packet255KickDisconnect()}
        };
        public static IPacket ReadPacket(NetworkStream stream)
        {
            int packetID = stream.ReadByte();
            Console.WriteLine("Received Packet: "+packetID);
            Console.WriteLine(Packets[packetID].GetType().ToString());
            return Packets[packetID];
        }
        #region WriteShort
        public static void WriteUShort(NetworkStream stream, ushort value)
        {
            stream.Write(new[]
            {
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 2);
        }
        public static void WriteShort(NetworkStream stream,short value)
        {
            WriteUShort(stream,(ushort)value);
        }
        #endregion
        #region ReadShort
        public static ushort ReadUShort(NetworkStream stream)
        {
            return (ushort)(
                (stream.ReadByte() << 8) |
                stream.ReadByte());
        }
        public static short ReadShort(NetworkStream stream)
        {
            return (short)ReadUShort(stream);
        }
        #endregion

        #region ReadInt
        public static uint ReadUInt(NetworkStream stream)
        {
            return (uint)(
                (stream.ReadByte() << 24) |
                (stream.ReadByte() << 16) |
                (stream.ReadByte() << 8) |
                 stream.ReadByte());
        }
        public static int ReadInt(NetworkStream stream)
        {
            return (int)ReadUInt(stream);
        }
        #endregion
        #region WriteInt
        public static void WriteUInt32(NetworkStream stream, uint value)
        {
            stream.Write(new[]
            {
                (byte)((value & 0xFF000000) >> 24),
                (byte)((value & 0xFF0000) >> 16),
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 4);
        }

        public static void WriteInt32(NetworkStream stream, int value)
        {
            WriteUInt32(stream, (uint)value);
        }
        #endregion


        #region ByteArray
        public static byte[] ReadByteArray(NetworkStream stream, int length)
        {
            var result = new byte[length];
            if (length == 0) return result;
            stream.Read(result, 0, length);
            return result;
        }
        public static void WriteByteArray(NetworkStream stream, byte[] value)
        {
            stream.Write(value, 0, value.Length);
        }
        #endregion

        #region String
        public static string ReadString(NetworkStream stream)
        {
            ushort length = ReadUShort(stream);
            var data = ReadByteArray(stream,length * 2);
            return Encoding.BigEndianUnicode.GetString(data);
        }

        public static void WriteString(NetworkStream stream,string value)
        {
            WriteUShort(stream,(ushort)value.Length);
            WriteByteArray(stream,Encoding.BigEndianUnicode.GetBytes(value));
        }
        #endregion
    }
}
