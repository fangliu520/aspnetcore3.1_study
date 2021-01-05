using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CommandPattern
{
    public class ReadCommand : BaseCommand
    {
        //public ReadCommand(Document doc) : base(doc) { }
        public override void Excute()
        {
            this._Receive.Read();
            //Console.WriteLine("Read");
        }
    }
}
