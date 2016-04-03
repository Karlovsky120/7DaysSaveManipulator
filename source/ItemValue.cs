using System;
using System.Collections.Generic;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class ItemValue
    {
        //b = 3
        public Value<byte> itemValueVersion;
        //type
        public Value<int> type;
        //UseTimes
        public Value<int> useTimes;
        //Quality
        public Value<int> quality;
        //Meta
        public Value<int> meta;
        //Parts
        public ItemValue[] parts = new ItemValue[0];
        //Attachments
        public List<ItemValue> attachments = new List<ItemValue>();
        //Activated
        public Value<bool> activated;
        //SelectedAmmoTypeIndex
        public Value<byte> selectedAmmoTypeIndex;

        public void Read(BinaryReader reader)
        {
            itemValueVersion = new Value<byte>(reader.ReadByte());
            type = new Value<int>((int) reader.ReadUInt16());
            useTimes = new Value<int>((int) reader.ReadUInt16());
            quality = new Value<int>((int) reader.ReadUInt16());
            meta = new Value<int>((int) reader.ReadInt16());

            //b2
            byte partNumber = reader.ReadByte();
            if (partNumber != 0)
            {
                parts = new ItemValue[4];

                for (int i = 0; i < (int) partNumber; i++)
                {
                    if (reader.ReadBoolean())
                    {
                        parts[i] = new ItemValue();
                        parts[i].Read(reader);
                    }

                    else
                    {
                        parts[i] = null;
                    }
                }
            }

            //notSaved
            bool hasAttachments = reader.ReadBoolean();
            if (hasAttachments)
            {
                //int
                int attachmentsLength = (int) reader.ReadByte();
                for (int j = 0; j < attachmentsLength; j++)
                {
                    attachments.Add(new ItemValue());
                    attachments[j].Read(reader);
                }
            }

            activated = new Value<bool>(reader.ReadBoolean());
            selectedAmmoTypeIndex = new Value<byte>(reader.ReadByte());
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(itemValueVersion.get());
            writer.Write((ushort) type.get());
            writer.Write((ushort) useTimes.get());
            writer.Write((ushort) quality.get());
            writer.Write((ushort) meta.get());
            int num = 0;
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] != null)
                {
                    num = i + 1;
                }
            }
            writer.Write((byte) num);

            for (int j = 0; j < num; j++)
            {
                bool hasPart = parts[j] != null;
                writer.Write(hasPart);

                if (hasPart)
                {
                    parts[j].Write(writer);
                }
            }

            bool hasAttachments = attachments.Count > 0;
            writer.Write(hasAttachments);
            if (hasAttachments)
            { 
                int l = 0;

                for (int k = 0; k < attachments.Count; k++)
                {
                    if (attachments[k].type.get() != 0)
                    {
                        l++;
                    }
                }

                writer.Write((byte)l);

                for (int k = 0; k < attachments.Count; k++)
                {
                    if (attachments[k].type.get() != 0)
                    {
                        attachments[k].Write(writer);
                    }
                }              
            }

            writer.Write(activated.get());
            writer.Write(selectedAmmoTypeIndex.get());
        }
    }
}
