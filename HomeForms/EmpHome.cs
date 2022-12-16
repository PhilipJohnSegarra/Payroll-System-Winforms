using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PayrollGroup1.MainForms
{
    public partial class EmpHome : UserControl
    {
        HomeForms.EmpForm emp;
        string CompName, Address, email;
        string EmpName, EmpAddress, EmpEmail;

        public EmpHome()
        {
            InitializeComponent();
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            PayrollDatabase db = new PayrollDatabase();
            string query = "SELECT * FROM Payroll WHERE EmpName=@empname AND Month=@month";
            
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                db.myConnection.Open();
                cmd.Parameters.AddWithValue("@empname", lblEmpName.Text);
                cmd.Parameters.AddWithValue("@month", cbMonth.Text);
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                lblWorkType.Text = $"{reader["WorkType"]}";
                txtMonth.Text = $"{reader["Month"]}";
                txtWorktype.Text = lblWorkType.Text;
                if (lblWorkType.Text == "Full-time")
                {
                    txtRate.Text = "Php 150/hr";
                }
                else
                {
                    txtRate.Text = "Php 80/hr";
                }
                txtDays.Text = $"{reader["DaysWorked"]}";
                int lates = Convert.ToInt32(reader["Lates"]);
                double penalty = lates * 50;
                txtLatePenalty.Text = penalty.ToString();
                txtOvertime.Text = $"{reader["Overtime"]}";
                txtInitialSalary.Text = $"{reader["InitialPay"]}";
                txtDeductions.Text = $"{reader["Deductions"]}";
                txtTotalSalary.Text = $"{reader["TotalPay"]}";

                reader.Close();
            }
            catch
            {
                MessageBox.Show("An error occurred while loading your Payslip\nPlease talk to your HR for assistance");
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using(var bm = new Bitmap(panel1.Width, panel1.Height))
                {
                    panel1.DrawToBitmap(bm, new Rectangle(0, 0, bm.Width, bm.Height));
                    bm.Save(@"Z:\PanelImage.png");
                }

                MessageBox.Show("Payslip is saved");
            }
            catch(Exception a)
            {
                MessageBox.Show(a.Message);
            }
            
        }

        //GET COMPANY DETAILS
        public void DisplayCompInfo()
        {
            Database db = new Database();
            string query = "SELECT CompName, Address, email FROM CompData";
            
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                db.myConnection.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                CompName = $"{reader["CompName"]}";
                Address = $"{reader["Address"]}";
                email = $"{reader["email"]}";

                reader.Close();
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
        }
        //GET EMPLOYEE DETAILS
        public void GetEmp()
        {
            emp = (HomeForms.EmpForm)this.FindForm();
            Database db = new Database();
            try
            {
                string query = "SELECT lastName, fisrtName, address, email FROM UserData WHERE email=@email";
                db.myConnection.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
                cmd.Parameters.AddWithValue("@email", emp.loggedEmail);
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                EmpName = $"{reader["lastName"]}, {reader["fisrtName"]}";
                EmpAddress = $"{reader["address"]}";
                EmpEmail = $"{reader["email"]}";

                reader.Close();
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                

                GC.Collect();
            }
        }
        //GET PAY DETAILS
        public void GetPay()
        {
            PayrollDatabase db = new PayrollDatabase();
            string query = "SELECT * FROM Payroll WHERE EmpName=@empname AND Month=@month";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                cmd.Parameters.AddWithValue("@empname", lblEmpName.Text);
                cmd.Parameters.AddWithValue("@month", cbMonth.Text);
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                lblWorkType.Text = $"{reader["WorkType"]}";
                txtMonth.Text = $"{reader["Month"]}";
                txtWorktype.Text = lblWorkType.Text;
                if(lblWorkType.Text == "Full-time")
                {
                    txtRate.Text = "Php 150/hr";
                }
                else
                {
                    txtRate.Text = "Php 80/hr";
                }
                txtDays.Text = $"{reader["DaysWorked"]}";
                int lates = Convert.ToInt32(reader["Lates"]);
                double penalty = lates * 50;
                txtLatePenalty.Text = penalty.ToString();
                txtOvertime.Text = $"{reader["Overtime"]}";
                txtInitialSalary.Text = $"{reader["InitialPay"]}";
                txtDeductions.Text = $"{reader["Deductions"]}";
                txtTotalSalary.Text = $"{reader["TotalPay"]}";

                reader.Close();
            }
            catch
            {
                MessageBox.Show("An error occurred while laoding your Payslip\nPlease talk to your HR for assistance");
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
        }

        private void EmpHome_Load(object sender, EventArgs e)
        {
            //GET ALL DETAILS
            DisplayCompInfo();
            GetEmp();
            
            //SHOW COMPANY DETAILS
            lblCompName.Text = CompName;
            lblCompAddress.Text = Address;
            lblCompEmail.Text = email;
            //SHOW EMPLOYEE DETAILS
            lblEmpName.Text = EmpName;
            lblEmpAddress.Text = EmpAddress;
            lblEmpEmail.Text = EmpEmail;
            GetPay();
        }
    }
}
