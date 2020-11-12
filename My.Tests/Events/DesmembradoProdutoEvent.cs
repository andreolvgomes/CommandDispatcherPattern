using System;
using System.Collections.Generic;
using System.Text;
using MessagerBus;

namespace My.Tests.Events
{
    public class DesmembradoProdutoEvent : IEvent
    {
        public int NrDoc { get; set; }
        public List<string> Produtos { get; set; }
    }
}