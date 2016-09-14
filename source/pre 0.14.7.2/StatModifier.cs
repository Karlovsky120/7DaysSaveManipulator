using System;
using System.Collections.Generic;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public abstract class StatModifier
    {
        //version = 4
        public static Value<int> statModifierVersion;
        //num 
        public static Value<int> enumStatModifierClassId;
        //G
        public static Dictionary<EnumStatModifierClassId, Type> dictionary;
        //J
        public EnumStatModifierClassId enumId;
        //W
        public Value<int> UID;
        //Q
        public Value<ushort> fileId;
        //S
        public EnumBuffCategoryFlags buffCategoryFlags;
        //E
        public Value<int> stackCount;
        //C
        public BuffTimer buffTimer;
        //O
        public Stat stat;

        public static StatModifier Read(BinaryReader reader)
        {
            statModifierVersion = new Value<int>(reader.ReadInt32());
            enumStatModifierClassId = new Value<int>((int) reader.ReadByte());

            Type type;
            dictionary.TryGetValue((EnumStatModifierClassId)enumStatModifierClassId.get(), out type);

            StatModifier statModifier = Activator.CreateInstance(type, null) as StatModifier;
            statModifier.enumId = (EnumStatModifierClassId)enumStatModifierClassId.get();
            statModifier.Read(reader, statModifierVersion.get());
            return statModifier;
        }

        public virtual void Read(BinaryReader reader, int version)
        {
            UID = new Value<int>(reader.ReadInt32());
            fileId = new Value<ushort>(reader.ReadUInt16());
            buffCategoryFlags = (EnumBuffCategoryFlags)reader.ReadInt32();
            stackCount = new Value<int>(reader.ReadInt32());
            buffTimer = BuffTimer.Read(reader);
        }

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write(statModifierVersion.get());
            writer.Write((byte) enumId);
            writer.Write(UID.get());
            writer.Write(fileId.get());
            writer.Write((int) buffCategoryFlags);
            writer.Write(stackCount.get());
            buffTimer.Write(writer);
        }

        public static void RegisterClass(EnumStatModifierClassId classId, Type type)
        {
            dictionary[classId] = type;
        }

        static StatModifier()
        {
            dictionary = new Dictionary<EnumStatModifierClassId, Type>();
            RegisterClass(EnumStatModifierClassId.StatModifierMax, typeof(StatModifierMax));
            RegisterClass(EnumStatModifierClassId.StatModifierValueOT, typeof(StatModifierValueOT));
            RegisterClass(EnumStatModifierClassId.StatModifierModifyValue, typeof(StatModifierModifyValue));
            RegisterClass(EnumStatModifierClassId.StatModifierSetValue, typeof(StatModifierSetValue));
            RegisterClass(EnumStatModifierClassId.StatModifierMulValue, typeof(StatModifierMulValue));
        }
    }
}
