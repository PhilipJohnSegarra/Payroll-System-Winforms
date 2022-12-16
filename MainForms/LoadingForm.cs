using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollGroup1.MainForms
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if(progressBar1.Value < 100)
            {
                progressBar1.Value += 5;
                label2.Text = progressBar1.Value.ToString() + "%";
                if(progressBar1.Value == 50)
                {
                    label1.Text = "Fetching Data...please wait";
                }
                if(progressBar1.Value == 80)
                {
                    label1.Text = "Almost There!";
                }
            }
            else
            {
                label1.Text = "Completed!";
                timer1.Stop();
                this.Visible = false;
                this.Close();
            }
        }
    }
}
