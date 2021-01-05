using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.MediatorPattern
{
    public class Students:BaseCharacter
    {
        public override void GetMessage(string message, BaseCharacter characterFrom)
        {
            Console.WriteLine($"{base.Name}学生 get{characterFrom.Name}:{message}");

        }

        public override void SendMessage(string message, BaseCharacter characterTo)
        {
            Console.WriteLine($"{base.Name}学生 to {characterTo.Name}:{message}");
            characterTo.GetMessage(message, this);
        }
    }
}
