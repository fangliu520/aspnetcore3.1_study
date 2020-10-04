using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppNetCore.Utility.Validate
{
    public class IPValidateAttribute : BaseValidateAttribute
    {
        private string error;
        public override string ErrorMsg
        {
            get { return error ?? "IP地址格式错误"; }
            set { error = value; }
        }
        public override bool Validate(object obj)
        {
            Regex rx = new Regex(@"^((25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))$");
            return obj != null
                   && rx.IsMatch(obj.ToString()); //匹配ip4地址格式匹配
        }
    }
}
