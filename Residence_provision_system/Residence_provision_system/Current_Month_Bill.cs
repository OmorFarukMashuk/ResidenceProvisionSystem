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
    public partial class Current_Month_Bill : Form
    {
        public Current_Month_Bill()
        {
            InitializeComponent();
            if (login.user != "owner")
            {
                button2.Hide();
                label7.Show();
                label8.Show();
                pictureBox3.Visible = true;
                
                load_contact();
                prev_record();
            }

            load_bill();

            
        }

        private void prev_record()
        {
            rpsDataContext dbcon = new rpsDataContext();



            Bill_Table bill = new Bill_Table();
            var bills = from q in dbcon.Bill_Tables
                        where q.flat == login.user
                        select new
                        {
                            //q.Bill_Date,
                            q.House_Rent,
                            q.Electricity_Bill,
                            q.Water_Bill,
                            q.Gas_Bill,
                            q.Service_Charge
                        };

            if (bills.Sum(x => x.House_Rent) == 0 &&
                bills.Sum(x => x.Electricity_Bill) == 0 &&
                bills.Sum(x => x.Water_Bill) == 0 &&
                bills.Sum(x => x.Gas_Bill) == 0 &&
                bills.Sum(x => x.Service_Charge) == 0)
            {
                pictureBox3.Image = Properties.Resources.check;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.caution;
            }
        }

        private void load_bill()
        {
            rpsDataContext dbcon = new rpsDataContext();
            
            
            string flat_search;
            if (login.user != "owner")
                flat_search = login.user;
            else
                flat_search = flat.flatselected;


            Bill_Table bill = new Bill_Table();
            var query = from q in dbcon.Bill_Tables
                        where q.flat == flat_search
                        select q; 
            
            var curr_bill = query.OrderByDescending(x => x.Bill_Date).First();

            
            if (curr_bill != null)
            {
                richTextBox1.Text = curr_bill.House_Rent.ToString();
                richTextBox2.Text = curr_bill.Electricity_Bill.ToString();
                richTextBox3.Text = curr_bill.Water_Bill.ToString();
                richTextBox4.Text = curr_bill.Gas_Bill.ToString();
                richTextBox5.Text = curr_bill.Service_Charge.ToString();
            }
        }

        private void load_contact()
        {
            rpsDataContext dbcon = new rpsDataContext();
            building b = dbcon.buildings.SingleOrDefault(x => x.b_id == "1");
            if (b != null)
            {

                label8.Text = b.contact;

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Previous_Bills.billType = "new";
            rpsDataContext dbcon = new rpsDataContext();
            RENTER r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == flat.flatselected.ToString());
            if (r == null)
            {

                MessageBox.Show("Currently no renter in this flat");


            }
            else
                new New_Bill().ShowDialog();
        }

        private void Current_Month_Bill_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (login.user == "owner")
                new flat().Show();
            else
                new My_Information().Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }

        private void Current_Month_Bill_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Previous_Bills().Show();
        }
    }
}
