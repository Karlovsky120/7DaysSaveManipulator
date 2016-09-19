using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class PlayerProfile
    {
        //num = 4
        public static Value<int> playerProfileVersion;

        //Archetype
        public string archetype;

        //IsMale
        public Value<bool> isMale;

        //HairColor
        public Colour hairColor;

        //SkinColor
        public Colour skinColor;

        //EyeColor
        public Colour eyeColor;

        //HairName
        public Value<string> hairName;

        //BeardName
        public Value<string> beardName;

        //Values
        public float[] dnaValues;

        public static PlayerProfile Read(BinaryReader reader)
        {
            PlayerProfile playerProfile = new PlayerProfile();

            playerProfileVersion = new Value<int>(reader.ReadInt32());

            playerProfile.archetype = reader.ReadString();
            playerProfile.isMale = new Value<bool>(reader.ReadBoolean());
            playerProfile.hairColor = new Colour(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            playerProfile.skinColor = new Colour(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            playerProfile.eyeColor = new Colour(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            playerProfile.hairName = new Value<string>(reader.ReadString());
            if (playerProfile.isMale.Get())
            {
                playerProfile.beardName = new Value<string>(reader.ReadString());
            }
            else
            {
                playerProfile.beardName = new Value<string>("");
            }

            float[] array = new float[46];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = reader.ReadSingle();
            }

            playerProfile.dnaValues = array;

            return playerProfile;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(playerProfileVersion.Get());
            writer.Write(archetype);
            writer.Write(isMale.Get());

            writer.Write(hairColor.r.Get());
            writer.Write(hairColor.g.Get());
            writer.Write(hairColor.b.Get());
            writer.Write(skinColor.r.Get());
            writer.Write(skinColor.g.Get());
            writer.Write(skinColor.b.Get());
            writer.Write(eyeColor.r.Get());
            writer.Write(eyeColor.g.Get());
            writer.Write(eyeColor.b.Get());

            writer.Write(hairName.Get());

            if (isMale.Get())
            {
                writer.Write(beardName.Get());
            }

            for (int i = 0; i < dnaValues.Length; i++)
            {
                writer.Write(dnaValues[i]);
            }
        }
    }
}