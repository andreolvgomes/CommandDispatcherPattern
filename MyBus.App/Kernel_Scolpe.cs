using System;
using System.Collections.Generic;
using System.Text;
using Ninject;
using Ninject.Activation.Blocks;

namespace MyBus.App
{
    public class Kernel_Scolpe
    {
        public static void Test()
        {
            //https://csharp.hotexamples.com/pt/examples/Ninject/StandardKernel/Dispose/php-standardkernel-dispose-method-examples.html
            var kernel = new StandardKernel();

            kernel.Bind<IConnection>().To<Connection>().InTransientScope();
            kernel.Bind<ControllerClientes>().To<ControllerClientes>();
            kernel.Bind<ControllerProdutos>().To<ControllerProdutos>();

            var a = kernel.Get<ControllerClientes>();
            var b = kernel.Get<ControllerProdutos>();

            var c = kernel.Get<ControllerClientes>();
            var d = kernel.Get<ControllerProdutos>();

            using (IActivationBlock scolpe = kernel.BeginBlock())
            {
                var controllerClientes = scolpe.Get<ControllerClientes>();
                var controllerProdutos = scolpe.Get<ControllerProdutos>();

                var controllerClientes2 = scolpe.Get<ControllerClientes>();
                var controllerProdutos2 = scolpe.Get<ControllerProdutos>();
            }
        }
    }

    public class ControllerClientes
    {
        public ControllerClientes(IConnection connection)
        {
            var x = connection;
        }
    }

    public class ControllerProdutos
    {
        public ControllerProdutos(IConnection connection)
        {
            var x = connection;
        }
    }

    public class Connection : IConnection
    {
        public override string ToString()
        {
            return Id.ToString();
        }

        public Guid Id { get; set; }

        public Connection()
        {
            Id = Guid.NewGuid();
        }

        public void Execute(string sql) { }
        public string Query(string sql) { return "Query"; }
        public void Dispose() { }
    }

    public interface IConnection : IDisposable
    {
        Guid Id { get; set; }
        string Query(string sql);
        void Execute(string sql);
    }
}