using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class PlayerDataFile
    {
        //version = 24
        public Value<uint> saveFileVersion;
        //ecd
        public EntityCreationData ecd;
        //food
        public LiveStats food;
        //drink
        public LiveStats drink;
        //inventory
        public ItemStack[] inventory;
        //selectedInventorySlot
        public Value<int> selectedInventorySlot;
        //bag
        public ItemStack[] bag;
        //alreadyCraftedList
        public HashSet<string> alreadyCraftedList;
        //spawnPoints
        public List<Vector3Di> spawnPoints;
        //selectedSpawnPointKey
        public Value<long> selectedSpawnPointKey;
        //notSaved = true
        public Value<bool> randomBoolean;
        //notSaved = 0
        public Value<short> randomShort;
        //bLoaded
        public Value<bool> bLoaded;
        //lastSpawnPosition
        public Vector3Di lastSpawnPosition;
        //id
        public Value<int> id;
        //droppedBackpackPosition
        public Vector3Di droppedBackpackPosition;
        //playerKills
        public Value<int> playerKills;
        //zombieKills
        public Value<int> zombieKills;
        //deaths
        public Value<int> deaths;
        //score
        public Value<int> score;
        //equipment
        public Equipment equipment;
        //unlockedRecipeList
        public List<string> unlockedRecipeList;
        //notSaved = 1
        public Value<ushort> randomUShort;
        //markerPosition
        public Vector3Di markerPosition;
        //favoriteEquipment
        public Equipment favoriteEquipment;
        //experience
        public Value<uint> experience;
        //level
        public Value<int> level;
        //bCrouchedLocked
        public Value<bool> bCrouchedLocked;
        //craftingData
        public CraftingData craftingData;
        //favoriteRecipeList
        public List<string> favoriteRecipeList;
        //J
        public MemoryStream skillStream;
        //custom, doesn't exist
        public Skills skills;
        //totalItemsCrafted
        public Value<uint> totalItemsCrafted;
        //distanceWalked
        public Value<float> distanceWalked;
        //longestLife
        public Value<float> longestLife;
        //waypoints
        public WaypointCollection waypoints;
        //skillPoints
        public Value<int> skillPoints;
        //questJournal
        public QuestJournal questJournal;
        //deathUpdateTime
        public Value<int> deathUpdateTime;
        //currentLife
        public Value<float> currentLife;
        //bDead
        public Value<bool> bDead;

        public PlayerDataFile Clone()
        {            
            Stream stream = new FileStream("PlayerDataFile", FileMode.Create, FileAccess.Write, FileShare.None);

            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();

            stream = new FileStream("PlayerDataFile", FileMode.Open, FileAccess.Read, FileShare.None);//File.Open("PlayerDataFile", FileMode.Open);          
            PlayerDataFile clone = (PlayerDataFile)formatter.Deserialize(stream);
            stream.Close();

            File.Delete("PlayerDataFile");

            return clone;
        }

        public void Read(string path)
        {
            BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open));

            if (reader.ReadChar() == 't' && reader.ReadChar() == 't' && reader.ReadChar() == 'p' &&
                reader.ReadChar() == '\0')
            {
                saveFileVersion = new Value<uint>((uint) reader.ReadByte());

                ecd = new EntityCreationData();
                ecd.Read(reader);
                food = new LiveStats();
                food.Read(reader);
                drink = new LiveStats();
                drink.Read(reader);

                inventory = ItemStack.ReadItemStack(reader);
                selectedInventorySlot = new Value<int>((int) reader.ReadByte());
                bag = ItemStack.ReadItemStack(reader);

                //num
                int alreadyCraftedListLength = (int) reader.ReadUInt16();
                alreadyCraftedList = new HashSet<string>();
                for (int i = 0; i < alreadyCraftedListLength; i++)
                {
                    alreadyCraftedList.Add(reader.ReadString());
                }

                //b
                byte spawnPointsCount = reader.ReadByte();
                spawnPoints = new List<Vector3Di>();
                for (int j = 0; j < (int) spawnPointsCount; j++)
                {
                    Vector3Di spawnPoint = new Vector3Di();
                    spawnPoint.x = new Value<int>(reader.ReadInt32());
                    spawnPoint.y = new Value<int>(reader.ReadInt32());
                    spawnPoint.z = new Value<int>(reader.ReadInt32());

                    spawnPoints.Add(spawnPoint);
                }

                selectedSpawnPointKey = new Value<long>(reader.ReadInt64());

                randomBoolean = new Value<bool>(reader.ReadBoolean());
                randomShort = new Value<short>(reader.ReadInt16());

                bLoaded = new Value<bool>(reader.ReadBoolean());

                lastSpawnPosition = new Vector3Di();
                lastSpawnPosition.x = new Value<int>(reader.ReadInt32());
                lastSpawnPosition.y = new Value<int>(reader.ReadInt32());
                lastSpawnPosition.z = new Value<int>(reader.ReadInt32());
                lastSpawnPosition.heading = new Value<float>(reader.ReadSingle());

                id = new Value<int>(reader.ReadInt32());

                droppedBackpackPosition = new Vector3Di();
                droppedBackpackPosition.x = new Value<int>(reader.ReadInt32());
                droppedBackpackPosition.y = new Value<int>(reader.ReadInt32());
                droppedBackpackPosition.z = new Value<int>(reader.ReadInt32());

                playerKills = new Value<int>(reader.ReadInt32());
                zombieKills = new Value<int>(reader.ReadInt32());
                deaths = new Value<int>(reader.ReadInt32());
                score = new Value<int>(reader.ReadInt32());
                equipment = Equipment.Read(reader);

                //num
                int recipeCount = (int) reader.ReadUInt16();
                unlockedRecipeList = new List<string>();
                for (int k = 0; k < recipeCount; k++)
                {
                    unlockedRecipeList.Add(reader.ReadString());
                }

                randomUShort = new Value<ushort>(reader.ReadUInt16());

                markerPosition = new Vector3Di();
                markerPosition.x = new Value<int>(reader.ReadInt32());
                markerPosition.y = new Value<int>(reader.ReadInt32());
                markerPosition.z = new Value<int>(reader.ReadInt32());

                favoriteEquipment = Equipment.Read(reader);
                experience = new Value<uint>(reader.ReadUInt32());
                level = new Value<int>(reader.ReadInt32());

                bCrouchedLocked = new Value<bool>(reader.ReadBoolean());
                craftingData = new CraftingData();
                craftingData.Read(reader);

                //num
                int favoriteRecipeListSize = (int) reader.ReadUInt16();
                favoriteRecipeList = new List<string>();
                for (int l = 0; l < favoriteRecipeListSize; l++)
                {
                    favoriteRecipeList.Add(reader.ReadString());
                }

                //num2
                int memoryStreamSize = (int) reader.ReadUInt32();

                skills = new Skills();
                if (memoryStreamSize > 0)
                {
                    skillStream = new MemoryStream(reader.ReadBytes(memoryStreamSize));
                    skills.Read(new BinaryReader(skillStream));
                }

                totalItemsCrafted = new Value<uint>(reader.ReadUInt32());
                distanceWalked = new Value<float>(reader.ReadSingle());
                longestLife = new Value<float>(reader.ReadSingle());

                waypoints = new WaypointCollection();
                waypoints.Read(reader);

                skillPoints = new Value<int>(reader.ReadInt32());

                questJournal = new QuestJournal();
                questJournal.Read(reader);

                deathUpdateTime = new Value<int>(reader.ReadInt32());
                currentLife = new Value<float>(reader.ReadSingle());
                bDead = new Value<bool>(reader.ReadBoolean());

                //irelevant byte
                reader.ReadByte();

                //My own special varible!
                reader.ReadBoolean();

                reader.Close();
            }

            else
            {
                throw new IOException("Save file corrupted!");
            }
        }

        public void Write(string path)
        {
            BinaryWriter writer = new BinaryWriter(new FileStream(path, FileMode.Create));

            writer.Write('t');
            writer.Write('t');
            writer.Write('p');
            writer.Write((byte) 0);
            writer.Write((byte) saveFileVersion.Get());

            ecd.Write(writer);
            food.Write(writer);
            drink.Write(writer);

            ItemStack.WriteItemStack(writer, inventory);
            writer.Write((byte) selectedInventorySlot.Get());
            ItemStack.WriteItemStack(writer, bag);

            writer.Write((ushort) alreadyCraftedList.Count);
            HashSet<string>.Enumerator enumerator = alreadyCraftedList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                writer.Write(enumerator.Current);
            }

            writer.Write((byte) spawnPoints.Count);
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                writer.Write(spawnPoints[i].x.Get());
                writer.Write(spawnPoints[i].y.Get());
                writer.Write(spawnPoints[i].z.Get());
            }

            writer.Write(selectedSpawnPointKey.Get());
            writer.Write(randomBoolean.Get());
            writer.Write(randomShort.Get());
            writer.Write(bLoaded.Get());
            writer.Write((int) lastSpawnPosition.x.Get());
            writer.Write((int) lastSpawnPosition.y.Get());
            writer.Write((int) lastSpawnPosition.z.Get());
            writer.Write(lastSpawnPosition.heading.Get());
            writer.Write(id.Get());
            writer.Write(droppedBackpackPosition.x.Get());
            writer.Write(droppedBackpackPosition.y.Get());
            writer.Write(droppedBackpackPosition.z.Get());

            writer.Write(playerKills.Get());
            writer.Write(zombieKills.Get());
            writer.Write(deaths.Get());
            writer.Write(score.Get());

            equipment.Write(writer);

            writer.Write((ushort) unlockedRecipeList.Count);
            List<string>.Enumerator enumerator2 = unlockedRecipeList.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                writer.Write(enumerator2.Current);
            }

            writer.Write(randomUShort.Get());
            writer.Write(markerPosition.x.Get());
            writer.Write(markerPosition.y.Get());
            writer.Write(markerPosition.z.Get());
            favoriteEquipment.Write(writer);
            writer.Write(experience.Get());
            writer.Write(level.Get());
            writer.Write(bCrouchedLocked.Get());
            craftingData.Write(writer);

            writer.Write((ushort) favoriteRecipeList.Count);
            List<string>.Enumerator enumerator3 = favoriteRecipeList.GetEnumerator();
            while (enumerator3.MoveNext())
            {
                writer.Write(enumerator3.Current);
            }

            skillStream = new MemoryStream();
            skills.Write(new BinaryWriter(skillStream));
            byte[] array = skillStream.ToArray();
            writer.Write((uint) array.Length);
            if (array.Length > 0)
            {
                writer.Write(array);
            }

            writer.Write(totalItemsCrafted.Get());
            writer.Write(distanceWalked.Get());
            writer.Write(longestLife.Get());
            waypoints.Write(writer);
            writer.Write(skillPoints.Get());

            questJournal.Write(writer);
            writer.Write(deathUpdateTime.Get());
            writer.Write(currentLife.Get());
            writer.Write(bDead.Get());

            writer.Write((byte)88);
            writer.Write(true);

            writer.Close();
        }

        public PlayerDataFile(string path)
        {
            Read(path);
        }
    }
}
