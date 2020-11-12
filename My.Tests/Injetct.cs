using System;
using System.Collections.Generic;
using System.Text;
using MessagerBus;
using SimpleInjector;

namespace MyBus.Tests
{
    public class Injetct
    {
        public static void Insert(Container _container)
        {
            _container.Register(typeof(ICommandHandler<>), new[] { typeof(ICommandHandler<>).Assembly });
            _container.Register(typeof(ICommandHandler<,>), new[] { typeof(ICommandHandler<,>).Assembly });
        }
    }
}
