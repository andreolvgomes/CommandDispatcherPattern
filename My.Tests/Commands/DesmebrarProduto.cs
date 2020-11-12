using System.Collections.Generic;
using MessagerBus;

namespace My.Tests.Commands
{
    public class DesmebrarProduto : ICommand
    {
        public int NrDoc { get; set; }
        public List<string> Produtos { get; set; }
    }
}
