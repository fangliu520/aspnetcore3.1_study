using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.MediatorPattern
{
    public abstract class BaseCharacter
    {
        public string Name { get; set; }

        public  abstract void SendMessage(string message,BaseCharacter characterTo);
        public abstract void GetMessage(string message, BaseCharacter characterFrom);

    }
}
