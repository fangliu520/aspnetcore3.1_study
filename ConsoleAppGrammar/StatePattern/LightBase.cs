using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.StatePattern
{
    public abstract class LightBase
    {
        public LightColor Color { get; set; }

        public abstract void Show();


        public abstract void Turn();

        public abstract void TurnContext(Context context);

    }
}
