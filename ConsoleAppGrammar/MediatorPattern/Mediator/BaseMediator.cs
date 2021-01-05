using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.MediatorPattern.Mediator
{
    public class BaseMediator
    {
        public string Name { get; set; }

        private List<BaseCharacter> _baseCharacters = new List<BaseCharacter>();

        public void Add(BaseCharacter baseCharacter)
        {
            _baseCharacters.Add(baseCharacter);
        }

        public  void SendMessage(string message, BaseCharacter characterFrom)
        {
            Console.WriteLine($"{characterFrom.Name} send:{message}");
            foreach (var item in _baseCharacters)
            {
                item.GetMessage(message, characterFrom);
            }
        }
    }
}
