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
    public partial class My_Information : Form
    {
        public My_Information()
        {
            InitializeComponent();
            
            textBox9.Text = login.user;

            rpsDataContext dbcon = new rpsDataContext();
            RENTER r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == login.user);
            if (r != null)
            {
                textBox1.Text = r.r_name;

                textBox2.Text = r.rf_name;
                textBox3.Text = r.rm_name;
                textBox5.Text = r.r_contact;
                textBox10.Text = r.renter_email;
                textBox4.Text = r.pt_address;
                textBox7.Text = r.r_off_add;
                textBox6.Text = r.r_occupation;
                textBox12.Text = Convert.ToString(r.rent_date);
                textBox11.Text = r.renter_family_mamber;
                textBox8.Text = r.r_off_contact;
                textBox9.Text = r.rented_flat;


            }


        }



        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Current_Month_Bill().Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {


            textBox5.ReadOnly = false;
            textBox10.ReadOnly = false;
            textBox7.ReadOnly = false;
            textBox6.ReadOnly = false;
            textBox11.ReadOnly = false;
            textBox8.ReadOnly = false;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {


            rpsDataContext dbcon = new rpsDataContext();
            RENTER r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == login.user);
            if (r != null)
            {
                r.r_contact = textBox5.Text;
                r.renter_email = textBox10.Text;
                r.r_off_add = textBox7.Text;
                r.r_occupation = textBox6.Text;
                r.renter_family_mamber = textBox11.Text;
                r.r_off_contact = textBox8.Text;

                dbcon.SubmitChanges();
                MessageBox.Show("Successfully upadated");



                textBox1.Text = r.r_name;

                textBox2.Text = r.rf_name;
                textBox3.Text = r.rm_name;
                textBox5.Text = r.r_contact;
                textBox10.Text = r.renter_email;
                textBox4.Text = r.pt_address;
                textBox7.Text = r.r_off_add;
                textBox6.Text = r.r_occupation;
                textBox12.Text = Convert.ToString(r.rent_date);
                textBox11.Text = r.renter_family_mamber;
                textBox8.Text = r.r_off_contact;
                textBox9.Text = r.rented_flat;

                textBox5.ReadOnly = true;
                textBox10.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox11.ReadOnly = true;
                textBox8.ReadOnly = true;



            }
           


        }

        private void My_Information_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Password_Change().Show();
        }



    }
}
