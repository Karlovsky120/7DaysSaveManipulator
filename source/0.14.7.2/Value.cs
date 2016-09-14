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

        [NonSerialized]
        private List<IValueListener<T>> listeners = new List<IValueListener<T>>();

        public Value(T value)
        {
            this.value = value;
        }

        public T Get()
        {
            return value;
        }

        public T GetRemoveListeners()
        {
            listeners = null;
            return value;
        }

        public void Set(T value)
        {
            if (!this.value.Equals(value))
            {
                this.value = value;

                foreach (IValueListener<T> listener in listeners)
                {
                    listener.ValueUpdated(this);
                }
            }
        }

        public void AddListener(IValueListener<T> listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(IValueListener<T> listener)
        {
            listeners.Remove(listener);
        }
    }
}
