using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Drawing.SplitContainerViewInfo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EslamApp
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int x1 = 0;
        public static int x2 = 0;

        public bool key1 = false;
        public static bool key2 = false;

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel5.Text = DateTime.Today.ToString("yyyy/MM/dd");
            textEdit2.Enabled = false;
            textEdit2.Properties.UseSystemPasswordChar= true;

            
            try
            {
                string users = "select UID as 'رقم اليوزر',Trim(UName) as 'اسم اليوزر' from ULogin";
                sqliteHelper.lookupEditFill(users, "اسم اليوزر","رقم اليوزر",lookUpEdit1,);
      


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            textEdit2.Enabled= true;
            textEdit2.Focus();
        }

        private void textEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
        }

        private void checkEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == false)
            {
                textEdit2.Properties.UseSystemPasswordChar = true;
            }
            else
            {
                textEdit2.Properties.UseSystemPasswordChar = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string selelctString = "select UName , UPassword from Ulogin where UName = '" + lookUpEdit1.Text + "'" + "AND UPassword ='" + textEdit2.Text + "'";

            if (sqliteHelper.isFound(selelctString))
            {
                this.key1 = false;

                string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                        "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بتسجيل الدخول"+"','"+DateTime.Today.ToString("yyyy-MM-dd")+"')";
                sqliteHelper.insert(InsString, 0);
                //this.key1 = false;
                this.Hide();
                if (x1 == 0)
                {
                  //  x1 = 1;
                    Master f1 = new Master();
                    
                    f1.UName.Text = lookUpEdit1.Text;
                    f1.key2 = true;
                    f1.Show();

                }

            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(x2 == 0)
            {
                x2 = 1;
                DataBase db = new DataBase();
                db.Show();
            }
        }
    }
}
