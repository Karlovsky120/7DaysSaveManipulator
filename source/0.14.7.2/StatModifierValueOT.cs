using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class StatModifierValueOT : StatModifier
    {
        //V
        public Value<float> unknownV;
        //F
        public Value<float> unknownF;
        //I
        public Value<float> frequency;
        //L
        public Value<float> unknownL;

        public override void Read(BinaryReader reader, int version)
        {
            base.Read(reader, version);
            unknownV = new Value<float>(reader.ReadSingle());
            unknownF = new Value<float>(reader.ReadSingle());
            frequency = new Value<float>(reader.ReadSingle());
            unknownL = new Value<float>(reader.ReadSingle());
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(unknownV.Get());
            writer.Write(unknownF.Get());
            writer.Write(frequency.Get());
            writer.Write(unknownL.Get());
        }
    }
}
