using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CommandPattern
{
   public class Invoker
    {
        BaseCommand _baseCommand = null;//还可以换成命令的集合

        public Invoker(BaseCommand baseCommand)
        {
            _baseCommand = baseCommand;
        }
        public void Excute()
        {
            _baseCommand.Excute();
        }
    }
}
