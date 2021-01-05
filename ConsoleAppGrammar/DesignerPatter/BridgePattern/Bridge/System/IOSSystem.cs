using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.BridgePattern.Bridge
{
    public class IOSSystem : ISystem
    {
        public string System()
        {
            return "IOS";
        }

        public string Version()
        {
            return "10.0";
        }
    }
}
