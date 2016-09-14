using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class MultiBuffPrefabAttachmentDescriptor
    {
        //notSaved = 1
        public static Value<int> multiBuffPrefabAttachmentDescriptorVersion;
        //PrefabName
        public Value<string> prefabName;
        //TransformPath
        public Value<string> transformPath;
        //TTL
        public Value<float> TTL;
        //FirstPerson
        public Value<bool> firstPerson;
        //ThirdPerson
        public Value<bool> thirdPerson;

        public static MultiBuffPrefabAttachmentDescriptor Read(BinaryReader reader)
        {
            multiBuffPrefabAttachmentDescriptorVersion = new Value<int>(reader.ReadInt32());
            return new MultiBuffPrefabAttachmentDescriptor
            {
                prefabName = new Value<string>(reader.ReadString()),
                transformPath = new Value<string>(reader.ReadString()),
                TTL = new Value<float>(reader.ReadSingle()),
                firstPerson = new Value<bool>(reader.ReadBoolean()),
                thirdPerson = new Value<bool>(reader.ReadBoolean())
            };
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(multiBuffPrefabAttachmentDescriptorVersion.get());
            writer.Write(prefabName.get());
            writer.Write(transformPath.get());
            writer.Write(TTL.get());
            writer.Write(firstPerson.get());
            writer.Write(thirdPerson.get());
        }
    }
}
