using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Microsoft.Office.Interop.Word;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace EslamApp
{
    public partial class AddingComToItem : DevExpress.XtraEditors.XtraForm
    {
        public AddingComToItem()
        {
            InitializeComponent();
        }





       public  string index = "";
        public string indexForDelete = "";


        private void AddingComToItem_Load(object sender, EventArgs e)
        {
            textEdit3.Text = index;
            textEdit1.Text = sqliteHelper.selectWithReturn("select IWCName from ItemWithCom where IWCID =" + index);
            textEdit2.Text = sqliteHelper.selectWithReturn("select IWCPrice from ItemWithCom where IWCID =" + index);
            textEdit8.Text = sqliteHelper.selectWithReturn("select IWCOtherCost from ItemWithCom where IWCID =" + index);
            textEdit7.Text = sqliteHelper.selectWithReturn("select IWCAddingCost from ItemWithCom where IWCID =" + index);


            string selectString = "select CID as 'رقم المكون', CName as 'اسم المكون',CQuantity as 'الكمية',CUnite as 'الوحدة', CPrice as 'سعر المكون الواحد',CTotal as 'السعر الكلي' from Components";
            sqliteHelper.lookupEditFill(selectString, "اسم المكون", "رقم المكون", lookUpEdit1);

            selectString = "select IMOID 'رقم المكون',IWCID as 'رقم المادة',IName as 'اسم المادة',IMadeOf as 'اسم المكون',IQuantity as 'الكمية',IPrice as 'السعر',ITotal as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote from ItemMadeOf where IWCID =" + index + "";

            sqliteHelper.select(selectString, dataGridView1);
            sqliteHelper.EnableStyle(dataGridView1);
           


        }



        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string selectString = "select CID as 'رقم المكون', CName as 'اسم المكون',CQuantity as 'الكمية',CUnite as 'الوحدة', CPrice as 'سعر المكون الواحد',CTotal as 'السعر الكلي' from Components";
            sqliteHelper.lookupEditFill(selectString, "اسم المكون", "رقم المكون", lookUpEdit1);
           
        }

        private void AddingComToItem_FormClosing(object sender, FormClosingEventArgs e)
        {

            string selectString = "select IWCPrice from ItemWithCom where IWCID ="+index;
            double IPrice = double.Parse(sqliteHelper.selectWithReturn(selectString));
            if(double.Parse(textEdit2.Text) != IPrice)
            {
                MessageBox.Show("لا يمكن الخروج من بطاقة المادة ... لم يتم حفظ سعر المادة الجديد");
                e.Cancel = true;
            }
            else
            {
                Items.x2 = 0;
            }


        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            string selectString = "select CPrice from Components where CName = '" + lookUpEdit1.Text + "'";
            string cprice = sqliteHelper.selectWithReturn(selectString);

            string insertString = "insert into ItemMadeOf(IMOID,IWCID,IMOCCID,IName,IMadeOf,IQuantity,IPrice,ITotal,IAddingDate,INote) values ((select coalesce(max(IMOID), 0) + 1 from ItemMadeOf)," + textEdit3.Text + ",(select CID from Components where CName ='"+lookUpEdit1.Text+"'),'" + textEdit1.Text + "','" + lookUpEdit1.Text + "','" + textEdit4.Text + "', " + cprice + "," + textEdit5.Text + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','')";
            sqliteHelper.insert(insertString, 0);

            string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
               "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بإضافة مكونات جديدة الى مادة في المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
            sqliteHelper.insert(InsString, 0);

        //    threadTrigger = 0;
             selectString = "select CID as 'رقم المكون', CName as 'اسم المكون',CQuantity as 'الكمية',CUnite as 'الوحدة', CPrice as 'سعر المكون الواحد',CTotal as 'السعر الكلي' from Components";
            sqliteHelper.lookupEditFill(selectString, "اسم المكون", "رقم المكون", lookUpEdit1);

            selectString = "select IMOID 'رقم المكون',IWCID as 'رقم المادة',IName as 'اسم المادة',IMadeOf as 'اسم المكون',IQuantity as 'الكمية',IPrice as 'السعر',ITotal as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote from ItemMadeOf where IWCID =" + textEdit3.Text + "";

            sqliteHelper.select(selectString,dataGridView1);

            textEdit8.Text = "";
            textEdit8.Text = sqliteHelper.selectWithReturn("select coalesce(sum(IPrice*IQuantity),0) from ItemMadeOf where IWCID =" + textEdit3.Text);



        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string selectString = "select IWCPrice from ItemWithCom where IWCID =" + index;
            double IPrice = double.Parse(sqliteHelper.selectWithReturn(selectString));
            if (double.Parse(textEdit2.Text) != IPrice)
            {
                MessageBox.Show("لا يمكن الخروج من بطاقة المادة ... لم يتم حفظ سعر المادة الجديد");
            }
            else
            {
                this.Close();

            }

        }







      
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الحفظ بالتأكيد؟", "مدير المواد", MessageBoxButtons.YesNo) ==
          System.Windows.Forms.DialogResult.Yes)
            {

                string comCost = sqliteHelper.selectWithReturn("select coalesce(sum(IPrice*IQuantity),0) from ItemMadeOf where IWCID =" + textEdit3.Text);

                string updateString = "update ItemWithCom set  IWCName = '" + textEdit1.Text + "', IWCPrice =" + textEdit2.Text + ",IWCOtherCost =" + comCost + ",IWCAddingCost =" + textEdit7.Text + ",IWCPrice =" + (double.Parse(textEdit7.Text)+ double.Parse(textEdit8.Text)).ToString() +" where  IWCID = " + textEdit3.Text;
                sqliteHelper.upDate(updateString, 1);
            }

        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الحفظ بالتأكيد؟", "مدير المواد", MessageBoxButtons.YesNo) ==
               System.Windows.Forms.DialogResult.Yes)
            {
                string deleteString = "delete from ItemMadeOf where IMOID ="+indexForDelete;
                sqliteHelper.delete(deleteString, 1);

                string resetString = "DBCC CHECKIDENT ('IID', reseed, (select max(IID) from ItemsWithOutCom))";
                sqliteHelper.resetPK(resetString, 0);


                string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                 "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم يحذف بيانات مادة في المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";

                sqliteHelper.insert(InsString, 0);


                string selectString = "select IMOID 'رقم المكون',IWCID as 'رقم المادة',IName as 'اسم المادة',IMadeOf as 'اسم المكون',IQuantity as 'الكمية',IPrice as 'السعر',ITotal as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote from ItemMadeOf where IWCID =" + index;

                sqliteHelper.select(selectString, dataGridView1);

                textEdit8.Text = "";
                textEdit8.Text = sqliteHelper.selectWithReturn("select coalesce(sum(IPrice*IQuantity),0) from ItemMadeOf where IWCID =" + textEdit3.Text);


            }
        }

      

        private void AddingComToItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            /* if (e.KeyCode == Keys.Enter)
           {
               e.Handled = true;*/
            try
            {
                string selectstring = "select CPrice*" + Double.Parse(textEdit4.Text) + " From Components where CName ='" + lookUpEdit1.Text + "'";
                textEdit5.Text = sqliteHelper.selectWithReturn(selectstring);

            }
            catch
            {

            }
            // }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                indexForDelete = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch
            {

            }
        }

      

        private void textEdit4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton2.PerformClick();

            }
           
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                simpleButton4.PerformClick();

            }
        }

        private void textEdit8_EditValueChanged(object sender, EventArgs e)
        {
            try
            {


                if (textEdit8.Text == "")
                {
                    textEdit8.Text = "0";
                    textEdit2.Text = (double.Parse(textEdit8.Text) + double.Parse(textEdit7.Text)).ToString();
                }
                else if (textEdit7.Text == "")
                {
                    textEdit7.Text = "0";
                    textEdit2.Text = (double.Parse(textEdit8.Text) + double.Parse(textEdit7.Text)).ToString();
                }
                else
                {
                    textEdit2.Text = (double.Parse(textEdit8.Text) + double.Parse(textEdit7.Text)).ToString();
                }
            }
            catch { }
        }

        private void textEdit7_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (textEdit8.Text == "")
                {
                    textEdit8.Text = "0";
                    textEdit2.Text = (double.Parse(textEdit8.Text) + double.Parse(textEdit7.Text)).ToString();
                }
                else if (textEdit7.Text == "")
                {
                    textEdit7.Text = "0";
                    textEdit2.Text = (double.Parse(textEdit8.Text) + double.Parse(textEdit7.Text)).ToString();
                }
                else
                {
                    textEdit2.Text = (double.Parse(textEdit8.Text) + double.Parse(textEdit7.Text)).ToString();
                }
            }
            catch
            {

            }
           
        }

        private void lookUpEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                e.Handled = true;
                textEdit4.Focus();

            }
        }
    }
}