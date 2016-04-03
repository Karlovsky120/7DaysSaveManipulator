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
        public Color hairColor;
        //SkinColor
        public Color skinColor;
        //EyeColor
        public Color eyeColor;
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
            playerProfile.hairColor = new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            playerProfile.skinColor = new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            playerProfile.eyeColor = new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            playerProfile.hairName = new Value<string>(reader.ReadString());
            if (playerProfile.isMale.get())
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
            writer.Write(playerProfileVersion.get());
            writer.Write(archetype);
            writer.Write(isMale.get());

            writer.Write(hairColor.r.get());
            writer.Write(hairColor.g.get());
            writer.Write(hairColor.b.get());
            writer.Write(skinColor.r.get());
            writer.Write(skinColor.g.get());
            writer.Write(skinColor.b.get());
            writer.Write(eyeColor.r.get());
            writer.Write(eyeColor.g.get());
            writer.Write(eyeColor.b.get());

            writer.Write(hairName.get());

            if (isMale.get())
            {
                writer.Write(beardName.get());
            }

            for (int i = 0; i < dnaValues.Length; i++)
            {
                writer.Write(dnaValues[i]);
            }
        }
    }
}
