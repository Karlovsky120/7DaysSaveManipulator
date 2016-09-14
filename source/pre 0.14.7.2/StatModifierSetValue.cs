using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class StatModifierSetValue : StatModifier
    {
        //V
        public Value<float> unknownV;
        //F
        public Value<float> unknownF;

        public override void Read(BinaryReader reader, int version)
        {
            base.Read(reader, version);
            unknownV = new Value<float>(reader.ReadSingle());
            unknownF = new Value<float>(reader.ReadSingle());
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(unknownV.get());
            writer.Write(unknownF.get());
        }
    }
}
