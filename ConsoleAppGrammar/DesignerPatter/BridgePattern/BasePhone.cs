using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.BridgePattern
{
    public abstract class BasePhone
    {
        public int Price { get; set; }
        /// <summary>
        /// 操作系统版本
        /// </summary>
        /// <returns></returns>
        public abstract string Version();
        /// <summary>
        /// 操作系统
        /// </summary>
        /// <returns></returns>
        public abstract string System();

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
