using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.StatePattern
{
   public class Context
    {
        public LightBase CurrentLight { get; set; }

        public void Show()
        {
            CurrentLight.Show(); 
        }
        public void Turn()
        {
            CurrentLight.TurnContext(this);
        }
    }
}
