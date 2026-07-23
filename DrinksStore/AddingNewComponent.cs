using DevExpress.XtraEditors;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EslamApp
{
    public partial class AddingNewComponent : DevExpress.XtraEditors.XtraForm
    {
        public AddingNewComponent()
        {
            InitializeComponent();
            textEdit2.Select();
        }

        private string isItWithOtherCom = "0";

        private void AddingNewComponent_Load(object sender, EventArgs e)
        {
            string selectTarget = "Select UID as 'رقم الوحدة' , UName as 'اسم الوحدة' from Units";
            sqliteHelper.lookupEditFill(selectTarget, "اسم الوحدة", "رقم الوحدة", this.lookUpEdit1);

            textEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");

            sqliteHelper.select("select CName from Components",comboBox1);

            textEdit2.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit2.Text != "" && textEdit3.Text != "" && lookUpEdit1.Text != "" && textEdit4.Text !="")
            {
                try
                {
                    double d = double.Parse(textEdit3.Text);
                    double b = double.Parse(textEdit4.Text);
                    
                    if (MessageBox.Show("هل انت متأكد وتريد المتابعة؟", "مدير المكونات", MessageBoxButtons.YesNo) ==
                      System.Windows.Forms.DialogResult.Yes)
                    {

                        if (toggleSwitch1.IsOn == true)
                        {
                            string insertString = "insert into Components (CID,CName,CQuantity,CUnite,CDivideOn,CPrice,CTotal,CAddingDate,CNote,isItWithOtherCom) values ((select coalesce(max(CID),0)+1 from Components),'" + textEdit2.Text + "','0','" + lookUpEdit1.Text + "'," + textEdit4.Text + ",'" + textEdit3.Text + "','0','" + textEdit1.Text + "','" + comboBox1.Text + "' ," + isItWithOtherCom + ")";
                            sqliteHelper.insert(insertString, 1);

                            string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                               "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بإضافة مكون جديد الى المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                            sqliteHelper.insert(InsString, 0);
                        }
                        else if (toggleSwitch1.IsOn == false)
                        {
                            string insertString = "insert into Components (CID,CName,CQuantity,CUnite,CDivideOn,CPrice,CTotal,CAddingDate,CNote,isItWithOtherCom) values ((select coalesce(max(CID),0)+1 from Components),'" + textEdit2.Text + "','0','" + lookUpEdit1.Text + "'," + textEdit4.Text + ",'" + textEdit3.Text + "','0','" + textEdit1.Text + "','" + textEdit7.Text + "' ," + isItWithOtherCom + ")";
                            sqliteHelper.insert(insertString, 1);

                            string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                               "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بإضافة مكون جديد الى المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                            sqliteHelper.insert(InsString, 0);
                        }

                    }
                }
                catch  { MessageBox.Show("الاسعار يجب ان تكون رقماً"); }
            }
            else
            {
                MessageBox.Show("يجب ادخال كامل البيانات الضرورية المعلمة بالنجمة");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddingNewComponent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Components.x1 = 0;
        }

        private void textEdit3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit2.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit1.Focus();

            }
        }

        private void textEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                lookUpEdit1.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit1.Focus();

            }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit2.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit1.Focus();

            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
                textEdit3.Focus();
            string selectString = "select USmall from Units where UName ='" + lookUpEdit1.Text+"'";
            if (int.Parse(sqliteHelper.selectWithReturn(selectString)) == 1)
            {
                textEdit4.Enabled = true ;
                textEdit4.Text = "1";

            }else if(int.Parse(sqliteHelper.selectWithReturn(selectString)) == 0)
            {
                textEdit4.Enabled = false ;
                textEdit4.Text = "1";
            }

        }
        
        private void textEdit7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit1.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit3.Focus();

            }
        }

        private void AddingNewComponent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                simpleButton2.Focus();

            }
        }

        private void textEdit4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit7.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit3.Focus();

            }
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {

            if (toggleSwitch1.IsOn == true)
            {
                comboBox1.Enabled = true;
                isItWithOtherCom = "1";
            }
            else if (toggleSwitch1.IsOn == false)
            {
                comboBox1.Enabled = false;
                isItWithOtherCom = "0";

            }
        }
    }
}