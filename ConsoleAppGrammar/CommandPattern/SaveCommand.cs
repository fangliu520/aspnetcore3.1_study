using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CommandPattern
{
    public class SaveCommand : BaseCommand
    {
        public override void Excute()
        {
            this._Receive.Save();
        }
    }
}
