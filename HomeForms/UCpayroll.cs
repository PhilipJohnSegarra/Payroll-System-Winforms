using PayrollGroup1.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PayrollGroup1.MainForms
{
    public partial class UCpayroll : UserControl
    {
        //List Declaration
        public List<Classes.Payroll> payrollList = new List<Classes.Payroll>();
        //string emptxtFile = @"";
        double totalDeductions = 0;
        double TotalSalary;
        string worktype;
        DataTable dt;
        string CompName, Address, email;
        static MainForms.Form1 frm = new MainForms.Form1();

        public UCpayroll()
        {
            InitializeComponent();
        }
        public List<string> EmpList()
        {
            Database db = new Database();
            List<string> emplist = new List<string>();
            string query = "SELECT lastName, fisrtName FROM UserData WHERE userType='Employee'";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string emp = $"{reader["lastName"]}, {reader["fisrtName"]}";
                    emplist.Add(emp);
                }


                reader.Close();

                return emplist;

                
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
        }
        private void btnTotalPay_Click(object sender, EventArgs e)
        {
            //When btnTotalPay is clicked
            int partTimeHrs = 4;
            int fullTimeHrs = 8;
            double overTimeRate = 45;
            double partTimeRate = 80;
            double fullTimeRate = 150;
            double dailySalary;
            double tax = TotalSalary * .10;
            


            int overTime = int.Parse(txtOverTime.Text);
            double overTimePay = overTime * overTimeRate;
            double daysWorked = double.Parse(txtTotalDays.Text);
            int lates = int.Parse(txtTotalLates.Text);

            double penalty = 50 * lates;

            if (rdbFullTime.Checked)
            {
                dailySalary = fullTimeRate * fullTimeHrs;
                TotalSalary = dailySalary * daysWorked - penalty + overTimePay - totalDeductions;
                txtPay.Text = TotalSalary.ToString();
                totalDeductions += penalty;
                worktype = "Full-time";
            }
            if (rdbPartTime.Checked)
            {
                dailySalary = partTimeRate * partTimeHrs;
                TotalSalary = dailySalary * daysWorked - penalty + overTimePay - totalDeductions;
                txtPay.Text = TotalSalary.ToString();
                totalDeductions += penalty;
                worktype = "Part-time";
            }

            groupBox1.Enabled = true;

            if(!rdbFullTime.Checked && !rdbPartTime.Checked)
            {
                MessageBox.Show("Please choose 'Full-Time' or 'Part-Time'");
                groupBox1.Enabled = false;
            }

            btnSaveData.Enabled = true;
            btnTotalPay.Enabled = false;
            if(rdbFullTime.Checked== false && rdbPartTime.Checked == false)
            {
                btnTotalPay.Enabled = true;
            }

        }

        private void chkSSS_CheckedChanged(object sender, EventArgs e)
        {
            double totalPay = TotalSalary;
            if (chkSSS.Checked)
            {
                totalDeductions += 100;
                totalPay -= totalDeductions;
                txtPay.Text = totalPay.ToString();
                
            }
            if(chkSSS.Checked == false)
            {
                totalDeductions -= 100;
                totalPay -=  totalDeductions;
                txtPay.Text = totalPay.ToString();
                
                //totalPay = TotalSalary;
            }
        }

        private void chkPhilhealth_CheckedChanged(object sender, EventArgs e)
        {
            double totalPay = TotalSalary;
            if (chkPhilhealth.Checked)
            {
                totalDeductions += 300;
                totalPay -= totalDeductions;
                txtPay.Text = totalPay.ToString();
                
            }
            if (chkPhilhealth.Checked == false)
            {
                totalDeductions -= 300;
                totalPay -= totalDeductions;
                txtPay.Text = totalPay.ToString();
                
                //totalPay = TotalSalary;
            }
        }

        private void chkPagibig_CheckedChanged(object sender, EventArgs e)
        {
            double totalPay = TotalSalary;
            if (chkPagibig.Checked)
            {
                totalDeductions += 200;
                totalPay -= totalDeductions;
                txtPay.Text = totalPay.ToString();
                
            }
            if (chkPagibig.Checked == false)
            {
                totalDeductions -= 200;
                totalPay -= totalDeductions;
                txtPay.Text = totalPay.ToString();
                
                //totalPay = TotalSalary;
            }
        }

        private void chkTax_CheckedChanged(object sender, EventArgs e)
        {
            double totalPay = TotalSalary;
            double tax = TotalSalary * .10;
            if (chkTax.Checked)
            {
                totalDeductions += tax;
                totalPay -= totalDeductions;
                txtPay.Text = totalPay.ToString();
                
            }
            if (chkTax.Checked == false)
            {
                totalDeductions -= tax;
                totalPay -= totalDeductions;
                txtPay.Text = totalPay.ToString();
                
                
                //totalPay = TotalSalary;
            }
        }

        private void UCpayroll_Load(object sender, EventArgs e)
        {
            //INITIALIZE CBEMPLOYEE ITEMS
            foreach (var emp in EmpList())
            {
                cbEmpName.Items.Add(emp);
            }
            //INITIALIZE CONTROLS APPEARANCE
            groupBox1.Enabled = false;
            txtOverTime.Text = "0";
            txtTotalDays.Text = "0";
            txtTotalLates.Text = "0";
            //INITIALIZE DATAGRIDVIEW ITEMS
            PayrollDatabase db = new PayrollDatabase();
            string query = "SELECT * FROM Payroll";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                adapter.Dispose();
            }catch(Exception a)
            {
                MessageBox.Show(a.Message);
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
            

        }

        private void txtOverTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTotalDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTotalLates_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            Classes.Payroll employee = new Classes.Payroll();
            employee.month = cbMonth.Text;
            employee.Name = cbEmpName.Text;
            employee.workType = worktype;
            employee.daysWorked = int.Parse(txtTotalDays.Text);
            employee.lates = int.Parse(txtTotalLates.Text);
            employee.overTime = int.Parse(txtOverTime.Text);
            employee.deductions = totalDeductions;
            employee.totalPay = double.Parse(txtPay.Text);
            employee.totalSalary = TotalSalary;
            payrollList.Add(employee);
            dt.Rows.Add(employee.month, employee.Name, employee.workType, employee.daysWorked.ToString(),
                                   employee.overTime.ToString(), employee.lates.ToString(), employee.totalSalary.ToString(), employee.deductions.ToString(),
                                   employee.totalPay.ToString());
            PayrollDatabase db = new PayrollDatabase();
            string query = "INSERT INTO Payroll VALUES(@month,@name,@worktype,@days,@overtime,@lates,@initialpay,@deductions,@totalpay)";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                cmd.Parameters.AddWithValue("@month", employee.month);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@worktype", employee.workType);
                cmd.Parameters.AddWithValue("@days", employee.daysWorked);
                cmd.Parameters.AddWithValue("@overtime", employee.overTime);
                cmd.Parameters.AddWithValue("@lates", employee.lates);
                cmd.Parameters.AddWithValue("@initialpay", employee.totalSalary);
                cmd.Parameters.AddWithValue("@deductions", employee.deductions);
                cmd.Parameters.AddWithValue("@totalpay", employee.totalPay);
                cmd.ExecuteNonQuery();
            }catch(Exception a)
            {
                MessageBox.Show(a.Message);
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }

            
            //txtOverTime.Text = "0";
            //txtPay.Text = "0";
            //txtOverTime.Text = "0";
            //txtTotalLates.Text = "0";
            //txtTotalDays.Text = "0";
            btnSaveData.Enabled = false;
            //groupBox1.Enabled = false;
            //chkPagibig.Checked = false;
            //chkSSS.Checked = false;
            //chkPhilhealth.Checked = false;
            //chkTax.Checked = false;
            //btnTotalPay.Enabled = false;
        }

        private void rdbFullTime_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFullTime.Checked)
            {
                worktype = "Full-time";
            }
            else
            {
                worktype = "Part-time";
            }
        }

        private void cbEmpName_TextChanged(object sender, EventArgs e)
        {
            txtOverTime.Text = "0";
            txtPay.Text = "0";
            txtOverTime.Text = "0";
            txtTotalLates.Text = "0";
            txtTotalDays.Text = "0";
            rdbFullTime.Checked = false;
            rdbPartTime.Checked = false;
            groupBox1.Enabled = false;
            chkPagibig.Checked = false;
            chkSSS.Checked = false;
            chkPhilhealth.Checked = false;
            chkTax.Checked = false;
            btnTotalPay.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOverTime.Text = "0";
            txtPay.Text = "0";
            txtOverTime.Text = "0";
            txtTotalLates.Text = "0";
            txtTotalDays.Text = "0";
            rdbFullTime.Checked = false;
            rdbPartTime.Checked = false;
            groupBox1.Enabled = false;
            chkPagibig.Checked = false;
            chkSSS.Checked = false;
            chkPhilhealth.Checked = false;
            chkTax.Checked = false;
            btnTotalPay.Enabled = true;
        }
        //GET COMPANY DETAILS
        public void DisplayCompInfo()
        {
            Database db = new Database();
            string query = "SELECT CompName, Address, email FROM CompData";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                CompName = $"{reader["CompName"]}";
                Address = $"{reader["Address"]}";
                email = $"{reader["email"]}";

                lblCompName.Text = CompName;
                lblCompEmail.Text = email;
                lblCompAddress.Text = Address;

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
            string[] name = cbEmpName.Text.Split(',');
            lblEmpName.Text = $"{name[0]}, {name[1]}";
            
        }
        private void btnReceipt_Click(object sender, EventArgs e)
        {
            int lates = Convert.ToInt32(txtTotalLates.Text);
            double penalty = lates * 50;
            DisplayCompInfo();
            GetEmp();
            lblWorkType.Text = worktype;
            txtMonth.Text = cbMonth.Text;
            txtWorktype.Text = worktype;
            if(worktype == "Full-time")
            {
                txtRate.Text = "150";
            }
            else
            {
                txtRate.Text = "80";
            }
            txtDays.Text = txtTotalDays.Text;
            txtLatePenalty.Text = penalty.ToString();
            textBox1.Text = txtOverTime.Text;
            txtInitialSalary.Text = TotalSalary.ToString();
            txtDeductions.Text = totalDeductions.ToString();
            txtTotalSalary.Text = txtPay.Text;
            panel5.Visible = true;
        }
    }
}
