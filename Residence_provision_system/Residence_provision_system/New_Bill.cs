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
    public partial class New_Bill : Form
    {
        public New_Bill()
        {
            InitializeComponent();
            if (Previous_Bills.billType == "update")
            {
                reform_newToupdate();
                button2.Visible = true;
            }

            //comboBox1.Items.Add(flat.flatselected);
            //comboBox1.SelectedIndex = 0;

            comboBox1.SelectedIndex = comboBox1.FindStringExact(flat.flatselected);
       
            
        }


        private void reform_newToupdate()
        {
            this.Text = "Update_bill";
            button1.Text = "update bill";
           
        }


        private void New_Bill_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void update_bill_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                rpsDataContext dbcon = new rpsDataContext();

                if (this.Text == "Update_bill")
                {
                    //rpsDataContext dbcon = new rpsDataContext();
                    //int id = Int32.Parse(comboBox1.SelectedValue.ToString());
                    Bill_Table bill = dbcon.Bill_Tables.SingleOrDefault(x => x.flat == comboBox1.SelectedItem.ToString() && x.Bill_Date == dateTimePicker1.Value);
                    if (bill != null)
                    {


                        bill.House_Rent = Convert.ToDouble(richTextBox1.Text);
                        bill.Electricity_Bill = Convert.ToDouble(richTextBox2.Text);
                        bill.Water_Bill = Convert.ToDouble(richTextBox3.Text);
                        bill.Gas_Bill = Convert.ToDouble(richTextBox4.Text);
                        bill.Service_Charge = Convert.ToDouble(richTextBox5.Text);
                        dbcon.SubmitChanges();
                        MessageBox.Show("Bill Updated successfully");
                        this.Dispose();
                    }

                }

                else
                {
                    // Bill_Table bill = dbcon.Bill_Tables.SingleOrDefault(x => x.Bill_Date == dateTimePicker1.Value);


                    if (comboBox1.Text == null || dateTimePicker1.Text == null || richTextBox1.Text == null ||
                        richTextBox2.Text == null || richTextBox3.Text == null ||
                        richTextBox4.Text == null || richTextBox5.Text == null)
                    {
                        MessageBox.Show("Please fill up all feilds with valid data");
                    }

                    else if (dbcon.Bill_Tables.Any(x => x.flat == comboBox1.SelectedItem.ToString() && x.Bill_Date == dateTimePicker1.Value) == true)
                    {
                        MessageBox.Show("A bill aready exists for this date in this flat");
                    }
                    else
                    {

                        Bill_Table bill = new Bill_Table();

                        bill.flat = comboBox1.SelectedItem.ToString();
                        bill.Bill_Date = dateTimePicker1.Value;
                        bill.House_Rent = Convert.ToDouble(richTextBox1.Text);
                        bill.Electricity_Bill = Convert.ToDouble(richTextBox2.Text);
                        bill.Water_Bill = Convert.ToDouble(richTextBox3.Text);
                        bill.Gas_Bill = Convert.ToDouble(richTextBox4.Text);
                        bill.Service_Charge = Convert.ToDouble(richTextBox5.Text);

                        dbcon.Bill_Tables.InsertOnSubmit(bill);
                        dbcon.SubmitChanges();
                        MessageBox.Show("new bill created for " + comboBox1.SelectedItem.ToString());
                        this.Dispose();
                    }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.Text == "New_Bill")
            {
                rpsDataContext dbcon = new rpsDataContext();
                RENTER r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == comboBox1.SelectedItem.ToString());
                if (r == null)
                {

                    dateTimePicker1.Enabled = false;
                    richTextBox1.Enabled = false;
                    richTextBox2.Enabled = false;
                    richTextBox3.Enabled = false;
                    richTextBox4.Enabled = false;
                    richTextBox5.Enabled = false;
                    button1.Enabled = false;
                    MessageBox.Show("Currently no renter in this flat");


                }
                else
                {

                    dateTimePicker1.Enabled = true;
                    richTextBox1.Enabled = true;
                    richTextBox2.Enabled = true;
                    richTextBox3.Enabled = true;
                    richTextBox4.Enabled = true;
                    richTextBox5.Enabled = true;
                    button1.Enabled = true;
                    //MessageBox.Show("Currently no renter in this flat");


                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rpsDataContext dbcon = new rpsDataContext();
            //int id = Int32.Parse(comboBox1.SelectedValue.ToString());
            Bill_Table bill = dbcon.Bill_Tables.SingleOrDefault(x => x.flat == comboBox1.SelectedItem.ToString() && x.Bill_Date == dateTimePicker1.Value);
            if (bill != null)
            {


                richTextBox1.Text = Convert.ToString(bill.House_Rent);
                richTextBox2.Text = Convert.ToString(bill.Electricity_Bill);
                richTextBox3.Text = Convert.ToString(bill.Water_Bill);
                richTextBox4.Text = Convert.ToString(bill.Gas_Bill);
                richTextBox5.Text = Convert.ToString(bill.Service_Charge);
            }
            else
            {
                MessageBox.Show("Bill not found");
            }

        }
    }
}
