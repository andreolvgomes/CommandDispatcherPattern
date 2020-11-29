﻿using System;

namespace CommandDispatcher
{
    public interface IServiceContainer
    {
        object GetInstance(Type serviceType, params object[] params_constructor);
        //TService GetInstance<TService>(params object[] params_constructor) where TService : class;
    }
}