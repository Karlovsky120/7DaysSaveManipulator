using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class BodyDamage
    {
        //num = 2
        public Value<int> bodyDamageVersion;

        public Value<int> Head;
        public Value<int> Chest;

        public Value<int> LeftUpperArm;
        public Value<int> LeftLowerArm;
        public Value<int> RightUpperArm;
        public Value<int> RightLowerArm;

        public Value<int> LeftUpperLeg;
        public Value<int> LeftLowerLeg;
        public Value<int> RightUpperLeg;
        public Value<int> RightLowerLeg;

        public Value<bool> DismemberedHead;

        public Value<bool> DismemberedLeftUpperArm;
        public Value<bool> DismemberedLeftLowerArm;
        public Value<bool> DismemberedRightUpperArm;
        public Value<bool> DismemberedRightLowerArm;

        public Value<bool> DismemberedLeftUpperLeg;
        public Value<bool> DismemberedLeftLowerLeg;
        public Value<bool> DismemberedRightUpperLeg;
        public Value<bool> DismemberedRightLowerLeg;

        public Value<bool> CrippledLeftLeg;
        public Value<bool> CrippledRightLeg;

        public void Read(BinaryReader reader)
        {
            bodyDamageVersion = new Value<int>(reader.ReadInt32());

            LeftUpperLeg = new Value<int>((int)reader.ReadInt16());
            RightUpperLeg = new Value<int>((int)reader.ReadInt16());
            LeftUpperArm = new Value<int>((int)reader.ReadInt16());
            RightUpperArm = new Value<int>((int)reader.ReadInt16());
            Chest = new Value<int>((int)reader.ReadInt16());
            Head = new Value<int>((int)reader.ReadInt16());
            DismemberedLeftUpperArm = new Value<bool>(reader.ReadBoolean());
            DismemberedRightUpperArm = new Value<bool>(reader.ReadBoolean());
            DismemberedHead = new Value<bool>(reader.ReadBoolean());
            DismemberedRightUpperLeg = new Value<bool>(reader.ReadBoolean());
            CrippledRightLeg = new Value<bool>(reader.ReadBoolean());

            LeftLowerLeg = new Value<int>((int)reader.ReadInt16());
            RightLowerLeg = new Value<int>((int)reader.ReadInt16());
            LeftLowerArm = new Value<int>((int)reader.ReadInt16());
            RightLowerArm = new Value<int>((int)reader.ReadInt16());
            DismemberedLeftLowerArm = new Value<bool>(reader.ReadBoolean());
            DismemberedRightLowerArm = new Value<bool>(reader.ReadBoolean());
            DismemberedLeftLowerLeg = new Value<bool>(reader.ReadBoolean());
            DismemberedRightLowerLeg = new Value<bool>(reader.ReadBoolean());

            DismemberedLeftUpperLeg = new Value<bool>(reader.ReadBoolean());
            CrippledLeftLeg = new Value<bool>(reader.ReadBoolean());
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(bodyDamageVersion.get());
            writer.Write((short)LeftUpperLeg.get());
            writer.Write((short)RightUpperLeg.get());
            writer.Write((short)LeftUpperArm.get());
            writer.Write((short)RightUpperArm.get());

            writer.Write((short)Chest.get());
            writer.Write((short)Head.get());
            writer.Write(DismemberedLeftUpperArm.get());
            writer.Write(DismemberedRightUpperArm.get());
            writer.Write(DismemberedHead.get());
            writer.Write(DismemberedRightUpperLeg.get());

            writer.Write(CrippledRightLeg.get());
            writer.Write((short)LeftLowerLeg.get());
            writer.Write((short)RightLowerLeg.get());
            writer.Write((short)LeftLowerArm.get());
            writer.Write((short)RightLowerArm.get());
            writer.Write(DismemberedLeftLowerArm.get());
            writer.Write(DismemberedRightLowerArm.get());
            writer.Write(DismemberedLeftLowerLeg.get());
            writer.Write(DismemberedRightLowerLeg.get());
            writer.Write(DismemberedLeftUpperLeg.get());
            writer.Write(CrippledLeftLeg.get());
        }
    }
}
