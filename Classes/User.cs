using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollGroup1.Classes
{
    public class User
    {
        public string userID { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string Address { get; set; }
        public string userType { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string GetID()
        {
            Random rnd = new Random();
            string[] nums = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            int index1 = rnd.Next(0, 9);
            int index2 = rnd.Next(0, 9);
            int index3 = rnd.Next(0, 9);
            int index4 = rnd.Next(0, 9);
            int index5 = rnd.Next(0, 9);
            int index6 = rnd.Next(0, 9);

            string ID = nums[index1] + nums[index2] + nums[index3] + nums[index4] + nums[index5] + nums[index6];

            return ID;
        }
        public string GettxtFile()
        {
            string txt = userID + "," + lastName + "," + firstName + "," + Address + "," + userType + "," + email + "," + password;
            return txt;
        }
    }
}
