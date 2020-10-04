using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppNetCore.Utility.Validate
{
    public class NumberValidateAttribute : BaseValidateAttribute
    {
        private string error;
        public override string ErrorMsg
        {
            get { return error ?? "数值格式错误"; }
            set { error = value; }
        }

        public override bool Validate(object obj)
        {

            Regex rx = new Regex(@"^(0|-?[1-9]\d*)\b");
            return obj != null
                   && rx.IsMatch(obj.ToString()); //匹配 任意整数的验证 正整数 负整数 0
        }
    }
}
