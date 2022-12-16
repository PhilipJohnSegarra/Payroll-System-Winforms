using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollGroup1.HomeForms
{
    public partial class EmpForm : Form
    {
        public string loggedEmail = "";
        static MainForms.Form1 frm = new MainForms.Form1();
        public EmpForm()
        {
            InitializeComponent();
        }

        private void EmpForm_Load(object sender, EventArgs e)
        {
            empHome1.Visible = true;
            empProfile1.Visible = false;
            button1.BackColor = Color.FromArgb(13, 77, 75);
            btnProfile.BackColor = Color.FromArgb(148, 78, 80);
            btnSignOut.BackColor = Color.FromArgb(148, 78, 80);
        }
        //HOME BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            empHome1.Visible = true;
            empProfile1.Visible = false;
            button1.BackColor = Color.FromArgb(13, 77, 75);
            btnProfile.BackColor = Color.FromArgb(148, 78, 80);
            btnSignOut.BackColor = Color.FromArgb(148, 78, 80);
            
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            empHome1.Visible = false;
            empProfile1.Visible = true;
            button1.BackColor = Color.FromArgb(148, 78, 80);
            btnProfile.BackColor = Color.FromArgb(13, 77, 75);
            btnSignOut.BackColor = Color.FromArgb(148, 78, 80);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm.Show();
        }
    }
}
