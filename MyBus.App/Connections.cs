using System;
using System.Collections.Generic;
using System.Text;

namespace MyBus.App
{
    public class ConnectionRemoto : IConnectionRemoto
    {
        public ConnectionRemoto(string connectionString) : base(connectionString) { }
    }

    public class ConnectionLocal : IConnectionLocal
    {
        public ConnectionLocal(string connectionString) : base(connectionString) { }
    }

    public abstract class IConnectionRemoto : IConnectionMaster
    {
        public IConnectionRemoto(string connectionString) : base(connectionString) { }
    }

    public abstract class IConnectionLocal : IConnectionMaster
    {

        public IConnectionLocal(string connectionString) : base(connectionString) { }
    }

    public abstract class IConnectionMaster
    {
        public override string ToString()
        {
            return connectionString;
        }

        public string connectionString;

        public IConnectionMaster(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Query()
        {
        }

        public void Execute()
        {
        }
    }
}
