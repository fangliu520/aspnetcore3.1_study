using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.BridgePattern.Bridge
{
    public interface ISystem
    {
        /// <summary>
        /// 操作系统版本
        /// </summary>
        /// <returns></returns>
        string Version();
        /// <summary>
        /// 操作系统
        /// </summary>
        /// <returns></returns>
        string System();

    }
}
