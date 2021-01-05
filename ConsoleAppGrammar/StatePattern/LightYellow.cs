using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.StatePattern
{
    public class LightYellow: LightBase
    {
        public LightYellow()
        {
            base.Color = LightColor.Yellow;
        }
        public override void Show()
        {
            Console.WriteLine("黄灯等一等");
        }

        public override void Turn()
        {
            Color = LightColor.Red;
        }

        public override void TurnContext(Context context)
        {
            context.CurrentLight = new LightRed();
        }
    }
}
