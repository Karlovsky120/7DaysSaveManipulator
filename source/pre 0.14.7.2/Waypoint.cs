using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class Waypoint
    {
        //pos
        public Vector3Di pos;
        //icon
        public Value<string> icon;
        //name
        public Value<string> name;
        //bTracked
        public Value<bool> bTracked;

        public void Read(BinaryReader reader)
        {
            pos = new Vector3Di();
            pos.x = new Value<int>(reader.ReadInt32());
            pos.y = new Value<int>(reader.ReadInt32());
            pos.z = new Value<int>(reader.ReadInt32());

            icon = new Value<string>(reader.ReadString());
            name = new Value<string>(reader.ReadString());
            bTracked = new Value<bool>(reader.ReadBoolean());
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(pos.x.get());
            writer.Write(pos.y.get());
            writer.Write(pos.z.get());

            writer.Write(icon.get());
            writer.Write(name.get());
            writer.Write(bTracked.get());
        }
    }
}
