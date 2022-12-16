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
    public partial class AdminHR : Form
    {
        public string loggedEmail { get; set; }
        static MainForms.Form1 frm = new MainForms.Form1();
        public AdminHR()
        {
            InitializeComponent();
        }

        private void AdminHR_Load(object sender, EventArgs e)
        {
            uCcompany1.Show(); button1.BackColor = Color.WhiteSmoke; button1.ForeColor = Color.Black;
            uCpayroll1.Hide(); button2.BackColor = Color.FromArgb(1, 48, 52); button2.ForeColor = Color.White;
            uCemployees1.Hide(); button3.BackColor = Color.FromArgb(1, 48, 52); button3.ForeColor = Color.White;
            uCaccount1.Hide(); button4.BackColor = Color.FromArgb(1, 48, 52); button4.ForeColor = Color.White;
            //ACCESS DATABASE
            Database db = new Database();
            string query = "SELECT userID, userType FROM UserData WHERE email=@email";
            SQLiteDataReader reader;


            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                db.myConnection.Open();
                cmd.Parameters.AddWithValue("@email", loggedEmail);
                reader = cmd.ExecuteReader();
                reader.Read();
                lblUserType.Text = $"{reader["userType"]}";
                lblUserID.Text = $"{reader["userID"]}";

                if(lblUserType.Text != "Admin")
                {
                    uCcompany1.Enabled = false;
                }
                reader.Close();
            }
            catch
            {
                lblUserID.Text = "N/A";
                lblUserType.Text = "N/A";
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
            uCcompany1.Show(); button1.BackColor = Color.WhiteSmoke; button1.ForeColor = Color.Black;
            uCpayroll1.Hide(); button2.BackColor = Color.FromArgb(1, 48, 52); button2.ForeColor = Color.White;
            uCemployees1.Hide(); button3.BackColor = Color.FromArgb(1, 48, 52); button3.ForeColor = Color.White;
            uCaccount1.Hide(); button4.BackColor = Color.FromArgb(1, 48, 52); button4.ForeColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uCcompany1.Hide(); button1.BackColor = Color.FromArgb(1, 48, 52); button1.ForeColor = Color.White;
            uCpayroll1.Show(); button2.BackColor = Color.WhiteSmoke; button2.ForeColor = Color.Black;
            uCemployees1.Hide(); button3.BackColor = Color.FromArgb(1, 48, 52); button3.ForeColor = Color.White;
            uCaccount1.Hide(); button4.BackColor = Color.FromArgb(1, 48, 52); button4.ForeColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            uCcompany1.Hide(); button1.BackColor = Color.FromArgb(1, 48, 52); button1.ForeColor = Color.White;
            uCpayroll1.Hide(); button2.BackColor = Color.FromArgb(1, 48, 52); button2.ForeColor = Color.White;
            uCemployees1.Show(); button3.BackColor = Color.WhiteSmoke; button3.ForeColor = Color.Black;
            uCaccount1.Hide(); button4.BackColor = Color.FromArgb(1, 48, 52); button4.ForeColor = Color.White;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            uCcompany1.Hide(); button1.BackColor = Color.FromArgb(1, 48, 52); button1.ForeColor = Color.White;
            uCpayroll1.Hide(); button2.BackColor = Color.FromArgb(1, 48, 52); button2.ForeColor = Color.White;
            uCemployees1.Hide(); button3.BackColor = Color.FromArgb(1, 48, 52); button3.ForeColor = Color.White;
            uCaccount1.Show(); button4.BackColor = Color.WhiteSmoke; button4.ForeColor = Color.Black;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm.Show();
            
        }
    }
}
