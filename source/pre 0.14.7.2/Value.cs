using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class Value<T>
    {
        private T value;

        public Value(T value)
        {
            this.value = value;
        }

        public T get()
        {
            return value;
        }

        public void set(T value)
        {
            this.value = value;
        }
    }
}
