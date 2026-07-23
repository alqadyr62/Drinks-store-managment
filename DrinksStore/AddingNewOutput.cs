using DevExpress.Data.Mask;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using Microsoft.Office.Interop.Word;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EslamApp
{
    public partial class AddingNewOutput : DevExpress.XtraEditors.XtraForm
    {
        public AddingNewOutput()
        {
            InitializeComponent();
        }

        

        private void AddingNewOutput_Load(object sender, EventArgs e)
        {
            textEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");

            comboBox1.Items.Add("مادة مكونات");
            comboBox1.Items.Add("مادة ثابتة");

            sqliteHelper.EnableStyle(dataGridView1);


        }










        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("هل تريد الحفظ بالتأكيد؟", "مدير المواد", MessageBoxButtons.YesNo) ==
            System.Windows.Forms.DialogResult.Yes)
            {

                if (comboBox1.Text == "مادة ثابتة")
                {
                    if (Double.Parse(sqliteHelper.selectWithReturn("select IQuantity from ItemsWithOutCom where IName ='" + comboBox2.Text + "'")) > 0 && Double.Parse(sqliteHelper.selectWithReturn("select IQuantity from ItemsWithOutCom where IName ='" + comboBox2.Text + "'")) >= Double.Parse(textEdit5.Text))
                    {

                        string cost = sqliteHelper.selectWithReturn("select ICost from ItemsWithOutCom where IName ='" + comboBox2.Text + "'");

                        string subCost = (Double.Parse(cost) * Double.Parse(textEdit5.Text)).ToString();

                        string subfinal = (Double.Parse(textEdit5.Text) * Double.Parse(textEdit4.Text)).ToString();

                        string final = (Double.Parse(subfinal) - Double.Parse(subCost)).ToString();

                        string insertString = "insert into Box(BID,BType,BItemType,IID,BItem,BUnite,BPrice,BQuantity,BTotal,BProfit,BAddingDate,BNote) values ((select coalesce(max(BID),0)+1 from Box),'اخراج','" + "مادة ثابتة" + "'," + "(select IID from ItemsWithOutCom where IName ='" + comboBox2.Text + "'),'" + comboBox2.Text + "','" + textEdit2.Text + "'," + textEdit4.Text + "," + textEdit5.Text + "," + subfinal + "," + final + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + textEdit6.Text + "')";

                        sqliteHelper.insert(insertString, 1);

                        string updateString = "update ItemsWithOutCom set IQuantity = IQuantity -" + textEdit5.Text + " , ITotal = (IQuantity -" + textEdit5.Text + ")*IPrice where IName ='" + comboBox2.Text + "'";

                        sqliteHelper.upDate(updateString, 0);
                        Box.threadTrigger = 1;
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد كمية كافية");
                    }
                }
                else if (comboBox1.Text == "مادة مكونات")
                {
                    int id = int.Parse(sqliteHelper.selectWithReturn("select IWCID from ItemWithCom where IWCName='" + comboBox2.Text + "'"));
                    bool t = sqliteHelper.comOutput(id, double.Parse(textEdit5.Text));
                    if (t)
                    {
                        string selectString = "select IMOCCID from ItemMadeOf where IWCID=" + id;
                        System.Data.DataTable dt = new System.Data.DataTable();

                        SQLiteConnection con = new SQLiteConnection();
                        con.ConnectionString = sqliteHelper.loadConnectionString("MyStore");
                        SQLiteCommand com = new SQLiteCommand(selectString, con);
                        SQLiteDataAdapter ada = new SQLiteDataAdapter();
                        ada.SelectCommand = com;
                        con.DefaultTimeout = 5000;
                        con.Open();
                        ada.Fill(dt);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if(sqliteHelper.selectWithReturn("select isItWithOtherCom from Components where CID = "+ dt.Rows[i][0].ToString()) == "1")
                            {
                                string comwith = sqliteHelper.selectWithReturn("select CNote from Components where CID = " + dt.Rows[i][0].ToString());
                                string q = sqliteHelper.selectWithReturn("select ITotal from ItemMadeOf where IMOCCID = " + dt.Rows[i][0].ToString() + " and IWCID = " + id);
                                string updateString = "update Components set CTotal = CTotal -" + (double.Parse(textEdit5.Text) * double.Parse(q)).ToString() + " where CName = '" + comwith + "'";
                                sqliteHelper.upDate(updateString, 0);

                            }
                            else if (sqliteHelper.selectWithReturn("select isItWithOtherCom from Components where CID = " + dt.Rows[i][0].ToString()) == "0")
                            {
                                string q = sqliteHelper.selectWithReturn("select IQuantity from ItemMadeOf where IMOCCID= " + dt.Rows[i][0].ToString() + " and IWCID =" + id);
                                string updateString = "update Components set CQuantity = CQuantity -" + (double.Parse(textEdit5.Text) * double.Parse(q)).ToString() + ", CTotal = (CQuantity -" + (double.Parse(textEdit5.Text) * double.Parse(q)).ToString() + ") * CPrice where CID =" + dt.Rows[i][0].ToString();
                                sqliteHelper.upDate(updateString, 0);

                            }

                        }

                        double costOFcom = 0;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            selectString = "select ITotal from ItemMadeOf where IMOCCID =" + dt.Rows[i][0].ToString() + " and IWCID = " + id;

                            costOFcom += double.Parse(sqliteHelper.selectWithReturn(selectString));
                        }


                        string costOfout = (costOFcom * Double.Parse(textEdit5.Text)).ToString();

                        string priceOfOut = (Double.Parse(textEdit4.Text) * Double.Parse(textEdit5.Text)).ToString();



                        string profitOfOut = (Double.Parse(priceOfOut) - Double.Parse(costOfout)).ToString();

                        string insertString = "insert into Box (BID,BType,BItemType,IID,BItem,BUnite,BPrice,BQuantity,BTotal,BProfit,BAddingDate,BNote) values ((select coalesce(max(BID),0)+1 from Box),'اخراج','" + "مادة مكونات" + "'," + "(select IWCID from ItemWithCom where IWCName ='" + comboBox2.Text + "'),'" + comboBox2.Text + "','" + textEdit2.Text + "'," + textEdit4.Text + "," + textEdit5.Text + "," + priceOfOut + "," + profitOfOut + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + textEdit6.Text + "')";

                        sqliteHelper.insert(insertString, 1);


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string ITotal = sqliteHelper.selectWithReturn("select ITotal from ItemMadeOf where IMOCCID=" + dt.Rows[i][0].ToString() + " and IWCID =" + id);
                            // priceOfOut = (Double.Parse(sqliteHelper.selectWithReturn("select CPrice from Components where CID =" + dt.Rows[i][0].ToString())) * Double.Parse(textEdit5.Text)).ToString();
                            string qOfCom = sqliteHelper.selectWithReturn("select IQuantity from ItemMadeOf where IMOCCID=" + dt.Rows[i][0].ToString() + " and IWCID =" + id);
                            insertString = "insert into BoxForComponents (BFCID,BFCType,BFCName,BFCCID,IWCID,IWCName,BFCQuantity,BFCPrice,BFCTotal,BFCAddingDate,BFCNote) values ((select coalesce(max(BFCID),0)+1 from BoxForComponents),'اخراج'," + "(select IMadeOf from ItemMadeOf where IMOCCID =" + dt.Rows[i][0].ToString() + " and IWCID = " + id + ")," + dt.Rows[i][0].ToString() +","+ id + ",'" + comboBox2.Text + "'," + double.Parse(textEdit5.Text)*double.Parse(qOfCom) + ",(select CPrice from Components where CID =" + dt.Rows[i][0].ToString() + ")," + double.Parse(textEdit5.Text) * double.Parse(ITotal) + ",'" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + textEdit6.Text + "')";
                            sqliteHelper.insert(insertString, 0);


                        }
                        Box.threadTrigger = 1;

                    }
                    else
                    {
                        id = int.Parse(sqliteHelper.selectWithReturn("select IWCID from ItemWithCom where IWCName='" + comboBox2.Text + "'"));

                        //  MessageBox.Show("لا يوجد مواد كافية");
                        sqliteHelper.comOutPutWithNames(id,double.Parse(textEdit5.Text));
                    }

                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddingNewOutput_FormClosing(object sender, FormClosingEventArgs e)
        {
            Box.x3 = 0;
        }

    

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "مادة مكونات")
            {
                comboBox2.Items.Clear();
                string selectTarget = "Select IWCName as 'اسم المادة'  from ItemWithCom ";
                sqliteHelper.select(selectTarget,comboBox2);
            }
            else if(comboBox1.Text == "مادة ثابتة") 
            {
                comboBox2.Items.Clear();
                string selectTarget = "Select  IName as 'اسم المادة'  from ItemsWithOutCom ";
                sqliteHelper.select(selectTarget, comboBox2);


            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "مادة مكونات")
            {
              
                string selectString = "select IWCUnite from ItemWithCom where IWCName ='" + comboBox2.Text + "'";
                textEdit2.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IWCPrice from ItemWithCom where IWCName ='" + comboBox2.Text + "'";

                textEdit4.Text = sqliteHelper.selectWithReturn(selectString);

                textEdit3.Text = "غير ضرورية";

                selectString = "select CID as 'رقم المكون', CName as 'المكون',CQuantity as 'الكمية', CPrice as 'سعر المكون الواحد',CTotal as 'السعر الكلي',CAddingDate as 'تاريخ الاضافة' from Components where CID IN(select IMOCCID from ItemMadeOf where IName = '" + comboBox2.Text + "')";
                sqliteHelper.select(selectString, dataGridView1);
             


            }
            else if (comboBox1.Text == "مادة ثابتة")
            {
                string selectString = "select IUnite from ItemsWithOutCom where IName ='" + comboBox2.Text + "'";
                textEdit2.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IPrice from ItemsWithOutCom where IName ='" + comboBox2.Text + "'";

                textEdit4.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IQuantity from ItemsWithOutCom where IName ='" + comboBox2.Text + "'";

                textEdit3.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IID as 'رقم المادة', IName as 'اسم المادة',IQuantity as 'الكمية', IPrice as 'سعر المادة',ICost as 'التكلفة',IAddingCost as 'القيمة المضافة',ITotal as 'السعر الكلي',IAddingDate as 'تاريخ الاضافة' from ItemsWithOutCom where IName ='" + comboBox2.Text.Trim() + "'";
                sqliteHelper.select(selectString, dataGridView1);
               

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