﻿using System;
using System.Collections.Generic;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public abstract class BuffModifier
    {
        //version = 1
        public static Value<int> buffModifierVersion;
        //num
        public static Value<int> buffModifierClassId;
        //C
        public static Dictionary<EnumBuffModifierClassId, Type> dictionary;
        //S
        public EnumBuffModifierClassId enumId;//S
        //Q
        public Value<int> UID;
        //J
        public Buff buff;

        public static BuffModifier Read(BinaryReader reader)
        {
            buffModifierVersion = new Value<int>(reader.ReadInt32());
            buffModifierClassId = new Value<int>((int) reader.ReadByte());

            Type type;
            dictionary.TryGetValue((EnumBuffModifierClassId)buffModifierClassId.get(), out type);

            BuffModifier buffModifier = Activator.CreateInstance(type, null) as BuffModifier;
            buffModifier.enumId = (EnumBuffModifierClassId)buffModifierClassId.get();
            buffModifier.Read(reader, buffModifierVersion.get());
            return buffModifier;
        }

        public virtual void Read(BinaryReader reader, int version)
        {
            UID = new Value<int>(reader.ReadInt32());
        }

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write(buffModifierVersion.get());
            writer.Write((byte)buffModifierClassId.get());
            writer.Write(UID.get());
        }

        public static void RegisterClass(EnumBuffModifierClassId classId, Type type)
        {
            dictionary[classId] = type;
        }

        static BuffModifier()
        {
            dictionary = new Dictionary<EnumBuffModifierClassId, Type>();
            RegisterClass(EnumBuffModifierClassId.BuffModifierSetTickRate, typeof(BuffModifierSetTickRate));
        }
    }
}