﻿using System;
using System.IO;
using System.Text;

namespace SiphoningStrike.Game.Common
{
    public abstract class HeroStat
    {
        internal abstract void Read(ByteReader reader);
        internal abstract void Write(ByteWriter writer);
    }

    public class HeroStatBool : HeroStat
    {
        public bool Value { get; set; }
        internal override void Read(ByteReader reader)
        {
            Value = reader.ReadBool();
        }
        internal override void Write(ByteWriter writer)
        {
            writer.WriteBool(Value);
        }
    }

    public class HeroStatInt32 : HeroStat
    {
        public int Value { get; set; }
        internal override void Read(ByteReader reader)
        {
            Value = reader.ReadInt32();
        }
        internal override void Write(ByteWriter writer)
        {
            writer.WriteInt32(Value);
        }
    }

    public class HeroStatFloat : HeroStat
    {
        public float Value { get; set; }
        internal override void Read(ByteReader reader)
        {
            Value = reader.ReadFloat();
        }
        internal override void Write(ByteWriter writer)
        {
            writer.WriteFloat(Value);
        }
    }

    public class HeroStatInt64 : HeroStat
    {
        public long Value { get; set; }
        internal override void Read(ByteReader reader)
        {
            Value = reader.ReadInt64();
        }
        internal override void Write(ByteWriter writer)
        {
            writer.WriteInt64(Value);
        }
    }


    public class HeroStatString : HeroStat
    {
        public string Value { get; set; }
        internal override void Read(ByteReader reader)
        {
            short size = reader.ReadInt16();
            byte[] data = reader.ReadBytes(size);
            Value = Encoding.UTF8.GetString(data);
        }
        internal override void Write(ByteWriter writer)
        {
            byte[] data = Encoding.UTF8.GetBytes(Value);
            int size = data.Length;
            if (size >= 63)
            {
                throw new IOException("HeroStatString value too big > 63");
            }
            writer.WriteInt16((short)size);
            writer.WriteBytes(data);
        }
    }
}
