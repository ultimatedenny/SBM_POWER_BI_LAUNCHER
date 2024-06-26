﻿using System;
using System.Collections.Generic;

namespace SBM_POWER_BI_LAUNCHER.Utilities.DependencyInjection
{
    public class DependencyInjectionContainer
    {
        private readonly Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

        public void Register<T>(T implementation)
        {
            _dependencies[typeof(T)] = implementation;
        }

        public T Resolve<T>()
        {
            return (T)_dependencies[typeof(T)];
        }
    }
}