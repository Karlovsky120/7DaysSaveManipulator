using System;
using System.Collections.Generic;

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
