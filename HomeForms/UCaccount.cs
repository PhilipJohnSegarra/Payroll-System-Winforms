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

namespace PayrollGroup1.HomeForms
{
    public partial class UCaccount : UserControl
    {
        static MainForms.Form1 frm1 = new MainForms.Form1();
        
        public string loggedEmail { get; set; }
        
        
        public UCaccount()
        {
            InitializeComponent();
            
            
        }
       
        private void UCaccount_Load(object sender, EventArgs e)
        {
            
            Database db = new Database();
            string query = "SELECT lastName, fisrtName, address, email, password FROM UserData WHERE email = @email";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                cmd.Parameters.AddWithValue("@email", loggedEmail.ToString());
                SQLiteDataReader data = cmd.ExecuteReader();
                data.Read();
                txtLastName.Text = $"{data["lastName"]}";
                txtFirstName.Text = $"{data["fisrtName"]}";
                txtAddress.Text = $"{data["address"]}";
                txtEmail.Text = $"{data["email"]}";
                txtPassword.Text = $"{data["password"]}";

                data.Close();

            }
            catch
            {
                MessageBox.Show("Error loading information!");
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
                cmd.Parameters.AddWithValue("@loggedEmail", loggedEmail);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Changes have been saved!");
            }
            catch (Exception a)
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
