﻿using System;
using System.Collections.Generic;

namespace SevenDaysSaveManipulator.GameData
{
    [Serializable]
    public class Value<T>
    {
        private T value;

        [NonSerialized]
        private List<WeakReference<IValueListener<T>>> listeners = new List<WeakReference<IValueListener<T>>>();

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

                foreach (WeakReference<IValueListener<T>> reference in listeners)
                {
                    IValueListener<T> listener;
                    if (reference.TryGetTarget(out listener))
                    {
                        listener.ValueUpdated(this);
                    }
                }
            }
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public void AddListener(IValueListener<T> listener)
        {
            listeners.Add(new WeakReference<IValueListener<T>>(listener));
        }
    }
}