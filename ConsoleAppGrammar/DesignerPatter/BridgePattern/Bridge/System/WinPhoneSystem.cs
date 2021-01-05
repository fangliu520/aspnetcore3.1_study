using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.BridgePattern.Bridge.System
{
    public class WinPhoneSystem : ISystem
    {
        public string System()
        {
            return "winSystem";
        }

        public string Version()
        {
            return "5.0";
        }
    }
}
