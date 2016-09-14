using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class Skill
    {
        //num = 2
        public Value<int> skillVersion;
        //id
        public Value<int> id;
        //expToNextLevel
        public Value<int> expToNextLevel;
        //isLocked
        public Value<bool> isLocked;
        //level
        public Value<int> level;
        //parent
        public Skills parent;

        public void Read(BinaryReader reader)
        {
            skillVersion = new Value<int>((int)reader.ReadByte());
            id = new Value<int>(reader.ReadInt32());

            expToNextLevel = new Value<int>(reader.ReadInt32());
            isLocked = new Value<bool>(reader.ReadBoolean());
            level = new Value<int>(reader.ReadInt32());
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((byte)skillVersion.get());
            writer.Write(id.get());
            writer.Write(expToNextLevel.get());
            writer.Write(isLocked.get());
            writer.Write(level.get());
        }
    }
}
