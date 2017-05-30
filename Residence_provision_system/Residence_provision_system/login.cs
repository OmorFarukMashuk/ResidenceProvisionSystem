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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '●';
        }

        public static string user = "";
  
        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
     
       
            try
            {
                rpsDataContext dbcon = new rpsDataContext();
                login l = new login();

                l = dbcon.logins.SingleOrDefault(x => x.user_id == textBox1.Text && x.password == textBox2.Text);

                user = textBox1.Text;
                if (user == "owner" && textBox1.Text == l.user_id && textBox2.Text == l.password)
                {
                    
                    this.Hide();
                    new floors().Show();
                }


                else if (textBox1.Text == l.user_id && textBox2.Text == l.password)
                {
                    
                    this.Hide();
                    new My_Information().Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Username or Password" + "\nerror message: "+ex.Message);
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Sign_Up().Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
