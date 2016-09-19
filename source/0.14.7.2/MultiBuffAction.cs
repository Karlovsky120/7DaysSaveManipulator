using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class MultiBuffAction
    {
        //num = 3
        public static Value<int> multiBuffActionVersion;

        //Q
        public EnumCommand command;

        //C
        public Value<float> unknownC;

        //G
        public EnumBuffCategoryFlags categoryFlags;

        //V
        public Value<int> unknownV;

        //O
        public Value<string> unknownO;

        //W
        public Value<string> unknownW;

        //E
        public Value<string> context;

        //J
        public BuffTimer unknownJ;

        //S
        public BuffTimer unknownS;

        //F
        public Value<bool> unknownF;

        //I
        public Value<bool> unknownI;

        public enum EnumCommand
        {
            Damage,
            Kill,
            Debuff,
            AddImmunity,
            SetStatValue,
            ResetStat,
            AttachPrefab,
            Increment,
            Min,
            Max,
            SetVar,
            Buff
        }

        public static MultiBuffAction Read(BinaryReader reader)
        {
            multiBuffActionVersion = new Value<int>(reader.ReadInt32());

            MultiBuffAction multiBuffAction = new MultiBuffAction();
            multiBuffAction.command = (EnumCommand)reader.ReadByte();
            multiBuffAction.unknownC = new Value<float>(reader.ReadSingle());
            multiBuffAction.categoryFlags = (EnumBuffCategoryFlags)reader.ReadInt32();
            multiBuffAction.unknownV = new Value<int>(reader.ReadInt32());
            multiBuffAction.unknownO = new Value<string>(reader.ReadString());
            multiBuffAction.unknownW = new Value<string>(reader.ReadString());
            multiBuffAction.context = new Value<string>(reader.ReadString());

            multiBuffAction.unknownJ = BuffTimer.Read(reader);
            multiBuffAction.unknownS = BuffTimer.Read(reader);

            multiBuffAction.unknownF = new Value<bool>(reader.ReadBoolean());
            multiBuffAction.unknownI = new Value<bool>(reader.ReadBoolean());

            return multiBuffAction;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(multiBuffActionVersion.Get());
            writer.Write((byte)command);
            writer.Write(unknownC.Get());
            writer.Write((int)categoryFlags);
            writer.Write(unknownV.Get());
            writer.Write(unknownO.Get());
            writer.Write(unknownW.Get());
            writer.Write(context.Get());

            unknownJ.Write(writer);
            unknownS.Write(writer);

            writer.Write(unknownF.Get());
            writer.Write(unknownI.Get());
        }
    }
}