using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class TileEntityLootContainer
    {
        //version = 3
        public Value<int> tileEntityVersion;

        //localChunkPos
        public Vector3Di localChunkPos;

        //entityId
        public Value<int> entityId;

        //ZY
        public Value<ulong> unknownMT;

        //lootListIndex
        public Value<int> lootListIndex;

        //VU
        public Vector3Di unknownVU;

        //bTouched
        public Value<bool> bTouched;

        //worldTimeTouched
        public Value<ulong> worldTimeTouched;

        //bPlayerBackpack
        public Value<bool> bPlayerBackpack;

        //items
        public ItemStack[] items;

        //bPlayerStorage
        public Value<bool> bPlayerStorage;

        public void Read(BinaryReader reader)
        {
            tileEntityVersion = new Value<int>((int)reader.ReadUInt16());
            localChunkPos = new Vector3Di();
            localChunkPos.x = new Value<int>(reader.ReadInt32());
            localChunkPos.y = new Value<int>(reader.ReadInt32());
            localChunkPos.z = new Value<int>(reader.ReadInt32());
            entityId = new Value<int>(reader.ReadInt32());
            unknownMT = new Value<ulong>(reader.ReadUInt64());

            lootListIndex = new Value<int>((int)reader.ReadUInt16());
            unknownVU = new Vector3Di();
            unknownVU.x = new Value<int>((int)reader.ReadUInt16());
            unknownVU.y = new Value<int>((int)reader.ReadUInt16());
            bTouched = new Value<bool>(reader.ReadBoolean());
            worldTimeTouched = new Value<ulong>((ulong)reader.ReadUInt32());
            bPlayerBackpack = new Value<bool>(reader.ReadBoolean());

            int itemsLength = Math.Min((int)reader.ReadInt16(), unknownVU.x.Get() * unknownVU.y.Get());
            for (int i = 0; i < itemsLength; i++)
            {
                items[i].Read(reader);
            }

            bPlayerStorage = new Value<bool>(reader.ReadBoolean());
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort)tileEntityVersion.Get());
            writer.Write(localChunkPos.x.Get());
            writer.Write(localChunkPos.y.Get());
            writer.Write(localChunkPos.z.Get());
            writer.Write(entityId.Get());
            writer.Write(unknownMT.Get());

            writer.Write((ushort)lootListIndex.Get());
            writer.Write((ushort)unknownVU.x.Get());
            writer.Write((ushort)unknownVU.y.Get());
            writer.Write(bTouched.Get());
            writer.Write((uint)worldTimeTouched.Get());
            writer.Write(bPlayerBackpack.Get());
            writer.Write((short)items.Length);
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Write(writer);
            }
            writer.Write(bPlayerStorage.Get());
        }
    }
}