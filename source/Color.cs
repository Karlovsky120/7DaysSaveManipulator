using System;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class Color
    {
        public Value<float> r;
        public Value<float> g;
        public Value<float> b;
        public Value<float> a;

        public Color(float red, float green, float blue)
        {
            r = new Value<float>(red);
            g = new Value<float>(green);
            b = new Value<float>(blue);
            a = new Value<float>(1f);
        }
    }
}
