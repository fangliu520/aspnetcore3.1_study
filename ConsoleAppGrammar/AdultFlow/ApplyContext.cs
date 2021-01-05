using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.AdultFlow
{
    /// <summary>
    /// 请假审批
    /// context 保存业务处理的中参数-中间结果-最终结果
    /// 行为型设计模式的标配
    /// </summary>
    public class ApplyContext
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 请假时长
        /// </summary>
        public int Hour { get; set; }

        public string Description { get; set; }

        public bool AdultResult { get; set; }

        public string AdultRemark { get; set; }
    }
}
