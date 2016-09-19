using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class LiveStats
    {
        //S
        public Value<int> lifeLevel;

        //F
        public Value<int> unknownW;

        //P
        public Value<float> saturationLevel;

        //L
        public Value<float> exhaustionLevel;

        public void Read(BinaryReader reader)
        {
            lifeLevel = new Value<int>((int)reader.ReadInt16());
            unknownW = new Value<int>((int)reader.ReadInt16());
            saturationLevel = new Value<float>(reader.ReadSingle());
            exhaustionLevel = new Value<float>(reader.ReadSingle());
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort)lifeLevel.Get());
            writer.Write((ushort)unknownW.Get());
            writer.Write(saturationLevel.Get());
            writer.Write(exhaustionLevel.Get());
        }
    }
}