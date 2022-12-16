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
    public partial class UserForm : UserControl
    {
        //THIS USERTYPE IS FOR THE SIGN UP FORM TO DETERMINE THE USER TYPE OF A NEW USER
        string usertype;
        int attempts = 0;
        MainForms.Form1 frm;
        HomeForms.AdminHR adHR = new HomeForms.AdminHR();
        HomeForms.EmpForm emp = new HomeForms.EmpForm();
        
        
        //GET USERTYPE AND EMAIL TO DETERMINE THE NEXT COURSE OF ACTIONS TO TAKE PLACE
        public string loggedEmail { get; set; }
        public string userType = "";
        public string[] array = new string[1];

        
        public UserForm()
           
        {
            InitializeComponent();
            
        }
        //GET THE USERTYPE OF AN EXISTING USER
        public string GetUsertype()
        {
            Database db = new Database();
            
            string usertype = "";
            string query = "SELECT userType FROM UserData WHERE email = @email AND password = @password";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                if (CheckLogIn() == true)
                {
                    
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        usertype = reader.GetString(0);
                    }

                    reader.Close();

                }

                
                return usertype;
            }
            catch
            {
                return null;
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();
                

                GC.Collect();
            }
            
        }
        //CHECK LOG IN DETAILS AND IF IT EXISTS IN THE DATABASE
        public bool CheckLogIn()
        {
            
            Database db = new Database();
            string query = "SELECT COUNT(*) FROM UserData WHERE email = @email AND password = @password";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if(count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
            
        }
        
        private void UserForm_Load(object sender, EventArgs e)
        {
            int count;
            Database db = new Database();
            string query = "SELECT COUNT(*) FROM UserData";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 0)
                {
                    this.Visible = true;
                    PnlLocRight();
                    MessageBox.Show("For First time use\nPlease create an admin account");
                    rdbHR.Enabled = false;
                    rdbEmployee.Enabled = false;
                }
                else
                {
                    PnlLocLeft();
                }
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
            
            
                
        }

        private void btnCreAcc_Click(object sender, EventArgs e)
        {
            PnlLocRight();

        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            PnlLocLeft();
        }
        public void PnlLocLeft()
        {
            Form1 frm = (Form1)this.FindForm();
            frm.pnlLogo.Location = new Point(0, 0);
            frm.pnlLogo.BackColor = Color.White;
            frm.lblPhrase.ForeColor = Color.FromArgb(13, 77, 75);
        }
        public void PnlLocRight()
        {
            Form1 frm = (Form1)this.FindForm();
            frm.pnlLogo.Location = new Point(554, 0);
            frm.pnlLogo.BackColor = Color.FromArgb(13, 77, 75);
            frm.lblPhrase.ForeColor = Color.White;
        }
        public bool HasOneAdmin()
        {
            Database db = new Database();
            string query = "SELECT COUNT (userType) FROM UserData WHERE usertype='Admin'";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if(count > 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Classes.User user = new Classes.User();
            user.userID = user.GetID();
            user.lastName = txtLastName.Text;
            user.firstName = txtFirstName.Text;
            user.Address = txtAddress.Text;
            user.userType = usertype;
            user.email = txtNewEmail.Text;
            user.password = txtNewPass.Text;
            Database db = new Database();
            string query = "INSERT INTO UserData (userID, lastName, fisrtName, address, userType, email, password) VALUES(@userID,@lastName,@firstName,@address,@userType,@email,@password)";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                
                cmd.Parameters.AddWithValue("@userID", user.userID);
                cmd.Parameters.AddWithValue("@lastName", user.lastName);
                cmd.Parameters.AddWithValue("@firstName", user.firstName);
                cmd.Parameters.AddWithValue("@address", user.Address);
                cmd.Parameters.AddWithValue("@userType", user.userType);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.ExecuteNonQuery();
                
                if(user.userType == "Admin" && HasOneAdmin())
                {
                    panel11.Visible = true;
                }
                else
                {
                    LoadingForm idf = new LoadingForm();
                    idf.ShowDialog();
                    if(idf.Visible == false)
                    {
                        PnlLocLeft();
                        MessageBox.Show("Now Log In with the account you created!");
                    }
                    
                }

            }
            catch
            {
                MessageBox.Show("Please try to use a different email address!");
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
            

        }

        private void rdbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            usertype = "Admin";
        }

        private void rdbHR_CheckedChanged(object sender, EventArgs e)
        {
            usertype = "HR";
        }

        private void rdbEmployee_CheckedChanged(object sender, EventArgs e)
        {
            usertype = "Employee";
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            frm = (MainForms.Form1)this.FindForm();
            bool Existing = CheckLogIn();
            if (Existing)
            {
                loggedEmail = txtEmail.Text;
                adHR.uCaccount1.loggedEmail = loggedEmail;
                adHR.loggedEmail = loggedEmail;
                emp.loggedEmail = loggedEmail;
                userType = GetUsertype();
                this.ParentForm.Hide();
                if(userType == "Admin" || userType == "HR")
                {
                    
                    LoadingForm idf = new LoadingForm();
                    idf.ShowDialog();
                    if (idf.Visible == false)
                    {
                        txtEmail.Clear();
                        txtPassword.Clear();
                        frm.Hide();
                        adHR.Show();
                    }
                }
                else if(userType == "Employee")
                {
                    
                    LoadingForm idf = new LoadingForm();
                    idf.ShowDialog();
                    if (idf.Visible == false)
                    {
                        txtEmail.Clear();
                        txtPassword.Clear();
                        frm.Hide();
                        emp.Show();
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Account does not exist!");
                attempts++;
                if(attempts == 3)
                {
                    MessageBox.Show("You have attempted to log in to the system for 3 times\nFor security purposes, please, talk to your HR about your log in details, thank you!");
                    Environment.Exit(0);
                }
            }

            
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            string query = "INSERT INTO CompData (CompName, Address, Country, email, telephone) VALUES(@CompName, @Address, @Country, @email, @telephone)";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                              
                cmd.Parameters.AddWithValue("@CompName", txtCompName.Text);
                cmd.Parameters.AddWithValue("@Address", txtCompAddress.Text);
                cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                cmd.Parameters.AddWithValue("@email", txtCompEmail.Text);
                cmd.Parameters.AddWithValue("@telephone", txtCompTel.Text);
                cmd.ExecuteNonQuery();

                PnlLocLeft();
                MessageBox.Show("Now log in with the account you created!");
                btnProceed.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Pls enter the correct information");
            }
            finally
            {
                db.myConnection.Close();
                db.myConnection.Dispose();
                cmd.Dispose();

                GC.Collect();
            }
        }

        private void llblForPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please Talk to your HR for assistance! Thank you!");
        }
    }
}
