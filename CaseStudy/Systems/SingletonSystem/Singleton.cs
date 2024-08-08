﻿using System;

namespace Case.Systems.SingletonSystem
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        public static T Instance => instance.Value;
    }
}
