﻿using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class ItemStack
    {
        //itemValue
        public ItemValue itemValue;
        //count
        public Value<int> count;

        public ItemStack Read(BinaryReader reader)
        {
            itemValue= new ItemValue();
            itemValue.Read(reader);
            count = new Value<int>((int) reader.ReadInt16());
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            itemValue.Write(writer);
            writer.Write((short) count.Get());
        }

        public static ItemStack[] ReadItemStack(BinaryReader reader)
        {
            //num
            int itemStackLength = (int) reader.ReadUInt16();
            ItemStack[] array = new ItemStack[itemStackLength];
            for (int i = 0; i < itemStackLength; i++)
            {
                array[i] = new ItemStack();
                array[i].Read(reader);
            }

            return array;
        }

        public static void WriteItemStack(BinaryWriter writer, ItemStack[] itemStacks)
        {
            writer.Write((ushort) itemStacks.Length);
            for (int i = 0; i < itemStacks.Length; i++)
            {
                itemStacks[i].Write(writer);
            }
        }
    }
}