using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraVerticalGrid;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EslamApp
{
    public partial class Items : DevExpress.XtraEditors.XtraForm
    {
        public Items()
        {
            InitializeComponent();
        }

        public static int x1 = 0;
        public static int x2 = 0;
        private string index1 = "";
        private string index2 = "";



        private void Items_Load(object sender, EventArgs e)
        {
            dateEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit3.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit4.Text = DateTime.Today.ToString("yyyy-MM-dd");

            string selectTarget = "Select IID as 'رقم المادة' , IName as 'اسم المادة' from ItemsWithOutCom";
            sqliteHelper.lookupEditFill(selectTarget, "اسم المادة", "رقم المادة", this.lookUpEdit1);

            selectTarget = "Select IWCID as 'رقم المادة' , IWCName as 'اسم المادة' from ItemWithCom";
            sqliteHelper.lookupEditFill(selectTarget, "اسم المادة", "رقم المادة", this.lookUpEdit2);


            string selectString = "select IID as 'رقم المادة', IName as 'اسم المادة',IUnite as 'الوحدة',IQuantity as 'الكمية',printf('%,d',IPrice)  as 'سعر المادة',printf('%,d',ICost) as 'التكلفة',printf('%,d',IAddingCost) as 'القيمة المضافة',printf('%,d',ITotal)  as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote as 'ملاحظات' from ItemsWithOutCom";
            sqliteHelper.select(selectString, dataGridView1);




            selectString = "select IWCID as 'رقم المادة', IWCName as 'اسم المادة',printf('%,d',IWCOtherCost) as 'تكاليف اخرى',printf('%,d',IWCAddingCost) as 'القيمة المضافة',printf('%,d',IWCPrice) as 'السعر',IWCAddingDate as 'تاريخ الاضافة', IWCNote as 'ملاحظات'  from ItemWithCom";
            sqliteHelper.select(selectString, dataGridView2);



            sqliteHelper.EnableStyle2(dataGridView1);
            sqliteHelper.EnableStyle2(dataGridView2);


           selectString = "select coalesce(sum(IQuantity*ICost),0) from ItemsWithOutCom";

            toolStripStatusLabel2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            toolStripStatusLabel5.Text = sqliteHelper.selectWithReturn("select Count(IID) from ItemsWithOutCom");

            toolStripStatusLabel8.Text = sqliteHelper.selectWithReturn("select Count(IWCID) from ItemWithCom");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            string selectString = "select IID as 'رقم المادة', IName as 'اسم المادة',IUnite as 'الوحدة',IQuantity as 'الكمية', printf('%,d',IPrice) as 'سعر المادة',printf('%,d',ICost) as 'التكلفة',printf('%,d',IAddingCost) as 'القيمة المضافة',printf('%,d',ITotal) as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote as 'الملاحظات' from ItemsWithOutCom where IAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "'";
            sqliteHelper.select(selectString, dataGridView1);

        }

        private void Items_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.x4 = 0;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (x1 == 0)
            {
                x1 = 1;
                AddingNewItem f1 = new AddingNewItem();
                f1.Show();

            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            dateEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Today.ToString("yyyy-MM-dd");

            string selectTarget = "Select IID as 'رقم المادة' , IName as 'اسم المادة' from ItemsWithOutCom";
            sqliteHelper.lookupEditFill(selectTarget, "اسم المادة", "رقم المادة", this.lookUpEdit1);


            string selectString = "select IID as 'رقم المادة', IName as 'اسم المادة',IUnite as 'الوحدة',IQuantity as 'الكمية',printf('%,d',IPrice)  as 'سعر المادة',printf('%,d',ICost) as 'التكلفة',printf('%,d',IAddingCost) as 'القيمة المضافة',printf('%,d',ITotal) as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote as 'ملاحظات' from ItemsWithOutCom";
            sqliteHelper.select(selectString, dataGridView1);


            selectString = "select coalesce(sum(IQuantity*ICost),0) from ItemsWithOutCom";

            toolStripStatusLabel2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            toolStripStatusLabel5.Text = sqliteHelper.selectWithReturn("select Count(IID) from ItemsWithOutCom");





        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
              if (x2 == 0)
              {
                  x2 =1;
                  AddingComToItem f1 = new AddingComToItem();
                  f1.index = this.index2;
                  f1.Show();
              }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            string selectString = "select IID as 'رقم المادة', IName as 'اسم المادة',IUnite as 'الوحدة',IQuantity as 'الكمية' ,printf('%,d',IPrice) as 'سعر المادة',printf('%,d',ICost) as 'التكلفة',printf('%,d',IAddingCost) as 'القيمة المضافة',printf('%,d',ITotal) as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote as 'ملاحظات' from ItemsWithOutCom where IName ='" + lookUpEdit1.Text + "'";
            sqliteHelper.select(selectString, dataGridView1);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse(sqliteHelper.selectWithReturn("select IQuantity from ItemsWithOutCom where IID =" + index1)) == 0)
                {

                    if (MessageBox.Show("هل تريد الحذف بالتأكيد؟", "مدير المواد", MessageBoxButtons.YesNo) ==
                   System.Windows.Forms.DialogResult.Yes)
                    {
                        string deleteString = "delete from ItemsWithOutCom where IID =" + index1;
                        sqliteHelper.delete(deleteString, 1);

                        string resetString = "DBCC CHECKIDENT ('IID', reseed, (select max(IID) from ItemsWithOutCom))";
                        sqliteHelper.resetPK(resetString, 0);


                        string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                         "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم يحذف بيانات مادة في المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                        sqliteHelper.insert(InsString, 0);

                        string selectString = "select IID as 'رقم المادة', IName as 'اسم المادة',IUnite as 'الوحدة',IQuantity as 'الكمية' ,printf('%,d',IPrice) as 'سعر المادة',printf('%,d',ICost) as 'التكلفة',printf('%,d',IAddingCost) as 'القيمة المضافة',printf('%,d',ITotal) as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote as 'ملاحظات' from ItemsWithOutCom";
                        sqliteHelper.select(selectString, dataGridView1);



                    }
                }
                else
                {
                    MessageBox.Show("لا يمكن الحذف يوجد كمية بالفعل");
                }
            }
            catch
            {

            }
        }

       

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            dateEdit3.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit4.Text = DateTime.Today.ToString("yyyy-MM-dd");

            string selectTarget = "Select IWCID as 'رقم المادة' , IWCName as 'اسم المادة' from ItemWithCom";
            sqliteHelper.lookupEditFill(selectTarget, "اسم المادة", "رقم المادة", this.lookUpEdit2);

            string selectString = "select IWCID as 'رقم المادة', IWCName as 'اسم المادة',printf('%,d',IWCOtherCost) as 'تكاليف اخرى',printf('%,d',IWCAddingCost) as 'القيمة المضافة',printf('%,d',IWCPrice) as 'السعر',IWCAddingDate as 'تاريخ الاضافة', IWCNote as 'ملاحظات'  from ItemWithCom";
            sqliteHelper.select(selectString, dataGridView2);

            toolStripStatusLabel8.Text = sqliteHelper.selectWithReturn("select Count(IWCID) from ItemWithCom");


        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            string selectString = "select IWCID as 'رقم المادة', IWCName as 'اسم المادة',printf('%,d',IWCOtherCost) as 'تكاليف اخرى',printf('%,d',IWCAddingCost) as 'القيمة المضافة',printf('%,d',IWCPrice) as 'السعر',IWCAddingDate as 'تاريخ الاضافة', IWCNote as 'ملاحظات'  from ItemWithCom where IWCName ='"+lookUpEdit2.Text+"'";
            sqliteHelper.select(selectString, dataGridView2);
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {

            
                if (MessageBox.Show("هل تريد الحفظ بالتأكيد؟", "مدير المواد", MessageBoxButtons.YesNo) ==
               System.Windows.Forms.DialogResult.Yes)
                {
                    string deleteString = "delete from ItemWithCom where IWCID ='" + index2 + "'";
                    sqliteHelper.delete(deleteString, 1);

                    string resetString = "DBCC CHECKIDENT ('IWCID', reseed, (select max(IWCID) from ItemWithCom))";
                    sqliteHelper.resetPK(resetString, 0);

                 deleteString = "delete from ItemMadeOf where IMOID = (select IMOID from ItemMadeOf where IWCID=" + index2 + ")";
                    sqliteHelper.delete(deleteString, 0);

                 resetString = "DBCC CHECKIDENT ('IMOID', reseed, (select max(IMOID) from ItemMadeOf))";
                sqliteHelper.resetPK(resetString, 0);

                string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                     "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم يحذف بيانات مادة في المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                    sqliteHelper.insert(InsString, 0);

                string selectString = "select IWCID as 'رقم المادة', IWCName as 'اسم المادة',printf('%,d',IWCOtherCost) as 'تكاليف اخرى',printf('%,d',IWCAddingCost) as 'القيمة المضافة',printf('%,d',IWCPrice) as 'السعر',IWCAddingDate as 'تاريخ الاضافة', IWCNote as 'ملاحظات'  from ItemWithCom";
                sqliteHelper.select(selectString, dataGridView2);


               
                }


        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            string selectString = "select IWCID as 'رقم المادة', IWCName as 'اسم المادة',printf('%,d',IWCOtherCost) as 'تكاليف اخرى',printf('%,d',IWCAddingCost) as 'القيمة المضافة',printf('%,d',IWCPrice) as 'السعر',IWCAddingDate as 'تاريخ الاضافة', IWCNote as 'ملاحظات'  from ItemWithCom where IWCAddingDate ='" + dateEdit3.Text + "' and '" + dateEdit4.Text + "'";
            sqliteHelper.select(selectString, dataGridView2);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try {
                if (MessageBox.Show("هل تريد التعديل بالتأكيد؟", "مدير المستودع", MessageBoxButtons.YesNo) ==
                 System.Windows.Forms.DialogResult.Yes)
                {

                    string itemPrice = (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString())).ToString();
                    string updateString = "update ItemsWithOutCom set IName = '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "' , IPrice = " + itemPrice + ",ICost =" + double.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) + ",IAddingCost =" + double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()) + " , ITotal = " + (double.Parse(itemPrice) * double.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString())).ToString() + " where IID =" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    sqliteHelper.upDate(updateString, 1);


                    string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                     "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بتعديل بيانات مادة في المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                    sqliteHelper.insert(InsString, 0);

                    string selectString = "select IID as 'رقم المادة', IName as 'اسم المادة',IUnite as 'الوحدة',IQuantity as 'الكمية',printf('%,d',IPrice)  as 'سعر المادة',printf('%,d',ICost) as 'التكلفة',printf('%,d',IAddingCost) as 'القيمة المضافة',printf('%,d',ITotal) as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة',INote as 'ملاحظات' from ItemsWithOutCom";
                    sqliteHelper.select(selectString, dataGridView1);

                    try
                    {
                        int icolumn = e.ColumnIndex;
                        int irow = e.RowIndex;
                        if (icolumn == dataGridView1.Columns.Count - 1)
                        {
                            //  dataGridView1.Rows.Add();
                            //  dataGridView1.CurrentCell = dataGridView1[0, irow + 1];
                        }
                        else
                        {
                            dataGridView1.CurrentCell = dataGridView1.Rows[irow + 1].Cells[icolumn];
                        }
                    }
                    catch
                    {

                    }

                }


            }
            catch { }
            
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل تريد التعديل بالتأكيد؟", "مدير المستودع", MessageBoxButtons.YesNo) ==
                 System.Windows.Forms.DialogResult.Yes)
                {
                    string updateString = "update ItemWithCom set IWCName = '" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "' where IWCID =" + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    sqliteHelper.upDate(updateString, 1);


                    string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                     "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بتعديل بيانات مادة في المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                    sqliteHelper.insert(InsString, 0);

                    string selectString = "select IWCID as 'رقم المادة', IWCName as 'اسم المادة',printf('%,d',IWCOtherCost) as 'تكاليف اخرى',printf('%,d',IWCAddingCost) as 'القيمة المضافة',printf('%,d',IWCPrice) as 'السعر',IWCAddingDate as 'تاريخ الاضافة', IWCNote as 'ملاحظات'  from ItemWithCom";
                    sqliteHelper.select(selectString, dataGridView2);


                    try
                    {
                        int icolumn = e.ColumnIndex;
                        int irow = e.RowIndex;
                        if (icolumn == dataGridView1.Columns.Count - 1)
                        {
                            //   dataGridView1.Rows.Add();
                            //   dataGridView1.CurrentCell = dataGridView1[0, irow + 1];
                        }
                        else
                        {
                            dataGridView1.CurrentCell = dataGridView1.Rows[irow + 1].Cells[icolumn];
                        }
                    }
                    catch
                    {

                    }

                }
            }
            catch { }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index2 = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

            }
            catch { }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try {
            index1 = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch { }

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                simpleButton5.PerformClick();

            }
            if (e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                simpleButton2.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                e.Handled = true;
                simpleButton5.PerformClick();
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                simpleButton10.PerformClick();

            }
            if (e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                simpleButton2.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                e.Handled = true;
                simpleButton8.PerformClick();
            }
            if (e.KeyCode == Keys.F3)
            {
                e.Handled = true;
                simpleButton7.PerformClick();
            }


        }
    }
}