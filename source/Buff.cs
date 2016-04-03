﻿using System;
using System.Collections.Generic;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public abstract class Buff
    {
        //version = 6
        public static Value<int> buffVersion;
        //num
        public static Value<int> buffClassId;
        //D
        public static Dictionary<EnumBuffClassId, Type> dictionary;
        //E
        public BuffTimer timer;
        //C
        public BuffDescriptor descriptor;
        //G
        public Value<bool> isOverriden;
        //O
        public List<StatModifier> statModifierList;
        //W
        public List<BuffModifier> buffModifierList;
        //RandomLetter
        public Value<int> instigatorId;


        public static Buff Read(BinaryReader reader, Dictionary<ushort, StatModifier> idTable)
        {
            buffVersion = new Value<int>((int) reader.ReadUInt16());
            buffClassId = new Value<int>((int) reader.ReadByte());

            Type type;
            dictionary.TryGetValue((EnumBuffClassId) buffClassId.get(), out type);

            Buff buff = Activator.CreateInstance(type, null) as Buff;
            buff.Read(reader, buffVersion.get(), idTable);
            return buff;
        }

        public virtual void Read(BinaryReader reader, int buffVersion, Dictionary<ushort, StatModifier> idTable)
        {
            timer = BuffTimer.Read(reader);
            descriptor = BuffDescriptor.Read(reader);
            isOverriden = new Value<bool>(reader.ReadBoolean());

            int statModifierListCount = reader.ReadByte();
            statModifierList = new List<StatModifier>();
            for (int i = 0; i < statModifierListCount; i++)
            {
                ushort key = reader.ReadUInt16();
                StatModifier statModifier = idTable[key];
                statModifierList.Add(statModifier);
            }

            int buffModiferListCount = reader.ReadByte();
            buffModifierList = new List<BuffModifier>();
            for (int j = 0; j < buffModiferListCount; j++)
            {
                BuffModifier buffModifier = BuffModifier.Read(reader);
                buffModifier.buff = this;
                buffModifierList.Add(buffModifier);
            }

            instigatorId = new Value<int>(reader.ReadInt32());
        }

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write((ushort) buffVersion.get());
            writer.Write((byte) buffClassId.get());
            timer.Write(writer);
            descriptor.Write(writer);
            writer.Write(isOverriden.get());

            writer.Write((byte)statModifierList.Count);
            List<StatModifier>.Enumerator enumerator = statModifierList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                writer.Write(enumerator.Current.fileId.get());
            }

            writer.Write((byte)buffModifierList.Count);
            foreach (BuffModifier modifier in buffModifierList)
            {
                modifier.Write(writer);
            }

            writer.Write(instigatorId.get());
        }

        public static void RegisterClass(EnumBuffClassId classId, Type type)
        {
            dictionary[classId] = type;
        }

        static Buff()
        {
            dictionary = new Dictionary<EnumBuffClassId, Type>();
            RegisterClass(EnumBuffClassId.MultiBuff, typeof (MultiBuff));
        }
    }
}
