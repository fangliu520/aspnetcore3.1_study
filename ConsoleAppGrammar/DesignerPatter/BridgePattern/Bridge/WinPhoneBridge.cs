﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.BridgePattern.Bridge
{
    public class WinPhoneBridge : BasePhoneBridge
    {
        public override void Call()
        {
            Console.WriteLine($"Use OS {this.GetType().Name}{this.SystemVersion.System()}{this.SystemVersion.Version()} call");
        }

        public override void Text()
        {
            Console.WriteLine($"Use OS {this.GetType().Name}{this.SystemVersion.System()}{this.SystemVersion.Version()} call");
        }
    }
}
