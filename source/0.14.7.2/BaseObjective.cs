using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class BaseObjective
    {
        //CurrentVersion
        public Value<byte> currentVersion;

        //CurrentValue
        public Value<byte> currentValue;

        public virtual void Read(BinaryReader reader)
        {
            currentVersion = new Value<byte>(reader.ReadByte());
            currentValue = new Value<byte>(reader.ReadByte());
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(currentVersion.Get());
            writer.Write(currentValue.Get());
        }
    }
}