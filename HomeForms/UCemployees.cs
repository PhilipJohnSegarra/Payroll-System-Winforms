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

namespace PayrollGroup1.HomeForms
{
    public partial class UCemployees : UserControl
    {

        HomeForms.AdminHR adhr;
        DataTable dt = new DataTable();
        public UCemployees()
        {
            InitializeComponent();
        }

        private void UCemployees_Load(object sender, EventArgs e)
        {
            txtUsertype.Text = "Employee";
            Database db = new Database();
            string query = "SELECT userID, lastName, fisrtName, userType, address, email, password FROM UserData WHERE userType='Employee'";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Classes.User employee = new Classes.User();
            employee.userID = employee.GetID();
            employee.lastName = txtLastName.Text;
            employee.firstName = txtFirstName.Text;
            employee.Address = txtAddress.Text;
            employee.userType = txtUsertype.Text;
            employee.email = txtEmail.Text;
            employee.password = txtPassword.Text;
            dt.Rows.Add(employee.userID, employee.lastName, employee.firstName, employee.userType, employee.Address,
                                   employee.email, employee.password);
            string emp = $"{employee.lastName}, {employee.firstName}";
            adhr = (HomeForms.AdminHR)this.FindForm();
            adhr.uCpayroll1.cbEmpName.Items.Add(emp);
            Database db = new Database();
            string query = "INSERT INTO UserData (userID, lastName, fisrtName, userType, address, email, password) VALUES(@userID, @lastname, @firstname, @usertype, @address, @email, @password)";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                cmd.Parameters.AddWithValue("@userID", employee.userID);
                cmd.Parameters.AddWithValue("@lastname", employee.lastName);
                cmd.Parameters.AddWithValue("@firstname", employee.firstName);
                cmd.Parameters.AddWithValue("@usertype", employee.userType);
                cmd.Parameters.AddWithValue("@address", employee.Address);
                cmd.Parameters.AddWithValue("@email", employee.email);
                cmd.Parameters.AddWithValue("@password", employee.password);
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


        }

    }
}
