using System;
using System.Collections.Generic;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class Stat
    {
        //num = 5
        public Value<int> statVersion;
        //J
        public Value<float> value;
        //S
        public Value<float> maxModifier;
        //C
        public Value<float> valueModifier;
        //Q
        public Value<float> baseMax;
        //O
        public Value<float> originalMax;
        //W
        public Value<float> originalValue;
        //G
        public Value<bool> unknownG;
        //V
        public List<StatModifier> statModifierList;

        public void Read(BinaryReader reader, Dictionary<ushort, StatModifier> idTable)
        {
            statVersion = new Value<int>(reader.ReadInt32());
            value = new Value<float>(reader.ReadSingle());
            maxModifier = new Value<float>(reader.ReadSingle());
            valueModifier = new Value<float>(reader.ReadSingle());
            baseMax = new Value<float>(reader.ReadSingle());
            originalMax = new Value<float>(reader.ReadSingle());
            originalValue = new Value<float>(reader.ReadSingle());
            unknownG = new Value<bool>(reader.ReadBoolean());

            //num3
            int statModifierListCount = reader.ReadInt32();
            statModifierList = new List<StatModifier>();
            for (int j = 0; j < statModifierListCount; j++)
            {
                StatModifier statModifier = StatModifier.Read(reader);
                statModifier.stat = this;
                statModifierList.Add(statModifier);
                idTable[statModifier.fileId.get()] = statModifier;
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(statVersion.get());
            writer.Write(value.get());
            writer.Write(maxModifier.get());
            writer.Write(valueModifier.get());
            writer.Write(baseMax.get());
            writer.Write(originalMax.get());
            writer.Write(originalValue.get());
            writer.Write(unknownG.get());
            writer.Write(statModifierList.Count);

            List<StatModifier>.Enumerator enumerator = statModifierList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumerator.Current.Write(writer);
            }
        }
    }
}
