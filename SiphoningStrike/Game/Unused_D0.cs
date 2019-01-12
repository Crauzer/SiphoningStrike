using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using SiphoningStrike.Game.Common;

namespace SiphoningStrike.Game
{
    public sealed class Unused_D0 : GamePacket, IUnusedPacket // 0x0D0
    {
        public override GamePacketID ID => GamePacketID.Unused_D0;
        public Unused_D0() {}
        public Unused_D0(byte[] data)
        {
            var reader = new ByteReader(data);
            
            reader.ReadByte();
            this.SenderNetID = reader.ReadUInt32();

            //Unused

            this.BytesLeft = reader.ReadBytesLeft();
        }
        public override byte[] GetBytes()
        {
            var writer = new ByteWriter();
            
            writer.WriteByte((byte)this.ID);
            writer.WriteUInt32(this.SenderNetID);

            //Unused

            writer.WriteBytes(this.BytesLeft);
            return writer.GetBytes();
        }
    }
}