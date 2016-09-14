using System;
using System.Collections.Generic;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public abstract class BuffTimer
    {
        //version = 2
        public static Value<int> buffTimerVersion;
        //num
        public static Value<int> buffTimerClassId;
        //S
        public static Dictionary<EnumBuffTimerClassId, Type> dictionary;
        //Q
        public EnumBuffTimerClassId classId;
        //Null
        public static BuffTimerNull Null;

        public static BuffTimer Read(BinaryReader reader)
        {
            buffTimerVersion = new Value<int>(reader.ReadInt32());
            buffTimerClassId = new Value<int>((int) reader.ReadByte());

            Type type;
            dictionary.TryGetValue((EnumBuffTimerClassId) buffTimerClassId.Get(), out type);

            BuffTimer buffTimer = Activator.CreateInstance(type, null) as BuffTimer;
            buffTimer.classId = (EnumBuffTimerClassId) buffTimerClassId.Get();
            buffTimer.Read(reader, buffTimerVersion.Get());
            return buffTimer;
        }

        public virtual void Read(BinaryReader reader, int version)
        {         
        }

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write(buffTimerVersion.Get());
            writer.Write((byte) classId);
        }

        public static void RegisterClass(EnumBuffTimerClassId classId, Type type)
        {
            dictionary[classId] = type;
        }

        static BuffTimer()
        {
            Null = new BuffTimerNull();
            dictionary = new Dictionary<EnumBuffTimerClassId, Type>();
            RegisterClass(EnumBuffTimerClassId.Null, typeof(BuffTimerNull));
            RegisterClass(EnumBuffTimerClassId.Duration, typeof(BuffTimerDuration));
            RegisterClass(EnumBuffTimerClassId.Scheduled, typeof(BuffTimerScheduled));
        }

        public BuffTimer(EnumBuffTimerClassId classId)
        {
            this.classId = classId;
        }

        public BuffTimer()
        {
        }
    }
}
