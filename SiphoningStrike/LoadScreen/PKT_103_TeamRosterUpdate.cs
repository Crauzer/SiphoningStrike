﻿using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;

namespace SiphoningStrike.LoadScreen
{
    public class TeamRosterUpdate : LoadScreenPacket
    {
        private ulong[] _orderPlayerIDs = new ulong[24];
        private ulong[] _chaosPlayerIDs = new ulong[24];

        public override LoadScreenPacketID ID => LoadScreenPacketID.TeamRosterUpdate;
        public uint TeamSizeOrder { get; set; }
        public uint TeamSizeChaos { get; set; }
        public ulong[] OrderPlayerIDs => _orderPlayerIDs;
        public ulong[] ChaosPlayerIDs => _chaosPlayerIDs;
        public uint CurrentTeamSizeOrder { get; set; }
        public uint CurrentTeamSizeChaos { get; set; }

        public TeamRosterUpdate() { }

        public TeamRosterUpdate(byte[] data)
        {
            var reader = new ByteReader(data);

            reader.ReadByte();
            reader.ReadPad(3);

            this.TeamSizeOrder = reader.ReadUInt32();
            this.TeamSizeChaos = reader.ReadUInt32();
            for (var i = 0; i < OrderPlayerIDs.Length; i++)
            {
                this.OrderPlayerIDs[i] = reader.ReadUInt64();
            }
            for (var i = 0; i < ChaosPlayerIDs.Length; i++)
            {
                this.ChaosPlayerIDs[i] = reader.ReadUInt64();
            }
            this.CurrentTeamSizeOrder = reader.ReadUInt32();
            this.CurrentTeamSizeChaos = reader.ReadUInt32();

            this.BytesLeft = reader.ReadBytesLeft();
        }

        public override byte[] GetBytes()
        {
            var writer = new ByteWriter();

            writer.WriteByte((byte)this.ID);
            writer.WritePad(3);

            writer.WriteUInt32(this.TeamSizeOrder);
            writer.WriteUInt32(this.TeamSizeChaos);
            for (var i = 0; i < OrderPlayerIDs.Length; i++)
            {
                writer.WriteUInt64(this.OrderPlayerIDs[i]);
            }
            for (var i = 0; i < ChaosPlayerIDs.Length; i++)
            {
                writer.WriteUInt64(this.ChaosPlayerIDs[i]);
            }
            writer.WriteUInt32(this.CurrentTeamSizeOrder);
            writer.WriteUInt32(this.CurrentTeamSizeChaos);

            writer.WriteBytes(this.BytesLeft);
            return writer.GetBytes();
        }
    }
}