using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore3.Models
{
    public class Result
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public string Status { get; set; } = "error";
        /// <summary>
        /// 返回码
        /// </summary>
        public string Code { get; set; } = "";
        /// <summary>
        /// 返回结果
        /// </summary>
        public dynamic Tag { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "";
    }

    public class Status
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const string Ok = "ok";
        /// <summary>
        /// 请求错误
        /// </summary>
        public const string Error = "error";
    }
}
