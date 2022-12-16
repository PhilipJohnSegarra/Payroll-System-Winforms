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
    public partial class EmpProfile : UserControl
    {
        HomeForms.EmpForm emp;
        public EmpProfile()
        {
            InitializeComponent();
        }

        private void EmpProfile_Load(object sender, EventArgs e)
        {
            emp = (HomeForms.EmpForm)this.FindForm();
            Database db = new Database();
            string query = "SELECT lastName, fisrtName, address, email, password FROM UserData WHERE email=@email";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                  
                cmd.Parameters.AddWithValue("@email", emp.loggedEmail);
                SQLiteDataReader reader = cmd.ExecuteReader();   
                reader.Read();
                txtLastName.Text = $"{reader["lastName"]}";
                txtFirstName.Text = $"{reader["fisrtName"]}";
                txtAddress.Text = $"{reader["address"]}";
                txtEmail.Text = $"{reader["email"]}";
                txtPassword.Text = $"{reader["password"]}";

                reader.Close();
                       
            }
            catch(Exception a)
            {
                string title = a.Message;
                MessageBox.Show("An Error Occurred, Please talk to your HR for assistance", title);
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            emp = (HomeForms.EmpForm)this.FindForm();
            Database db = new Database();
            string query = "UPDATE UserData SET lastName=@lastname, fisrtName=@firstName, address=@address, email=@email, password=@password WHERE email=@loggedEmail";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {            
                                
                cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@loggedEmail", emp.loggedEmail);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Changes have been saved!");
            }
            catch (Exception a)
            {
                string title = a.Message;
                MessageBox.Show("An Error Occurred, Please talk to your HR for assistance", title);
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
