using System;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class Vector3Di
    {
        public Value<float> heading;
        public Value<int> x;
        public Value<int> y;
        public Value<int> z;
    }
}