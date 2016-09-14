using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class BuffTimerDuration : BuffTimer
    {
        //C
        public Value<float> elapsed;
        //O
        public Value<float> duration;

        public override void Read(BinaryReader reader, int version)
        {
            base.Read(reader, version);
            elapsed = new Value<float>(reader.ReadSingle());
            duration = new Value<float>(reader.ReadSingle());
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(elapsed.Get());
            writer.Write(duration.Get());
        }
    }
}
