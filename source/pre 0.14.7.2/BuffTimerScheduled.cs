using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class BuffTimerScheduled : BuffTimer
    {
        //num = 3
        public Value<int> buffTimerScheduledVersion;
        //G
        public Value<ulong> unknownG;
        //O
        public Value<int> duration;
        //E
        public Value<float> unknownE;
        //W
        public Value<int> elapsed;

        public override void Read(BinaryReader reader, int buffVersion)
        {
            base.Read(reader, buffVersion);
            buffTimerScheduledVersion = new Value<int>(reader.ReadInt32());

            unknownG = new Value<ulong>(reader.ReadUInt64());
            duration = new Value<int>(reader.ReadInt32());
            unknownE = new Value<float>(reader.ReadSingle());
            elapsed = new Value<int>(reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(buffTimerScheduledVersion.get());
            writer.Write(unknownG.get());
            writer.Write(duration.get());
            writer.Write(unknownE.get());
            writer.Write(elapsed.get());
        }
    }
}
