using System;
using System.Collections.Generic;
using System.Text;

namespace MyBus.App
{
    public abstract class IClassWithDisposable: IDisposable
    {
        public void Dispose()
        {

        }
    }

    public class ClassWithDisposable : IClassWithDisposable
    {

    }
}