using System;
using System.Collections.Generic;
using CommandDispatcher;
using My.Tests.Events;
using My.Tests.Queries;
using MyBus.Tests.Commands;
using Ninject;
using Ninject.Activation.Blocks;

namespace MyBus.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IoCKernel.Ins.Init();
        }
    }
}