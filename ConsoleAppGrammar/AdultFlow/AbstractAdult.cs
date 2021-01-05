using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.AdultFlow
{
    public abstract class AbstractAdult
    {
        public string Name { get; set; }
        public abstract void Adult(ApplyContext context);

        private AbstractAdult _abstractAdult = null;
        public void SetNext(AbstractAdult abstractAdult)
        {
            _abstractAdult = abstractAdult;
        }

        public void AdultNext(ApplyContext context)
        {
            _abstractAdult?.Adult(context);
        }
    }
}
