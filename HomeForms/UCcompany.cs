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
    public partial class UCcompany : UserControl
    {
        public UCcompany()
        {
            InitializeComponent();
        }

        private void UCcompany_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            string query = "SELECT * FROM CompData";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                SQLiteDataReader data = cmd.ExecuteReader();
                data.Read();
                txtCompName.Text = $"{data["CompName"]}";
                txtCompAddress.Text = $"{data["Address"]}";
                textBox2.Text = $"{data["Country"]}";
                textBox3.Text = $"{data["email"]}";
                textBox4.Text = $"{data["telephone"]}";

                data.Close();
            }
            catch
            {
                MessageBox.Show("Error loading Company Information");
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
            string query = "UPDATE CompData SET CompName=@compname, Address=@address, Country=@country, email=@email, telephone=@telephone";
            db.myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, db.myConnection);
            try
            {
                
                cmd.Parameters.AddWithValue("@compname", txtCompName.Text);
                cmd.Parameters.AddWithValue("@address", txtCompAddress.Text);
                cmd.Parameters.AddWithValue("@country", textBox2.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                cmd.Parameters.AddWithValue("@telephone", textBox4.Text);
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
