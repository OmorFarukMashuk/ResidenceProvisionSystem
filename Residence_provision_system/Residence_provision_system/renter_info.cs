using Residence_provision_system.Properties;
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
//using System.Data.Linq.Binary;

namespace Residence_provision_system
{
    public partial class renter_info : Form
    {
        public renter_info()
        {
            InitializeComponent();
            load_info();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new flat().Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }

        private void load_info()
        {
            try
            {
                rpsDataContext dbcon = new rpsDataContext();

                RENTER r;

                ///flat f = new flat();
        
                r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == flat.flatselected);

                if (r != null)
                {


                    textBox1.Text = r.r_name ;
                    textBox2.Text = r.rf_name;
                    textBox3.Text = r.rm_name;
                    textBox4.Text = r.pt_address;
                    textBox5.Text = r.r_contact;
                    textBox6.Text = r.r_occupation;
                    textBox7.Text = r.r_off_add;
                    textBox8.Text = r.r_off_contact;
                    textBox9.Text = r.rented_flat;
                    textBox10.Text = r.renter_family_mamber;
                    textBox11.Text = r.renter_email;
                    textBox12.Text = r.rent_date.ToString();
                    renterImage.Image = load_image(r);

                }
             

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Image load_image(RENTER r)
        {
            byte[] img = r.renter_image.ToArray();
            MemoryStream ms = new MemoryStream(img);
            //renterImage.Image = Image.FromStream(ms);

            return Image.FromStream(ms);
        }

        private void renter_info_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show(" Are you sure want to remove this renter from your building?",
                "Clear", MessageBoxButtons.YesNo);

            if (dlg == DialogResult.Yes)
            {
                rpsDataContext dbcon = new rpsDataContext();
                
                var bills = from b in dbcon.Bill_Tables
                            where b.flat == flat.flatselected
                            select b;
                dbcon.Bill_Tables.DeleteAllOnSubmit(bills);

                var renter = from r in dbcon.RENTERs
                             where r.rented_flat == flat.flatselected
                             select r;

                dbcon.RENTERs.DeleteAllOnSubmit(renter);

                var log = from l in dbcon.logins
                          where l.user_id == flat.flatselected
                          select l;
                dbcon.logins.DeleteAllOnSubmit(log);

                           
                
                dbcon.SubmitChanges();

                MessageBox.Show("Cleared Scuccessfully");
                this.Hide();
                new floors().Show();
                

            }
            else if (dlg == DialogResult.No)
            {
                this.Show();
            }
        }

    }
}
