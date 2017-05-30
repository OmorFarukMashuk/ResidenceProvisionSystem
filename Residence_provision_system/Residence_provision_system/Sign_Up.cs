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

namespace Residence_provision_system
{

    public partial class Sign_Up : Form
    {

        public Sign_Up()
        {
            InitializeComponent();
            rpsDataContext dbcon = new rpsDataContext();
            giveRent();
            

        }

        private void InitializePictureBox()
        {
            pictureBox1.Padding = new Padding(0);
            //pictureBox2.Padding = new Padding(0);


        }

        public void giveRent()
        {
            rpsDataContext dbcon = new rpsDataContext();
            RENTER r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "A-1");
            if (r != null)
                RentedFlatComboBox.Items.Remove("A-1");

            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "A-2");
            if (r != null)
                RentedFlatComboBox.Items.Remove("A-2");

            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "A-3");
            if (r != null)
                RentedFlatComboBox.Items.Remove("A-3");

            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "A-4");
            if (r != null)
                RentedFlatComboBox.Items.Remove("A-4");
            
            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "A-5");
            if (r != null)
                RentedFlatComboBox.Items.Remove("A-5");

            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "A-6");
            if (r != null)
                RentedFlatComboBox.Items.Remove("A-6");
            
            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "B-1");
            if (r != null)
                RentedFlatComboBox.Items.Remove("B-1");
            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "B-2");
            if (r != null)
                RentedFlatComboBox.Items.Remove("B-2");

            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "B-3");
            if (r != null)
            
                RentedFlatComboBox.Items.Remove("B-3");

            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "B-4");
            if (r != null)
            
                RentedFlatComboBox.Items.Remove("B-4");

            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "B-5");
            if (r != null)
                RentedFlatComboBox.Items.Remove("B-5");
            r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == "B-6");
            if (r != null)
                RentedFlatComboBox.Items.Remove("B-6");


        }



        private void ConfirmButton_Click(object sender, EventArgs e)
        {

            try
            {
                rpsDataContext dbcon = new rpsDataContext();

                if (FirstNameTextBox.Text == null || FatherFirstNameTextBox.Text == null ||
                    MotherFirstNameTextBox == null || ContactNoTextbox.Text == null ||
                    EmailTextbox.Text == null || PAddressTextBox.Text == null ||
                    OffAddressTextBox == null || comboBox1.Text == null ||
                    OfficeContNoTextBox == null || RentedFlatComboBox.Text == null ||
                    renterImage.Image == null)
                {
                    MessageBox.Show("Please fill up all the feilds!!!");

                }

                else if (textBox1.Text.Length < 6)
                {
                    MessageBox.Show("Select a password with at least 6 characters !!");
                }

                else
                {

                    //renter
                    RENTER r = new RENTER();
                    r.r_name = FirstNameTextBox.Text + " " + LastNameTextBox.Text;
                    r.rf_name = FatherFirstNameTextBox.Text + " " + FatherLastNameTextBox.Text;
                    r.rm_name = MotherFirstNameTextBox.Text + " " + MotherLastNameTextBox.Text;
                    r.r_contact = ContactNoTextbox.Text;
                    r.renter_email = EmailTextbox.Text;
                    r.pt_address = PAddressTextBox.Text;
                    r.r_off_add = OffAddressTextBox.Text;
                    r.r_occupation = textBox2.Text;
                    r.rent_date = dateTimePicker1.Value;
                    r.renter_family_mamber = comboBox1.Text;
                    r.r_off_contact = OfficeContNoTextBox.Text;
                    r.rented_flat = RentedFlatComboBox.Text;

                    byte[] img = null;
                    FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img = br.ReadBytes((int)fs.Length);

                    r.renter_image = img;

                    //login_table
                    login l = new login();
                    l.user_id = RentedFlatComboBox.Text;
                    l.password = textBox1.Text;

                    //deafult bill
                    Bill_Table bill = new Bill_Table();
                    bill.flat = RentedFlatComboBox.Text;
                    bill.House_Rent = 0;
                    bill.Electricity_Bill = 0;
                    bill.Water_Bill = 0;
                    bill.Gas_Bill = 0;
                    bill.Service_Charge = 0;

                    dbcon.logins.InsertOnSubmit(l);
                    dbcon.RENTERs.InsertOnSubmit(r);
                    dbcon.Bill_Tables.InsertOnSubmit(bill);

                    dbcon.SubmitChanges();

                    MessageBox.Show("Congratulations!! You may login now");
                    this.Hide();
                    new login().Show();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        string imgLoc = "";


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Padding = new Padding(0);
        }

        private void browse_buttom_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG files (*.jpg) | *.jpg | GIF files (*.gif) | *.gif | All files (*.*) | *.*";
                dlg.Title = "Select Photo";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName.ToString();
                    renterImage.ImageLocation = imgLoc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       /* private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            int back_width = pictureBox1.Image.Width + ((pictureBox1.Image.Width * 20)/100);
            int back_height = pictureBox1.Image.Height + ((pictureBox1.Image.Height * 20) / 100);

            Bitmap back_1 = new Bitmap(back_width, back_height);
            Graphics g = Graphics.FromImage(back_1);
            g.DrawImage(pictureBox1.Image, new Rectangle(Point.Empty, back_1.Size));
            pictureBox1.Image = back_1;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            int logout_width = pictureBox1.Image.Width + ((pictureBox2.Image.Width * 20) );
            int logout_height = pictureBox1.Image.Height + ((pictureBox2.Image.Height * 20) );

            Bitmap logout_1 = new Bitmap(logout_width, logout_height);
            Graphics g = Graphics.FromImage(logout_1);
            g.DrawImage(pictureBox2.Image, new Rectangle(Point.Empty, logout_1.Size));
            pictureBox2.Image = logout_1;

        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox2.Image;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox2.Image;
        } */

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
            //MessageBox.Show("back button");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("logout button");
        }

        private void Sign_Up_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }



    }
}
