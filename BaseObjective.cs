using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            writer.Write(currentVersion.get());
            writer.Write(currentValue.get());
        }
    }
}
