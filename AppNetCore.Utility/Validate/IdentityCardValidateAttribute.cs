using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppNetCore.Utility.Validate
{
    public class IdentityCardValidateAttribute : BaseValidateAttribute
    {

        private string error;
        public override string ErrorMsg
        {
            get { return error ?? "身份证格式错误"; }
            set { error = value; }
        }
        public override bool Validate(object obj)
        {
            //18位身份证号码验证
            Regex rx = new Regex(@"\b\d{6}(19|20)\d{2}(0[1-9]|1[012])(0[1-9]|[12]\d|3[01])\d{3}(\d|X)\b");

            //15位身份证号码验证
            Regex rx15 = new Regex(@"\b\d{8}(0[1-9]|1[012])(0[1-9]|[12]\d|3[01])\d{3}\b");
            return obj != null
                   && (rx.IsMatch(obj.ToString())|| rx15.IsMatch(obj.ToString())); //匹配
        }
    }
}
