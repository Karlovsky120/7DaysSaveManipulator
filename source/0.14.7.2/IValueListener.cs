using SevenDaysSaveManipulator.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDaysSaveManipulator
{
    public interface IValueListener<T>
    {
        void ValueUpdated(Value<T> source);
    }
}
