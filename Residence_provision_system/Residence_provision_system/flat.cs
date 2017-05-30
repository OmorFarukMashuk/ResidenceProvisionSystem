using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Residence_provision_system
{
    public partial class flat : Form
    {
        
        public flat()
        {
            InitializeComponent();
            //richTextBox1.Text = "sadi khondoker\n01696969";

            label1.Text = floors.floor_no;
            label2.Text = floors.flat1;
            label3.Text = floors.flat2;


            rpsDataContext dbcon = new rpsDataContext();
            
            RENTER r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == label2.Text);
            if (r != null)
            {
                label4.Visible = true;
                pictureBox3.Visible = true;
                label6.Visible = true;
                flat1_status();

                richTextBox1.Text = r.r_name+"\n" +r.r_contact;


            }
            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == label3.Text);
            if (r != null)
            {
                label5.Visible = true;
                pictureBox4.Visible = true;
                label7.Visible = true;
                flat2_status();

                richTextBox2.Text = r.r_name + "\n" + r.r_contact;
            }
            
         

        }

        private void flat1_status()
        {
            rpsDataContext dbcon = new rpsDataContext();



            Bill_Table bill = new Bill_Table();
            var query = from q in dbcon.Bill_Tables
                        where q.flat == label2.Text
                        select new
                        {
                            //q.Bill_Date,
                            q.House_Rent,
                            q.Electricity_Bill,
                            q.Water_Bill,
                            q.Gas_Bill,
                            q.Service_Charge
                        };

            if (query.Sum(x => x.House_Rent) == 0 &&
                query.Sum(x => x.Electricity_Bill) == 0 &&
                query.Sum(x => x.Water_Bill) == 0 &&
                query.Sum(x => x.Gas_Bill) == 0 &&
                query.Sum(x => x.Service_Charge) == 0)
            {
                pictureBox3.Image = Properties.Resources.check;
                label6.Text = "paid";
            }
            else
            {
                label6.Text = "due";
                pictureBox3.Image = Properties.Resources.caution;
            }
                
        }

        private void flat2_status()
        {
            rpsDataContext dbcon = new rpsDataContext();

            Bill_Table bill = new Bill_Table();
            var query = from q in dbcon.Bill_Tables
                        where q.flat == label3.Text
                        select new
                        {
                            //q.Bill_Date,
                            q.House_Rent,
                            q.Electricity_Bill,
                            q.Water_Bill,
                            q.Gas_Bill,
                            q.Service_Charge
                        };



            if (query.Sum(x => x.House_Rent) == 0 &&
                query.Sum(x => x.Electricity_Bill) == 0 &&
                query.Sum(x => x.Water_Bill) == 0 &&
                query.Sum(x => x.Gas_Bill) == 0 &&
                query.Sum(x => x.Service_Charge) == 0)
            {
                pictureBox4.Image = Properties.Resources.check;
                label5.Text = "paid";
            }
            else
            {
                label5.Text = "due";
                pictureBox4.Image = Properties.Resources.caution;
            }

        }

        public static string flatselected = "";

        private bool flat_has_renter()
        {
            rpsDataContext dbcon = new rpsDataContext();
            RENTER r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == flatselected);

            if (r != null)
                return true;
            else
                return false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new floors().Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }

        private void f1bill_Click(object sender, EventArgs e)
        {
            flatselected = label2.Text;

            if (flat_has_renter() == true)
            {
                this.Hide();
                new Current_Month_Bill().Show();
            }

            else
                MessageBox.Show("currently no renter in this flat");
        }

        private void f1info_Click(object sender, EventArgs e)
        {
            flatselected = label2.Text;

            if (flat_has_renter() == true)
            {
                this.Hide();
                new renter_info().Show();
            }

            else
                MessageBox.Show("currently no renter in this flat");
        }

        private void f2bill_Click(object sender, EventArgs e)
        {

            flatselected = label3.Text;
            if (flat_has_renter() == true)
            {
                this.Hide();
                new Current_Month_Bill().Show();
            }
            else
                MessageBox.Show("currently no renter in this flat");


        }

        private void f2info_Click(object sender, EventArgs e)
        {
            flatselected = label3.Text;
            if (flat_has_renter() == true)
            {
                this.Hide();

                new renter_info().Show();
            }

            else
                MessageBox.Show("currently no renter in this flat");

        }

        private void flat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


    }
}
