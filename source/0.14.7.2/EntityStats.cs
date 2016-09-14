using System;
using System.Collections.Generic;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class EntityStats
    {
        //num = 6
        public Value<int> statsVersion;
        //L
        public EnumBuffCategoryFlags buffCategoryFlags;
        //D
        public int[] immunity = new int[13];
        //idTable
        public Dictionary<ushort, StatModifier> idTable;
        public Stat health;
        public Stat stamina;
        public Stat sickness;
        public Stat gassiness;
        public Stat speedModifier;
        public Stat wellness;
        public Stat coreTemp;
        public Stat food;
        public Stat water;
        //JJ
        public Value<float> waterLevel;
        //E
        public List<Buff> buffList;
        //W
        public Dictionary<string, MultiBuffVariable> multiBuffVariableDictionary;

        public void Read(BinaryReader reader)
        {
            statsVersion = new Value<int>(reader.ReadInt32());
            buffCategoryFlags = (EnumBuffCategoryFlags)reader.ReadInt32();

            //num2
            int immunityLength = reader.ReadInt32();
            for (int i = 0; i < immunityLength; i++)
            {
                int num = reader.ReadInt32();
                if (i < immunity.Length)
                {
                    immunity[i] = num;
                }
            }

            idTable = new Dictionary<ushort, StatModifier>();
            health = new Stat();
            health.Read(reader, idTable);
            stamina = new Stat();
            stamina.Read(reader, idTable);
            sickness = new Stat();
            sickness.Read(reader, idTable);
            gassiness = new Stat();
            gassiness.Read(reader, idTable);
            speedModifier = new Stat();
            speedModifier.Read(reader, idTable);
            wellness = new Stat();
            wellness.Read(reader, idTable);
            coreTemp = new Stat();
            coreTemp.Read(reader, idTable);
            food = new Stat();
            food.Read(reader, idTable);
            water = new Stat();
            water.Read(reader, idTable);

            waterLevel = new Value<float>(reader.ReadSingle());
            
            //num4
            int buffListCount = reader.ReadInt32();
            buffList = new List<Buff>();
            for (int j = 0; j < buffListCount; j++)
            {
                Buff buff = Buff.Read(reader, idTable);
                buffList.Add(buff);
            }

            //num5
            int multiBuffVariableDictionaryCount = reader.ReadInt32();
            multiBuffVariableDictionary = new Dictionary<string, MultiBuffVariable>();
            for (int k = 0; k < multiBuffVariableDictionaryCount; k++)
            {
                string key = reader.ReadString();
                multiBuffVariableDictionary[key] = MultiBuffVariable.Read(reader);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(statsVersion.Get());
            writer.Write((int) buffCategoryFlags);
            writer.Write(immunity.Length);
            for (int i = 0; i < immunity.Length; i++)
            {
                writer.Write(immunity[i]);
            }
            health.Write(writer);
            stamina.Write(writer);
            sickness.Write(writer);
            gassiness.Write(writer);
            speedModifier.Write(writer);
            wellness.Write(writer);
            coreTemp.Write(writer);
            food.Write(writer);
            water.Write(writer);
            writer.Write(waterLevel.Get());
            writer.Write(buffList.Count);

            List<Buff>.Enumerator enumerator = buffList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumerator.Current.Write(writer);
            }

            writer.Write(multiBuffVariableDictionary.Count);
            foreach (KeyValuePair<string, MultiBuffVariable> current in multiBuffVariableDictionary)
            {
                writer.Write(current.Key);
                current.Value.Write(writer);
            }
        }
    }
}
