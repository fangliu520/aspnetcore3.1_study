using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.BridgePattern.Bridge
{
    public class AndroidSystem : ISystem
    {
        public string System()
        {
            return "android";
        }

        public string Version()
        {
            return "11.0";
        }
    }
}
