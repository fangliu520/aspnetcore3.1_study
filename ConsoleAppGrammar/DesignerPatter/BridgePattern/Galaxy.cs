﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.BridgePattern
{
    public class Galaxy : BasePhone
    {
        public override void Call()
        {
            Console.WriteLine($"Use OS {this.GetType().Name}{this.System()}{this.Version()} call");
        }

        public override string System()
        {
            return "android";
        }

        public override void Text()
        {
            Console.WriteLine($"Use OS {this.GetType().Name}{this.System()}{this.Version()} Text");
        }

        public override string Version()
        {
            return "11.0";
        }
    }
}
