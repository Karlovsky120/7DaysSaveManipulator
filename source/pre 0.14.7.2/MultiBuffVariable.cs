using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class MultiBuffVariable
    {
        //notSaved = 1
        public static Value<int> multiBuffVariableVersion;
        //Q
        public Value<float> unknownQ;
        //J
        public Value<float> unknownJ;
        //S
        public Value<float> unknownS;
        //C
        public Value<float> unknownC;

        public static MultiBuffVariable Read(BinaryReader reader)
        {
            multiBuffVariableVersion = new Value<int>(reader.ReadInt32());
            return new MultiBuffVariable()
            {
                unknownQ = new Value<float>(reader.ReadSingle()),
                unknownJ = new Value<float>(reader.ReadSingle()),
                unknownS = new Value<float>(reader.ReadSingle()),
                unknownC = new Value<float>(reader.ReadSingle())
            };
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(multiBuffVariableVersion.get());
            writer.Write(unknownQ.get());
            writer.Write(unknownJ.get());
            writer.Write(unknownS.get());
            writer.Write(unknownC.get());
        }
    }
}
