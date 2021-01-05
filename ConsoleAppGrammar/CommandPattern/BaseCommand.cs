using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CommandPattern
{
    public abstract class BaseCommand
    {
        public Document _Document
        {
            get;
            private set;
        }
        public void Set(Document doc)
        {
            _Document = doc;
        }

        public IReceive _Receive
        {
            get;
            private set;
        }
        public void SetReceive(IReceive rec)
        {
            _Receive = rec;
        }
        //public BaseCommand(Document document)
        //{
        //    this._Document = document;
        //}
        public abstract void Excute();
    }
}
