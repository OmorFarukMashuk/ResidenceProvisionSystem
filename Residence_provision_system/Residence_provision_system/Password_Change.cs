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
    public partial class Password_Change : Form
    {
        public Password_Change()
        {
            InitializeComponent();
            textBox1.PasswordChar = textBox2.PasswordChar = textBox3.PasswordChar = '●';
            label5.Text = login.user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                rpsDataContext dbcon = new rpsDataContext();

                if (dbcon.logins.Any(x => x.user_id == login.user &&
                    x.password == textBox1.Text) == true)
                {
                    if (textBox2.Text.Length < 6)
                    {
                        MessageBox.Show("Select a password with at least 6 characters !!");
                    }
                    else if(textBox2.Text != textBox3.Text)
                    {
                        MessageBox.Show("Password doesn't match");
                    }

                    else
                    {

                        login l = new login();
                        l = dbcon.logins.SingleOrDefault(x => x.user_id == login.user);

                        if(l!= null)
                        {
                            l.user_id = label5.Text;
                            l.password = textBox2.Text;
                            dbcon.SubmitChanges();
                            MessageBox.Show("Password successfully changed! Please re-login");
                            this.Dispose();
                            new login().Show();
                        }


                    }
                }

                else
                {
                    MessageBox.Show("Your password is incorrect!");
                }


            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        
        
        }

        private void Password_Change_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            if (login.user == "owner")
            {
                new floors().Show();
            }
            else
            {
                new My_Information().Show();
            }
        }
    
    }

}
