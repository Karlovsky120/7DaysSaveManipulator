using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class EntityCreationData
    {
        //readFileVersion = 22
        public Value<byte> entityCreationDataVersion;
        //entityClass
        public Value<int> entityClass;
        //id
        public Value<int> id;
        //lifetime
        public Value<float> lifetime;
        //pos
        public Vector3Df pos;
        //rot
        public Vector3Df rot;
        //onGround
        public Value<bool> onGround;
        //bodyDamage
        public BodyDamage bodyDamage;
        //stats
        public EntityStats stats;
        //deathTime
        public Value<int> deathTime;
        //type = 5
        public Value<int> type;
        //lootContainer
        public TileEntityLootContainer lootContainer;
        //Q
        public Vector3Di homePosition;
        //D = -1
        public Value<int> unknownD;
        //P
        public EnumSpawnerSource spawnerSource;
        //belongsPlayerId
        public Value<int> belongsPlayerId;
        //itemStack
        public ItemStack itemStack;
        //holdingItem
        public ItemValue holdingItem;
        //teamNumber
        public Value<int> teamNumber;
        //entityName
        public Value<string> entityName;
        //skinTexture
        public Value<string> skinTexture;
        //playerProfile
        public PlayerProfile playerProfile;
        //entityData
        public MemoryStream entityData;

        public void Read(BinaryReader reader)
        {
            entityCreationDataVersion = new Value<byte>(reader.ReadByte());
            entityClass = new Value<int>(reader.ReadInt32());

            id = new Value<int>(reader.ReadInt32());
            lifetime = new Value<float>(reader.ReadSingle());

            pos = new Vector3Df();
            pos.x = new Value<float>(reader.ReadSingle());
            pos.y = new Value<float>(reader.ReadSingle());
            pos.z = new Value<float>(reader.ReadSingle());

            rot = new Vector3Df();
            rot.x = new Value<float>(reader.ReadSingle());
            rot.y = new Value<float>(reader.ReadSingle());
            rot.z = new Value<float>(reader.ReadSingle());

            onGround = new Value<bool>(reader.ReadBoolean());

            bodyDamage = new BodyDamage();
            bodyDamage.Read(reader);

            bool isStatsNotNull = reader.ReadBoolean();
            
            stats = new EntityStats();
            stats.Read(reader);           

            deathTime = new Value<int>((int) reader.ReadInt16());

            //MAY BE USELESS
            bool tileEntityLootContainerNotNull = reader.ReadBoolean();
            if (tileEntityLootContainerNotNull)
            {
                type =  new Value<int>(reader.ReadInt32());
                lootContainer = new TileEntityLootContainer();
                lootContainer.Read(reader);
            }
            //END USELESS

            homePosition = new Vector3Di();
            homePosition.x = new Value<int>(reader.ReadInt32());
            homePosition.y = new Value<int>(reader.ReadInt32());
            homePosition.z = new Value<int>(reader.ReadInt32());

            unknownD = new Value<int>((int) reader.ReadInt16());
            spawnerSource = (EnumSpawnerSource) reader.ReadByte();

            holdingItem = new ItemValue();
            holdingItem.Read(reader);
            teamNumber = new Value<int>((int) reader.ReadByte());
            entityName = new Value<string>(reader.ReadString());
            skinTexture = new Value<string>(reader.ReadString());

            bool isPlayerProfileNotNull = reader.ReadBoolean();
            if (isPlayerProfileNotNull)
            {
                playerProfile = PlayerProfile.Read(reader);
            }

            else
            {
                playerProfile = null;
            }

            //num2
            int entityDataLength = (int) reader.ReadUInt16();
            if (entityDataLength > 0)
            {
                byte[] buffer = reader.ReadBytes(entityDataLength);
                entityData = new MemoryStream(buffer);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(entityCreationDataVersion.get()); 
            writer.Write(entityClass.get());
            writer.Write(id.get());
            writer.Write(lifetime.get());
            writer.Write(pos.x.get());

            writer.Write(pos.y.get());
            writer.Write(pos.z.get());
            writer.Write(rot.x.get());
            writer.Write(rot.y.get());
            writer.Write(rot.z.get());
            writer.Write(onGround.get());

            bodyDamage.Write(writer);
            writer.Write(stats != null);

            stats.Write(writer);

            writer.Write((short) deathTime.get());
            writer.Write(lootContainer != null);
            if (lootContainer != null)
            {
                writer.Write(5);
                lootContainer.Write(writer);
            }

            writer.Write(homePosition.x.get());
            writer.Write(homePosition.y.get());
            writer.Write(homePosition.z.get());
            writer.Write((short) unknownD.get());
            writer.Write((byte) spawnerSource);

            holdingItem.Write(writer);
            writer.Write((byte) teamNumber.get());
            writer.Write(entityName.get());
            writer.Write(skinTexture.get());
            writer.Write(playerProfile != null);
            if (playerProfile != null)
            {
                playerProfile.Write(writer);
            }

            int num = (int) entityData.Length;
            writer.Write((ushort) num);
            if (num > 0)
            {
                writer.Write(entityData.ToArray());
            }
        }
    }
}
