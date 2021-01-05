using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CommandPattern
{
    public class WriteCommand : BaseCommand
    {
        //public WriteCommand(Document doc) : base(doc) { }
        public override void Excute()
        {
            this._Receive.Write();
           // Console.WriteLine("Write");
        }
    }
}
