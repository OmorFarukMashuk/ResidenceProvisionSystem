using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Residence_provision_system.Properties;

namespace Residence_provision_system
{
    public partial class floors : Form
    {
        public static string floor_no = "", flat1 = "", flat2 = "";

        public floors()
        {
            InitializeComponent();
            load_house_info();
        }

        private void load_house_info()
        {
            rpsDataContext dbcon = new rpsDataContext();
            building b = dbcon.buildings.SingleOrDefault(x => x.b_id == "1");
            if (b != null)
            {

                HouseAddressTextbox.Text = b.address;
                textBox1.Text = b.owner_name;
                textBox2.Text = b.contact;

            }

            else
            {
                MessageBox.Show("Data not found");
            }
        }

        private void F1Button_Click(object sender, EventArgs e)
        {
            floor_no = "Floor 1";
            flat1 = "A-1";
            flat2 = "B-1";


            this.Hide();
            new flat().Show();
        }

        private void F2Button_Click(object sender, EventArgs e)
        {
            floor_no = "Floor 2";
            flat1 = "A-2";
            flat2 = "B-2";
            this.Hide();
            new flat().Show();
        }

        private void F3Button_Click(object sender, EventArgs e)
        {

            floor_no = "Floor 3";
            flat1 = "A-3";
            flat2 = "B-3";
            this.Hide();
            new flat().Show();
        }


        private void F4Button_Click(object sender, EventArgs e)
        {

            floor_no = "Floor 4";
            flat1 = "A-4";
            flat2 = "B-4";
            this.Hide();
            new flat().Show();

        }

        private void F5Button_Click(object sender, EventArgs e)
        {

            floor_no = "Floor 5";
            flat1 = "A-5";
            flat2 = "B-5";
            this.Hide();
            new flat().Show();
        }

        private void F6Button_Click(object sender, EventArgs e)
        {
            floor_no = "Floor 6";
            flat1 = "A-6";
            flat2 = "B-6";
            this.Hide();
            new flat().Show();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }

        public string search;
        private void button1_Click(object sender, EventArgs e)
        {
            search = textBox2.Text;
            textBox2.ReadOnly = false;
            

        }

        private void button2_Click(object sender, EventArgs e)
        {

            rpsDataContext dbcon = new rpsDataContext();

            
            building b = dbcon.buildings.SingleOrDefault(x => x.contact == search);
            if (b != null)
            {

                b.contact = textBox2.Text;
                dbcon.SubmitChanges();
                MessageBox.Show("Successfully upadated");

                textBox2.Text = b.contact;
                textBox2.ReadOnly = true;
            }
        }

        private void floors_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Password_Change().Show();
        }

       


    }
}
