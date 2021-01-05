using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.StatePattern
{
    public class LightRed : LightBase
    {
        public LightRed()
        {
            base.Color = LightColor.Red;
        }

        public override void Show()
        {
            Console.WriteLine("红灯停");
        }

        public override void Turn()
        {
            Color = LightColor.Green;
        }

        public override void TurnContext(Context context)
        {
            context.CurrentLight = new LightGreen();
        }
    }
}
