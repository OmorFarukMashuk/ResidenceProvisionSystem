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
    public partial class Previous_Bills : Form
    {
        
        
        public Previous_Bills()
        {
            InitializeComponent();

            rpsDataContext dbcon = new rpsDataContext();
            comboBox1.DataSource = dbcon.RENTERs;
            comboBox1.ValueMember = "rented_flat";

            load_data();

            //string s = comboBox1.SelectedText;

            if (login.user != "owner")
            {
                button1.Visible = false;
                button2.Visible = false;
                //comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.SelectedIndex = comboBox1.FindStringExact(login.user);
                comboBox1.Enabled = false;
                //comboBox1.IsAccessible = false  ;
                //remove_flats_combo();
            }
            else
                comboBox1.SelectedIndex = comboBox1.FindStringExact(flat.flatselected);


        }

        private void load_data()
        {
            rpsDataContext dbcon = new rpsDataContext();
            
            //Bill_Table bill = new Bill_Table();
            var bills = from q in dbcon.Bill_Tables
                        where q.flat == comboBox1.SelectedValue.ToString()
                        select new { q.Bill_Date,
                            q.House_Rent, 
                            q.Electricity_Bill, 
                            q.Water_Bill,
                            q.Gas_Bill, 
                            q.Service_Charge};

            bills = bills.OrderByDescending(x => x.Bill_Date);
            dataGridView1.DataSource = bills;
      
            RENTER r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == comboBox1.SelectedValue.ToString());

            if (r != null)
            {
                label5.Text = r.r_name;
                label6.Text = r.rent_date.ToString(); ;
            }

             
 
        }
        private void remove_flats_combo()
        {

            comboBox1.Items.Clear();
            comboBox1.Items.Add(login.user);
            comboBox1.SelectedIndex = comboBox1.FindStringExact(login.user);

        }


        public static string billType = "";



        private void button1_Click(object sender, EventArgs e)
        {
            billType = "update";
            new New_Bill().ShowDialog();
           
        }

        private void Previous_Bills_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Current_Month_Bill().Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show(); 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            load_data();
        }

  

        private void button3_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            
            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program
            app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            // changing the name of active sheet
            worksheet.Name = "Exported from gridview";


            // storing header part in Excel
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }



            // storing Each row and column value to excel sheet
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }

            /*
            // save the application
            workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            */

           
            // Exit from the application
            app.Quit();
            
        }

    }
}
