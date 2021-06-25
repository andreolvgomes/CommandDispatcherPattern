using System;
using System.Collections.Generic;
using CommandDispatcher;
using Domain.Messenger;
using MyBus.App.Functions;
using Ninject;
using Ninject.Activation.Blocks;

namespace MyBus.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                IoCKernel.Ins.Init();

                var dispatcher = IoCKernel.Get<IDispatcher>();
                dispatcher.Function(new ReturnResultDefaultFunction(1, 2));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}