using ConsoleAppGrammar.DesignerPatter.BridgePattern.Bridge;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.BridgePattern
{
    public abstract class BasePhoneBridge
    {
        public int Price { get; set; }
        public ISystem SystemVersion { get; set; }
      
        /// <summary>
        /// 打电话
        /// </summary>
        public abstract void Call();
        /// <summary>
        /// 发短信
        /// </summary>
        public abstract void Text();
    }
}
