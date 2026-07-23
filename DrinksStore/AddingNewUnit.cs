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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace EslamApp
{
    public partial class AddingNewUnit : DevExpress.XtraEditors.XtraForm
    {
        public AddingNewUnit()
        {
            InitializeComponent();
        }

        private void AddingNewUnit_Load(object sender, EventArgs e)
        {
            textEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            textEdit2.Focus();
        }

        string isSmall = "0";
        private void simpleButton4_Click(object sender, EventArgs e)
        {

            if(textEdit1.Text != "" && textEdit2.Text != "") {
                if (MessageBox.Show("هل تريد الحفظ بالتأكيد؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
                  System.Windows.Forms.DialogResult.Yes)
                {
                    string insertString = "insert into Units (UID,UName,UAddingDate,UNote,USmall) values ((select coalesce(max(UID),0)+1 from Units),'" + textEdit2.Text + "','" + textEdit1.Text + "','" + textEdit3.Text + "'," + isSmall + ")";
                    sqliteHelper.insert(insertString, 1);

                    string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                       "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بإضافة وحدة جديد الى المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                    sqliteHelper.insert(InsString, 0);
                }
                
            }
            else
            {
                MessageBox.Show("يجب ادخال كامل البيانات الضرورية المعلمة بالنجمة");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddingNewUnit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Units.x = 0;
        }

        private void textEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton4.PerformClick();

            }else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit3.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit1.Focus();

            }
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch1.IsOn == true)
            {
                isSmall = "1";
            }
            else if (toggleSwitch1.IsOn == false)
            {
                isSmall = "0";            
            }
        }

        private void textEdit3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton4.PerformClick();

            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit1.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit2.Focus();

            }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton4.PerformClick();

            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                textEdit2.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                textEdit3.Focus();

            }
        }
    }
}