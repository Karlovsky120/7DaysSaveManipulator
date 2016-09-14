using System;
using System.Collections.Generic;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class Recipe
    {
        //Q = 2
        public Value<byte> recipeVersion;
        //GetName()
        public Value<string> itemName;
        //count
        public Value<int> count;
        //scrapable
        public Value<bool> scrapable;
        //wildcardForgeCategory
        public Value<bool> wildcardForgeCategory;
        //wildcardCampfireCategory
        public Value<bool> wildcardCampfireCategory;
        //text
        public Value<string> craftingToolTypeName;
        //craftingTime
        public Value<float> craftingTime;
        //craftingArea
        public Value<string> craftingArea;
        //tooltip
        public Value<string> tooltip;
        //ingredients, might be obsolete
        public List<ItemStack> ingredients;
        //custom, replaces ingredients
        public List<Tuple<string, int>> nameStackSizeList;
        //materialBasedRecipe
        public Value<bool> materialBasedRecipe;
        //craftingExpGain
        public Value<int> craftingExpGain; 

        public void Read(BinaryReader reader)
        {
            recipeVersion = new Value<byte>(reader.ReadByte());

            itemName = new Value<string>(reader.ReadString()); 
            count = new Value<int>(reader.ReadInt32());
            scrapable = new Value<bool>(reader.ReadBoolean());
            wildcardForgeCategory = new Value<bool>(reader.ReadBoolean());
            wildcardCampfireCategory = new Value<bool>(reader.ReadBoolean());

            craftingToolTypeName = new Value<string>(reader.ReadString());
            craftingTime = new Value<float>(reader.ReadSingle());
            craftingArea = new Value<string>(reader.ReadString());
            tooltip = new Value<string>(reader.ReadString());

            //num
            int ingredientCount = reader.ReadInt32();
            ingredients = new List<ItemStack>();
            nameStackSizeList = new List<Tuple<string, int>>();
            for (int i = 0; i < ingredientCount; i++)
            {
                string name = reader.ReadString();
                int stackSize = reader.ReadInt32();

                int zero1 = reader.ReadInt32();//0
                int zero2 = reader.ReadInt32();//0

                nameStackSizeList.Add(new Tuple<string, int>(name, stackSize));
            }

            materialBasedRecipe = new Value<bool>(reader.ReadBoolean());
            craftingExpGain = new Value<int>(reader.ReadInt32());
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(recipeVersion.get());
            writer.Write(itemName.get());
            writer.Write(count.get());
            writer.Write(scrapable.get());
            writer.Write(wildcardForgeCategory.get());
            writer.Write(wildcardCampfireCategory.get());

            writer.Write(craftingToolTypeName.get());
            writer.Write(craftingTime.get());
            writer.Write(craftingArea.get());
            writer.Write(tooltip.get());

            writer.Write(nameStackSizeList.Count);
            foreach (Tuple<string, int> stack in nameStackSizeList)
            {
                writer.Write(stack.Item1);
                writer.Write(stack.Item2);

                writer.Write(0);
                writer.Write(0);
            }

            writer.Write(materialBasedRecipe.get());
            writer.Write(craftingExpGain.get());
        }
    }
}
