using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
    public partial class Units : DevExpress.XtraEditors.XtraForm
    {
        public Units()
        {
            InitializeComponent();
        }

        public static int x = 0 ;
        public static int x1 = 0;
        private string index = "";

        private void barStaticItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();
           // string selectString = "SELECT * from Units_All  where تاريخ الاضافة between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "'";
            string selectString = "select UID as 'رقم التسلسل', UName as 'اسم الوحدة',UAddingDate as 'تاريخ الاضافة', UNote as 'ملاحظات' from Units" +
            " where UAddingDate between '"+ dateEdit1.Text +"' and '"+dateEdit2.Text+"'";
            sqliteHelper.select(selectString, dataGridView1);
        }

        private void Units_Load(object sender, EventArgs e)
        {
            dateEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Today.ToString("yyyy-MM-dd");

          //  gridView1.

            string selectTarget = "Select UID as 'رقم الوحدة' , UName as 'اسم الوحدة' from Units";
            sqliteHelper.lookupEditFill(selectTarget,"اسم الوحدة","رقم الوحدة",this.lookUpEdit1);
            string selectString = "select UID as 'رقم التسلسل', UName as 'اسم الوحدة',UAddingDate as 'تاريخ الاضافة', UNote as 'ملاحظات' from Units";
            sqliteHelper.select(selectString, dataGridView1);
            sqliteHelper.EnableStyle2(dataGridView1);

            toolStripStatusLabel2.Text = sqliteHelper.selectWithReturn("select Count(UID) from Units");
        }
    

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
          
                string selectString = "select UID as 'رقم الوحدة', UName as 'اسم الوحدة',UAddingDate as 'تاريخ الاضافة', UNote as 'ملاحظات' from Units"
               +" where UName ='" + lookUpEdit1.Text + "'";
                sqliteHelper.select(selectString, dataGridView1);


            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Units_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.x1 = 0;
        }

        

       

        

      

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل تريد الحذف بالتأكيد؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
                 System.Windows.Forms.DialogResult.Yes)
                {

                    string deleteString = "delete from Units where UID =" + index;
                    sqliteHelper.delete(deleteString, 1);

                    string resetString = "DBCC CHECKIDENT ('UID', reseed, (select max(UID) from Units))";
                    sqliteHelper.resetPK(resetString, 0);


                    string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                     "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم يحذف بيانات وحدة في المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                    sqliteHelper.insert(InsString, 0);

                    string selectString = "select UID as 'رقم الوحدة', UName as 'اسم الوحدة',UAddingDate as 'تاريخ الاضافة', UNote as 'ملاحظات' from Units";
                    sqliteHelper.select(selectString, dataGridView1);


                }
            }
            catch { }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
           dateEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Today.ToString("yyyy-MM-dd");

            string selectTarget = "Select UID as 'رقم الوحدة' , UName as 'اسم الوحدة' from Units";
            sqliteHelper.lookupEditFill(selectTarget, "اسم الوحدة", "رقم الوحدة", this.lookUpEdit1);
            string selectString = "select UID as 'رقم الوحدة', UName as 'اسم الوحدة',UAddingDate as 'تاريخ الاضافة', UNote as 'ملاحظات' from Units";
            sqliteHelper.select(selectString, dataGridView1);

            toolStripStatusLabel2.Text = sqliteHelper.selectWithReturn("select Count(UID) from Units");

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (x == 0)
            {
                x = 1;
                AddingNewUnit f1 = new AddingNewUnit();
                f1.Show();
            }
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

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                e.Handled = true;

                simpleButton5.PerformClick();

            }else if (e.KeyCode == Keys.N)
            {
                e.Handled = true;
                simpleButton3.PerformClick();
                
            }
           
        }

        private void Units_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N)
            {
                e.Handled = true;

                simpleButton3.PerformClick();

            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("هل تريد التعديل بالتأكيد؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
           System.Windows.Forms.DialogResult.Yes)
            {
                string updateString = "update Units set UName = '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "' where UID =" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                sqliteHelper.upDate(updateString, 1);


                string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                 "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم بتعديل بيانات وحدة في المستودع" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                sqliteHelper.insert(InsString, 0);

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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
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
