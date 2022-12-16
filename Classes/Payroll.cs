using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollGroup1.Classes
{
    public class Payroll
    {
        public string month { get; set; }
        public string Name { get; set; }
        public string workType { get; set; }
        public double daysWorked { get; set; }
        public int overTime { get; set; }
        public int lates { get; set; }
        public double totalSalary { get; set; }
        public double deductions { get; set; }
        public double totalPay { get; set; }

        public string ToText()
        {
            return month + "," + Name + "," + workType + "," +
                   daysWorked + "," + overTime + "," + lates + "," + totalSalary+ "," + deductions
                   + "," + totalPay;
        }
        public string GetList()
        {
            return month + Name + workType + daysWorked + overTime + lates + totalSalary + deductions + totalPay;
        }

        
    }
}
