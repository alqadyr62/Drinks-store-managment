using DevExpress.XtraEditors;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EslamApp
{
    public partial class AddingNewItem : DevExpress.XtraEditors.XtraForm
    {
        public AddingNewItem()
        {
            InitializeComponent();
            textEdit2.Select();
        }

        int itemType = 0;

      

        private void AddingNewItem_Load(object sender, EventArgs e)
        {
            textEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");

            string selectTarget = "select UID as 'رقم الوحدة', UName as 'اسم الوحدة' from Units";
            sqliteHelper.lookupEditFill(selectTarget,"اسم الوحدة","رقم الوحدة",lookUpEdit1);

            comboBox1.Items.Add("مادة مكونات");
            comboBox1.Items.Add("مادة ثابتة");

            textEdit5.Enabled = false;
            textEdit3.Enabled = false;

        }

        private void AddingNewItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            Items.x1 = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("هل تريد الحفظ بالتأكيد؟", "مدير المواد", MessageBoxButtons.YesNo) ==
            System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                   
                    if (comboBox1.Text == "مادة ثابتة")
                    {
                        double d = double.Parse(textEdit3.Text);
                        double cd = double.Parse(textEdit4.Text);
                        double ds = double.Parse(textEdit5.Text);

                        if (textEdit2.Text != "" && textEdit5.Text != "" && textEdit3.Text != "" && textEdit5.Text != "")
                        {
                            string insertString = "insert into ItemsWithOutCom (IID,IName,IUnite,IQuantity,ICost,IAddingCost,IPrice,ITotal,IAddingDate,INote) values ((select coalesce(max(IID),0)+1 from ItemsWithOutCom),'" + textEdit2.Text + "','" + lookUpEdit1.Text + "',0," + textEdit5.Text + "," + textEdit3.Text + "," + textEdit4.Text + ",0,'" + textEdit1.Text + "','" + textEdit6.Text + "')";
                            sqliteHelper.insert(insertString, 1);

                            string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                     "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بإضافة مادة جديدة الى المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                            sqliteHelper.insert(InsString, 0);
                        }
                        else
                        {
                            MessageBox.Show("يجب ادخال كامل البيانات الضرورية المعلمة بالنجمة");

                        }
                    }
                    else if (comboBox1.Text == "مادة مكونات")
                    {
                        if (textEdit2.Text != "" && textEdit4.Text != "" )
                        {
                            double d = double.Parse(textEdit4.Text);
                            string insertString = "insert into ItemWithCom (IWCID,IWCName,IWCUnite,IWCOtherCost,IWCPrice,IWCAddingDate,IWCNote) values ((select coalesce(max(IWCID),0)+1 from ItemWithCom),'" + textEdit2.Text + "','" + lookUpEdit1.Text + "',0," + textEdit4.Text + ",'" + textEdit1.Text + "','" + textEdit6.Text + "')";
                         
                            sqliteHelper.insert(insertString, 1);

                            string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                     "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بإضافة مادة جديدة الى المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                            sqliteHelper.insert(InsString, 0);
                        }
                        else
                        {
                            MessageBox.Show("يجب ادخال كامل البيانات الضرورية المعلمة بالنجمة");
                        }

                    }
                }
                catch { MessageBox.Show("الاسعار يجب ان تكون رقماً");  }
            }
                
        }
            
        

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "مادة مكونات")
            {
                textEdit5.Enabled = false;
                textEdit3.Enabled = false;
                textEdit4.Focus();
            }
            else if(comboBox1.Text == "مادة ثابتة")
            {
                textEdit5.Enabled = true;
                textEdit3.Enabled = true;
                textEdit5.Focus();

            }
        }

        private void textEdit4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit3.Focus();

            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit6.Focus();

            }
        }

        private void textEdit5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit2.Focus();

            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit3.Focus();

            }
        }

        

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit6.Focus();

            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit2.Focus();

            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit1.Focus();

            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                lookUpEdit1.Focus();

            }
        }

        private void textEdit3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit5.Focus();

            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit4.Focus();

            }
        }

        private void textEdit6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit4.Focus();

            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit1.Focus();

            }
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (textEdit5.Text == "")
                {
                    textEdit5.Text = "0";
                    textEdit4.Text = (Double.Parse(textEdit5.Text) + Double.Parse(textEdit3.Text)).ToString();

                }
                else if (textEdit3.Text == "")
                {
                    textEdit3.Text = "0";
                    textEdit4.Text = (Double.Parse(textEdit5.Text) + Double.Parse(textEdit3.Text)).ToString();

                }
                else
                {
                    textEdit4.Text = (Double.Parse(textEdit5.Text) + Double.Parse(textEdit3.Text)).ToString();
                }
            }
            catch (Exception ex) { }
            
        }
    }
}