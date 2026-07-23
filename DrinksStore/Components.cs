using DevExpress.Accessibility;
using DevExpress.Utils.MVVM;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Columns;
using Microsoft.Office.Interop.Word;
using myClinic;
using System;
using System.Data.SQLite;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace EslamApp
{
    public partial class Components : DevExpress.XtraEditors.XtraForm
    {
        public Components()
        {
            InitializeComponent();
        }

        public static int x1 = 0;
        string index = "";



        private void Components_Load(object sender, EventArgs e)
        {

          

            dateEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Today.ToString("yyyy-MM-dd");

            string selectTarget = "Select CID as 'رقم المكون' , CName as 'اسم المكون' from Components";
            sqliteHelper.lookupEditFill(selectTarget, "اسم المكون", "رقم المكون", this.lookUpEdit1);
            try {
                string selectString = "select CID as 'رقم المكون', CName as 'اسم المكون',CQuantity as 'الكمية',CUnite as 'الوحدة',printf('%,d', CDivideOn) as 'مقسمة على',(select iif(USmall = 1 , (CQuantity/CDivideOn) , CQuantity) from Units where Units.UName = Components.CUnite)  as 'وحدات كبيرة', printf('%,d',CPrice) as 'سعر المكون الواحد',printf('%,d',CTotal) as 'السعر الكلي',CAddingDate as 'تاريخ الاضافة',CNote as 'ملاحظات' from Components";

                sqliteHelper.select(selectString, this.dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            sqliteHelper.EnableStyle2(dataGridView1);

            string sString= "select coalesce(sum(CTotal),0) from Components";

            toolStripStatusLabel2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(sString)));


            toolStripStatusLabel5.Text = sqliteHelper.selectWithReturn("select count(CID) from Components");

        }

        private void Components_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.x3 = 0;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            if (x1 == 0)
            {
                x1 = 1;
                AddingNewComponent f1 = new AddingNewComponent();
                f1.Show();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string selectString = "select CID as 'رقم المكون', CName as 'اسم المكون',CQuantity as 'الكمية',CUnite as 'الوحدة',printf('%,d', CDivideOn) as 'مقسمة على',(select iif(USmall = 1 , (CQuantity/CDivideOn) , CQuantity) from Units where Units.UName = Components.CUnite)  as 'وحدات كبيرة', printf('%,d',CPrice) as 'سعر المكون الواحد',printf('%,d',CTotal) as 'السعر الكلي',CAddingDate as 'تاريخ الاضافة',CNote as 'ملاحظات' from Components" +
               " where CAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "'";

                sqliteHelper.select(selectString, this.dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string selectString = "select CID as 'رقم المكون', CName as 'اسم المكون',CQuantity as 'الكمية',CUnite as 'الوحدة',printf('%,d', CDivideOn) as 'مقسمة على' ,(select iif(USmall = 1 , (CQuantity/ CDivideOn) , CQuantity) from Units where Units.UName = Components.CUnite)  as 'وحدات كبيرة', printf('%,d',CPrice) as 'سعر المكون الواحد',printf('%,d',CTotal) as 'السعر الكلي',CAddingDate as 'تاريخ الاضافة',CNote as 'ملاحظات' from Components where CName ='" + lookUpEdit1.Text + "'";

                sqliteHelper.select(selectString, dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

     

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {


                if (!sqliteHelper.isFound("select IMOCCID from ItemMadeOf where IMOCCID =" + index))
                {
                    string quantity = sqliteHelper.selectWithReturn("select CQuantity from Components where CID =" + index);
                    if (Double.Parse(quantity) == 0)
                    {
                        if (MessageBox.Show("هل تريد الحذف بالتأكيد؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
                         System.Windows.Forms.DialogResult.Yes)
                        {
                            string deleteString = "delete from Components where CID ='" + index + "'";
                            sqliteHelper.delete(deleteString, 1);

                            string resetString = "DBCC CHECKIDENT ('CID', reseed, (select max(CID) from Components))";
                            sqliteHelper.resetPK(resetString, 0);


                            string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                             "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم يحذف بيانات مكون في المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                            sqliteHelper.insert(InsString, 0);


                            try
                            {
                                string selectString = "select CID as 'رقم المكون', CName as 'اسم المكون',CQuantity as 'الكمية',CUnite as 'الوحدة',printf('%,d', CDivideOn) as 'مقسمة على' ,(select iif(USmall = 1 , (CQuantity/ CDivideOn) , CQuantity) from Units where Units.UName = Components.CUnite)  as 'وحدات كبيرة', printf('%,d',CPrice) as 'سعر المكون الواحد',printf('%,d',CTotal) as 'السعر الكلي',CAddingDate as 'تاريخ الاضافة',CNote as 'ملاحظات' from Components ";
                                sqliteHelper.select(selectString, dataGridView1);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message); }


                        }
                    }
                    else
                    {
                        MessageBox.Show("لا يمكن الحذف هناك كمية باقية بالفعل");
                    }
                }
                else
                {
                    MessageBox.Show(" يجب حذف المكون من المادة ...المكون موجودة ضمن مكونات مادة");
                }
            }
            catch
            {

            }

        }


        private void simpleButton6_Click(object sender, EventArgs e)
        {
            dateEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Today.ToString("yyyy-MM-dd");

            string selectTarget = "Select CID as 'رقم المكون' , CName as 'اسم المكون' from Components";
            sqliteHelper.lookupEditFill(selectTarget, "اسم المكون", "رقم المكون", this.lookUpEdit1);
            try
            {
                string selectString = "select CID as 'رقم المكون', CName as 'اسم المكون',CQuantity as 'الكمية',CUnite as 'الوحدة',printf('%,d', CDivideOn) as 'مقسمة على',(select iif(USmall = 1 , (CQuantity/CDivideOn) , CQuantity) from Units where Units.UName = Components.CUnite)  as 'وحدات كبيرة', printf('%,d',CPrice) as 'سعر المكون الواحد',printf('%,d',CTotal) as 'السعر الكلي',CAddingDate as 'تاريخ الاضافة',CNote as 'ملاحظات' from Components";
                sqliteHelper.select(selectString, dataGridView1);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            string sString = "select coalesce(sum(CTotal),0) from Components";

            toolStripStatusLabel2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(sString)));


            toolStripStatusLabel5.Text = sqliteHelper.selectWithReturn("select count(CID) from Components");


            //gridView();

        }

        private void dateEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
        }

        private void dateEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                simpleButton1.PerformClick();

            }
        }

        

        

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch
            {

            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            System.Data.DataTable dt = new System.Data.DataTable();

            if (MessageBox.Show("هل تريد التعديل بالتأكيد؟", "مدير المكونات", MessageBoxButtons.YesNo) ==
             System.Windows.Forms.DialogResult.Yes)
            {
                string updateString = "update Components set CName = '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "', CPrice = " + double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()) + ", CTotal = " + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) + ", CNote = '" + dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString() + "' where CID =" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                sqliteHelper.upDate(updateString, 1);

                updateString = "update ItemMadeOf set IPrice ="+ double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()) + ", ITotal = IQuantity * " + double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()) + " where IMOCCID = " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                sqliteHelper.upDate(updateString,0);

                string selectString = "SELECT IWCID from ItemMadeOf where IMOCCID = "+ dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = sqliteHelper.loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(selectString, con);
                SQLiteDataAdapter ada = new SQLiteDataAdapter();
                ada.SelectCommand = com;
                con.DefaultTimeout = 5000;
                con.Open();
                ada.Fill(dt);

                for (int i = 0; i< dt.Rows.Count;i++)
                {
                    updateString = "update ItemWithCom set IWCOtherCost = (select sum(ITotal) from ItemMadeOf where IWCID = " + dt.Rows[i][0].ToString() + "),IWCPrice =(select sum(ITotal) from ItemMadeOf where IWCID = " + dt.Rows[i][0].ToString()+ ")+ IWCAddingCost  where IWCID ="+ dt.Rows[i][0].ToString();

                    sqliteHelper.upDate(updateString, 0);

                }


                /* updateString = "update ItemWithCom set IWCPrice = (IWCOtherCost + IWCAddingCost) where IWCID = (select IWCID from ItemMadeOf where IMOCCID = " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + ")";

                 sqliteHelper.upDate(updateString, 0);*/

                string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                 "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بتعديل مكون" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                sqliteHelper.insert(InsString, 0);
                try
                {

                     selectString = "select CID as 'رقم المكون', CName as 'اسم المكون',CQuantity as 'الكمية',CUnite as 'الوحدة',printf('%,d', CDivideOn) as 'مقسمة على',(select iif(USmall = 1 , (CQuantity/CDivideOn) , CQuantity) from Units where Units.UName = Components.CUnite)  as 'وحدات كبيرة',printf('%,d', CPrice) as 'سعر المكون الواحد',printf('%,d', CTotal) as 'السعر الكلي',CAddingDate as 'تاريخ الاضافة',CNote as 'ملاحظات' from Components";
                    sqliteHelper.select(selectString, dataGridView1);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

             //   sqliteHelper.EnableStyle2(dataGridView1);
                //   e.SuppressKeyPress = true;
                try
                {
                    int icolumn = e.ColumnIndex;
                    int irow = e.RowIndex;
                    if (icolumn == dataGridView1.Columns.Count - 1)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.CurrentCell = dataGridView1[0, irow + 1];
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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                simpleButton5.PerformClick();

            }
            if(e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                simpleButton3.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                e.Handled = true;
                simpleButton6.PerformClick();
            }
        }

       
    }
}