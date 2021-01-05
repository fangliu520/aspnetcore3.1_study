using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.StatePattern
{
  public  class LightGreen : LightBase
    {
        public LightGreen()
        {
            base.Color = LightColor.Green;
        }

        public override void Show()
        {
            Console.WriteLine("绿灯行");
        }

        public override void Turn()
        {
            Color = LightColor.Yellow;
        }       

        public override void TurnContext(Context context)
        {
            context.CurrentLight = new LightYellow();
        }
    }
}
