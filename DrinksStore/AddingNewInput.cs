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
    public partial class AddingNewInput : DevExpress.XtraEditors.XtraForm
    {
        public AddingNewInput()
        {
            InitializeComponent();
        }

        private void AddingNewInput_Load(object sender, EventArgs e)
        {
            textEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            comboBox1.Items.Add("مادة ثابتة");
            comboBox1.Items.Add("مكونات");

        }

        private void AddingNewInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            Box.x1 = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "مادة ثابتة")
            {

                comboBox2.Items.Clear();
                string selectString = "select IName as 'اسم المنتج' from ItemsWithOutCom ";
                sqliteHelper.select(selectString,comboBox2);
            }
            else if(comboBox1.Text =="مكونات")
            {
                comboBox2.Items.Clear();
                string selectString = "select CName as 'اسم المنتج' from Components";
                sqliteHelper.select(selectString, comboBox2);

            }
        }

       

       

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
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

                        double d = double.Parse(textEdit5.Text);
                        string insertString = "insert into Box (BID,BType,BItemType,IID ,BItem,BUnite,BPrice,BQuantity,BTotal,BProfit,BAddingDate,BNote) values ((select coalesce(max(BID),0)+1 from Box),'ادخال','" + "مادة ثابتة" + "',(select IID from ItemsWithOutCom where IName ='" + comboBox2.Text + "'),'" + comboBox2.Text + "','" + textEdit2.Text + "'," + textEdit4.Text + "," + textEdit5.Text + "," + (Double.Parse(textEdit5.Text) * Double.Parse(textEdit4.Text)).ToString() + ",0,'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + textEdit6.Text + "')";
                        sqliteHelper.insert(insertString, 1);

                        string updatestring = "update ItemsWithOutCom set IQuantity = IQuantity +" + textEdit5.Text + " where IName ='" + comboBox2.Text + "'";
                        sqliteHelper.upDate(updatestring, 0);

                        updatestring = "update ItemsWithOutCom set Itotal = IQuantity * IPrice where IName ='" + comboBox2.Text + "'";
                        sqliteHelper.upDate(updatestring, 0);
                        Box.threadTrigger = 1;


                    }
                    else if (comboBox1.Text == "مكونات")
                    {


                        string insertString = "insert into BoxForComponents (BFCID,BFCType,BFCName,BFCCID,IWCID ,IWCName,BFCQuantity,BFCPrice,BFCTotal,BFCAddingDate,BFCNote) values ((select coalesce(max(BFCID),0)+1 from BoxForComponents),'ادخال','" + comboBox2.Text + "',(select CID from Components where CName='"+ comboBox2.Text + "'),'',''," + textEdit5.Text + "," + textEdit4.Text + "," + (Double.Parse(textEdit5.Text) * Double.Parse(textEdit4.Text)).ToString() + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + textEdit6.Text + "')";
                        sqliteHelper.insert(insertString, 1);

                        string updatestring = "update Components set CQuantity = CQuantity +" + textEdit5.Text + " where CName ='" + comboBox2.Text + "'";
                        sqliteHelper.upDate(updatestring, 0);


                        updatestring = "update Components set CTotal = CQuantity * CPrice where CName ='" + comboBox2.Text + "'";
                        sqliteHelper.upDate(updatestring, 0);
                        Box.threadTrigger = 2;

                    }
                }
                catch
                {
                    MessageBox.Show("الاسعار يجب ان تكون رقماً");
                }



            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "مادة ثابتة")
            {
                string selectString = "select IUnite from ItemsWithOutCom where IName ='" + comboBox2.Text + "'";
                textEdit2.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IPrice from ItemsWithOutCom where IName ='" + comboBox2.Text + "'";

                textEdit4.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IQuantity from ItemsWithOutCom where IName ='" + comboBox2.Text + "'";

                textEdit3.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IID as 'رقم المادة', IName as 'اسم المادة',IUnite as 'الوحدة',IQuantity as 'الكمية', IPrice as 'سعر المادة',ICost as 'التكلفة',IAddingCost as 'القيمة المضافة',ITotal as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote as 'ملاحظات' from ItemsWithOutCom where IName ='" + comboBox2.Text + "'";
                sqliteHelper.select(selectString, dataGridView1);
                sqliteHelper.EnableStyle(dataGridView1);
                

            }
            else if (comboBox1.Text == "مكونات")
            {
                string selectString = "select CUnite from Components where CName ='" + comboBox2.Text + "'";
                textEdit2.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select CPrice from Components where CName ='" + comboBox2.Text + "'";
                textEdit4.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select CQuantity from Components where CName ='" + comboBox2.Text + "'";

                textEdit3.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select CID as 'رقم المكون', CName as 'المكون',CQuantity as 'الكمية',CUnite as 'الوحدة', CPrice as 'سعر المكون الواحد',CTotal as 'السعر الكلي',CAddingDate as 'تاريخ الاضافة',CNote as 'الملاحظات' from Components where CName = '" + comboBox2.Text + "'";
                sqliteHelper.select(selectString, dataGridView1);
                sqliteHelper.EnableStyle(dataGridView1);
              

            }
        }

        private void textEdit5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
        }
    }
}