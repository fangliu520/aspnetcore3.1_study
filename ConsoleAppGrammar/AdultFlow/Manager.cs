using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.AdultFlow
{
    public class Manager : AbstractAdult
    {

        public override void Adult(ApplyContext context)
        {
            Console.WriteLine($"this is {this.GetType().Name}{this.Name}审批");
            if (context.Hour < 32)
            {
                context.AdultResult = true;
                context.AdultRemark = "允许请假";
                Console.WriteLine($" {this.GetType().Name}{this.Name}审批通过");
            }
            else
            {
                base.AdultNext(context);
            }
        }
    }
}
