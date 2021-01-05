using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CompsitePattern
{
    /// <summary>
    /// 机构/个人
    /// </summary>
    public class Domain : AbstractDomain
    {
        private List<Domain> DomainChildList = new List<Domain>();

        public void AddChild(Domain domainChild)
        {
            DomainChildList.Add(domainChild);
        }
        public override void Commision(double total)
        {
            double result = total * Percent * 0.01;
            Console.WriteLine($"this{Name}提成{result}");
            foreach (var domainChild in DomainChildList)
            {
                domainChild.Commision(result);
            }
        }
    }
}
