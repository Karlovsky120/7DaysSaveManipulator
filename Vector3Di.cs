using System;
using System.IO;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class Vector3Di
    {
        public Value<int> x;
        public Value<int> y;
        public Value<int> z;
        public Value<float> heading;
    }   

}
