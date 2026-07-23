using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
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
using DevExpress.Xpo;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Threading;

namespace EslamApp
{
    public partial class Box : DevExpress.XtraEditors.XtraForm
    {
        public Box()
        {
            InitializeComponent();
        }

        public static int x1 = 0;
        public static int x2 = 0;
        public static int x3 = 0;
        string index = "";
        private int flagFromItem = 0;
        private int flagFromCom = 0;
        string ctotalForCom = "";
        string iTotalCost = "";
        Thread th1;
        Thread th2;
       public static int threadTrigger = 0;


        private void Box_Load(object sender, EventArgs e)
        {
            dateEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Today.ToString("yyyy-MM-dd");


            comboBox1.Items.Add("الكل");
            comboBox3.Items.Add("الكل");
            comboBox5.Items.Add("الكل");

            comboBox5.Items.Add("مادة مكونات");
            comboBox5.Items.Add("مادة ثابتة");


            comboBox2.Items.Add("الكل");
            comboBox2.Items.Add("ادخال");
            comboBox2.Items.Add("اخراج");

            comboBox4.Items.Add("الكل");
            comboBox4.Items.Add("ادخال");
            comboBox4.Items.Add("اخراج");
            sqliteHelper.EnableStyle(dataGridView1);
            string selectTarget = "Select  CName  from Components ";
            sqliteHelper.select(selectTarget, comboBox3);

            string selectString = "select coalesce(sum(CTotal),0) from Components";

            ctotalForCom = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            selectString = "select coalesce(sum(IQuantity*ICost),0) from ItemsWithOutCom";

            iTotalCost = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            toolStripStatusLabel2.Text = string.Format("{0:n}", (double.Parse(ctotalForCom) + double.Parse(iTotalCost)));

            th1 = new Thread(start);
            th1.IsBackground = true;
            th1.Start();
            threadTrigger = 0;

             selectString = "select BID as 'رقم العملية' ,BType as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',BItem as 'اسم المادة', printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',BAddingDate as 'تاريخ الإضافة',BNote as 'الملاحظات' from Box  where (BAddingDate between '" + dateEdit1.Text + "' and " +
                "'" + dateEdit2.Text + "')";
            sqliteHelper.select(selectString, this.dataGridView1);



            selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال')  and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

            textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج')  and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

            textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

            textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));





            textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            selectString = "select coalesce(sum(CTotal),0) from Components";

            ctotalForCom = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            selectString = "select coalesce(sum(IQuantity*ICost),0) from ItemsWithOutCom";

            iTotalCost = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            toolStripStatusLabel2.Text = string.Format("{0:n}", (double.Parse(ctotalForCom) + double.Parse(iTotalCost)));
        }


        private void start()
        {
            while (true)
            {
                th2 = new Thread(refresh);
                th2.Start();
            }
        }

        private void refresh()
        {
            if(threadTrigger == 1)
            {
                try
                {
                    threadTrigger = 0;
                    string selectString = "select BID as 'رقم العملية' ,BType as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',BItem as 'اسم المادة', printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',BAddingDate as 'تاريخ الإضافة',BNote as 'الملاحظات' from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "')";
                    sqliteHelper.select(selectString, this.dataGridView1);

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال')";

                    textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج')";

                    textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "')";

                    textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(CTotal),0) from Components";

                    ctotalForCom = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(IQuantity*ICost),0) from ItemsWithOutCom";

                    iTotalCost = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    toolStripStatusLabel2.Text = string.Format("{0:n}", (double.Parse(ctotalForCom) + double.Parse(iTotalCost)));
                    Thread.Sleep(1000);
                }
                catch
                {

                }
               
            }else if (threadTrigger == 2)
            {

                try
                {
                    threadTrigger = 0;
                    string selectString = "select BFCID as 'رقم العملية' ,BFCType as 'نوع العملية',BFCName as 'اسم المكون',IWCID as 'رقم المادة',IWCName as 'اسم المادة', printf('%,d',BFCPrice) as 'السعر',BFCQuantity as 'العدد',printf('%,d',BFCTotal) as 'السعر الكلي',BFCAddingDate as 'تاريخ الإضافة',BFCNote as 'الملاحظات' from BoxForComponents  where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "')";
                    sqliteHelper.select(selectString, this.dataGridView1);

                    selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'ادخال') ";

                    textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'اخراج')";

                    textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));



                    textEdit1.Text = string.Format("{0:n}", 0);

                    selectString = "select coalesce(sum(CTotal),0) from Components";

                    ctotalForCom = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(IQuantity*ICost),0) from ItemsWithOutCom";

                    iTotalCost = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    toolStripStatusLabel2.Text = string.Format("{0:n}", (double.Parse(ctotalForCom) + double.Parse(iTotalCost)));
                    Thread.Sleep(1000);
                }
                catch
                {

                }
               

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            flagFromItem =  1;
            flagFromCom = 0;

            if (comboBox5.Text == "مادة ثابتة")
            {
                if (comboBox1.Text == "الكل" && comboBox2.Text == "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,BType as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',BItem as 'اسم المادة', printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',BAddingDate as 'تاريخ الإضافة',BNote as 'الملاحظات' from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة ثابتة')";
                    sqliteHelper.select(selectString, dataGridView1);

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and (BItemType = 'مادة ثابتة')";

                    textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and (BItemType = 'مادة ثابتة')";

                    textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة ثابتة')";

                    textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));


                 
                }
                else if (comboBox1.Text != "الكل" && comboBox2.Text == "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',trim(BNote) as 'الملاحظات' from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "')  and (BItem ='" + comboBox1.Text + "') and (BItemType = 'مادة ثابتة')";
                    sqliteHelper.select(selectString, dataGridView1);


                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and (BItemType = 'مادة ثابتة') and (BItem ='" + comboBox1.Text + "')";

                    textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and (BItemType = 'مادة ثابتة') and (BItem ='" + comboBox1.Text + "')";

                    textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة ثابتة') and (BItem ='" + comboBox1.Text + "')";

                    textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                }
                else if (comboBox1.Text == "الكل" && comboBox2.Text != "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit),trim(BAddingDate) as 'تاريخ الإضافة' , trim(BNote) as 'الملاحظات'   from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType ='" + comboBox2.Text + "') and (BItemType = 'مادة ثابتة')";
                    sqliteHelper.select(selectString, dataGridView1);


                    if (comboBox2.Text == "ادخال")
                    {
                        selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and (BItemType = 'مادة ثابتة')";

                        textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                        textEdit3.Text = string.Format("{0:n}", 0);
                        textEdit1.Text = string.Format("{0:n}", 0);

                    }

                    if (comboBox2.Text == "اخراج")
                    {

                        textEdit2.Text = string.Format("{0:n}", 0);


                        selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and (BItemType = 'مادة ثابتة')";

                        textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                        selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة ثابتة')";

                        textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));
                    }
                }
                else if (comboBox1.Text != "الكل" && comboBox2.Text != "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',trim(BNote) as 'الملاحظات'   from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType ='" + comboBox2.Text + "') and (BItem ='" + comboBox1.Text + "')  and (BItemType = 'مادة ثابتة')";
                    sqliteHelper.select(selectString, dataGridView1);


                    if (comboBox2.Text == "ادخال")
                    {
                        selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and (BItemType = 'مادة ثابتة') and (BItem ='" + comboBox1.Text + "')";

                        textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                        textEdit3.Text = string.Format("{0:n}", 0);
                        textEdit1.Text = string.Format("{0:n}", 0);

                    }

                    if (comboBox2.Text == "اخراج")
                    {

                        textEdit2.Text = string.Format("{0:n}", 0);


                        selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and (BItemType = 'مادة ثابتة') and (BItem ='" + comboBox1.Text + "')";

                        textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                        selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة ثابتة') and (BItem ='" + comboBox1.Text + "')";

                        textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));
                    }
                }
            }
            else if (comboBox5.Text == "مادة مكونات")
            {
                if (comboBox1.Text == "الكل" && comboBox2.Text == "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,BType as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',BItem as 'اسم المادة', printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',BAddingDate as 'تاريخ الإضافة',BNote as 'الملاحظات' from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة مكونات')";
                    sqliteHelper.select(selectString, dataGridView1);

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and (BItemType = 'مادة مكونات')";

                    textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and (BItemType = 'مادة مكونات')";

                    textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة مكونات')";

                    textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                }
                else if (comboBox1.Text != "الكل" && comboBox2.Text == "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',trim(BNote) as 'الملاحظات' from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "')  and (BItem ='" + comboBox1.Text + "') and (BItemType = 'مادة مكونات')";
                    sqliteHelper.select(selectString, dataGridView1);

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and (BItemType = 'مادة مكونات') and (BItem ='" + comboBox1.Text + "')";

                    textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and (BItemType = 'مادة مكونات') and (BItem ='" + comboBox1.Text + "')";

                    textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة مكونات') and (BItem ='" + comboBox1.Text + "')";

                    textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));
                }
                else if (comboBox1.Text == "الكل" && comboBox2.Text != "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit),trim(BAddingDate) as 'تاريخ الإضافة' , trim(BNote) as 'الملاحظات'   from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType ='" + comboBox2.Text + "') and (BItemType = 'مادة مكونات')";
                    sqliteHelper.select(selectString, dataGridView1);


                    if (comboBox2.Text == "ادخال")
                    {
                        selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and (BItemType = 'مادة مكونات')";

                        textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                        textEdit3.Text = string.Format("{0:n}", 0);
                        textEdit1.Text = string.Format("{0:n}", 0);

                    }

                    if (comboBox2.Text == "اخراج")
                    {

                        textEdit2.Text = string.Format("{0:n}", 0);


                        selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and (BItemType = 'مادة مكونات')";

                        textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                        selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة مكونات')";

                        textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    }

                }
                else if (comboBox1.Text != "الكل" && comboBox2.Text != "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',trim(BNote) as 'الملاحظات'   from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType ='" + comboBox2.Text + "') and (BItem ='" + comboBox1.Text + "')  and (BItemType = 'مادة مكونات')";
                    sqliteHelper.select(selectString, dataGridView1);


                    if (comboBox2.Text == "ادخال")
                    {
                        selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and (BItemType = 'مادة مكونات') and (BItem ='" + comboBox1.Text + "')";

                        textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                        textEdit3.Text = string.Format("{0:n}", 0);
                        textEdit1.Text = string.Format("{0:n}", 0);

                    }

                    if (comboBox2.Text == "اخراج")
                    {

                        textEdit2.Text = string.Format("{0:n}", 0);


                        selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and (BItemType = 'مادة مكونات') and (BItem ='" + comboBox1.Text + "')";

                        textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                        selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BItemType = 'مادة مكونات') and (BItem ='" + comboBox1.Text + "')";

                        textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));
                    }
                }

            }

            if (comboBox1.Text == "الكل" && comboBox2.Text == "الكل" && comboBox5.Text == "الكل")
            {


                string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',trim(BNote) as 'الملاحظات'   from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";
                sqliteHelper.select(selectString, dataGridView1);


                selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال')  and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

                textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج')  and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

                textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

                textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            }
            else if (comboBox5.Text == "الكل" && comboBox1.Text == "الكل")
            {
                if (comboBox2.Text == "ادخال")
                {
                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',trim(BNote) as 'الملاحظات'   from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";
                    sqliteHelper.select(selectString, dataGridView1);


                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال') and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

                    textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    //  selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج')";

                    textEdit3.Text = string.Format("{0:n}", 0);

                    //  selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "')";

                    textEdit1.Text = string.Format("{0:n}", 0);
                }
                else if (comboBox2.Text == "اخراج")
                {
                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',BItemType as 'نوع المنتج',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfit) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',trim(BNote) as 'الملاحظات'   from Box  where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";
                    sqliteHelper.select(selectString, dataGridView1);


                    //     selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال')";

                    textEdit2.Text = string.Format("{0:n}", 0);

                    selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج') and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

                    textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

                    textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));
                }
            }

        }






        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            flagFromCom = 1;
            flagFromItem = 0;

            if (comboBox3.Text == "الكل" && comboBox4.Text == "الكل")
            {
                
                string selectString = "select BFCID as 'رقم العملية' ,BFCType as 'نوع العملية',BFCName as 'اسم المكون',IWCID as 'رقم المادة',IWCName as 'اسم المادة', printf('%,d',BFCPrice) as 'السعر',BFCQuantity as 'العدد',printf('%,d',BFCTotal) as 'السعر الكلي',BFCAddingDate as 'تاريخ الإضافة',BFCNote as 'الملاحظات' from BoxForComponents  where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "')";
                sqliteHelper.select(selectString, dataGridView1);

                selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'ادخال') ";

                textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'اخراج')";

                textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));



                textEdit1.Text = string.Format("{0:n}", 0);

            }
            else if (comboBox3.Text != "الكل" && comboBox4.Text == "الكل")
            {

                string selectString = "select BFCID as 'رقم العملية' ,BFCType as 'نوع العملية',BFCName as 'اسم المكون',IWCID as 'رقم المادة',IWCName as 'اسم المادة', printf('%,d',BFCPrice) as 'السعر',BFCQuantity as 'العدد',printf('%,d',BFCTotal) as 'السعر الكلي',BFCAddingDate as 'تاريخ الإضافة',BFCNote as 'الملاحظات' from BoxForComponents  where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "')  and (BFCName ='" + comboBox3.Text + "')";
                sqliteHelper.select(selectString, dataGridView1);


                selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'ادخال') and (BFCName ='" + comboBox3.Text + "')";

                textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'اخراج')  and (BFCName ='" + comboBox3.Text + "')";

                textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                textEdit1.Text = string.Format("{0:n}", 0);

            }
            else if (comboBox3.Text == "الكل" && comboBox4.Text != "الكل")
            {

                string selectString = "select BFCID as 'رقم العملية' ,BFCType as 'نوع العملية',BFCName as 'اسم المكون',IWCID as 'رقم المادة',IWCName as 'اسم المادة', printf('%,d',BFCPrice) as 'السعر',BFCQuantity as 'العدد',printf('%,d',BFCTotal) as 'السعر الكلي',BFCAddingDate as 'تاريخ الإضافة',BFCNote as 'الملاحظات' from BoxForComponents  where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType ='" + comboBox4.Text + "')";
                sqliteHelper.select(selectString, dataGridView1);


                if (comboBox4.Text == "ادخال")
                {
                    selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'ادخال') ";

                    textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    textEdit3.Text = string.Format("{0:n}", 0);
                    textEdit1.Text = string.Format("{0:n}", 0);

                }

                if (comboBox4.Text == "اخراج")
                {

                    textEdit2.Text = string.Format("{0:n}", 0);


                    selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'اخراج') ";

                    textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));


                    textEdit1.Text = string.Format("{0:n}", 0);
                }

            }
            else if (comboBox3.Text != "الكل" && comboBox4.Text != "الكل")
            {

                string selectString = "select BFCID as 'رقم العملية' ,BFCType as 'نوع العملية',BFCName as 'اسم المكون',IWCID as 'رقم المادة',IWCName as 'اسم المادة', printf('%,d',BFCPrice) as 'السعر',BFCQuantity as 'العدد',printf('%,d',BFCTotal) as 'السعر الكلي',BFCAddingDate as 'تاريخ الإضافة',BFCNote as 'الملاحظات' from BoxForComponents  where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType ='" + comboBox4.Text + "') and (BFCName ='" + comboBox3.Text + "')";
                sqliteHelper.select(selectString, dataGridView1);

                if (comboBox4.Text == "ادخال")
                {
                    selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'ادخال') and (BFCName ='" + comboBox3.Text + "')";

                    textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    textEdit3.Text = string.Format("{0:n}", 0);
                    textEdit1.Text = string.Format("{0:n}", 0);

                }

                if (comboBox4.Text == "اخراج")
                {

                    textEdit2.Text = string.Format("{0:n}", 0);


                    selectString = "select coalesce(sum(BFCTotal),0)  from BoxForComponents where (BFCAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BFCType = 'اخراج') and (BFCName ='" + comboBox3.Text + "')";

                    textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

                    textEdit1.Text = string.Format("{0:n}", 0);
                }

            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (x1 == 0)
            {
                x1 = 1;
                AddingNewInput f1 = new AddingNewInput();
                f1.Show();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            dateEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Today.ToString("yyyy-MM-dd");

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();


            comboBox1.Items.Add("الكل");
            comboBox3.Items.Add("الكل");
            comboBox5.Items.Add("الكل");



            comboBox5.Items.Add("مادة مكونات");
            comboBox5.Items.Add("مادة ثابتة");


            comboBox2.Items.Add("الكل");
            comboBox2.Items.Add("ادخال");
            comboBox2.Items.Add("اخراج");

            comboBox4.Items.Add("الكل");
            comboBox4.Items.Add("ادخال");
            comboBox4.Items.Add("اخراج");

            string selectTarget = "Select  CName  from Components ";
            sqliteHelper.select(selectTarget, comboBox3);

            string selectString = "select coalesce(sum(CTotal),0) from Components";

            ctotalForCom = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            selectString = "select coalesce(sum(IQuantity*ICost),0) from ItemsWithOutCom";

            iTotalCost = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            toolStripStatusLabel2.Text = string.Format("{0:n}", (double.Parse(ctotalForCom) + double.Parse(iTotalCost)));

            selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'ادخال')  and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

            textEdit2.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            selectString = "select coalesce(sum(BTotal),0)  from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and (BType = 'اخراج')  and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

            textEdit3.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));

            selectString = "select coalesce(sum(BProfit),0) from Box where (BAddingDate between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "') and ((BItemType ='مادة مكونات') or (BItemType ='مادة ثابتة'))";

            textEdit1.Text = string.Format("{0:n}", Double.Parse(sqliteHelper.selectWithReturn(selectString)));
        }

        private void Box_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.x5 = 0;

            th1.Abort();
            th2.Abort();
      

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (x3 == 0)
            {
                x3 = 1;
                AddingNewOutput f1 = new AddingNewOutput();
                f1.Show();
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "مادة مكونات")
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("الكل");
                string selectTarget = "Select  IWCName  from ItemWithCom ";
                sqliteHelper.select(selectTarget, comboBox1);
            }
            else if (comboBox5.Text == "مادة ثابتة")
            {

                comboBox1.Items.Clear();
                comboBox1.Items.Add("الكل");
                string selectTarget = "Select  IName  from ItemsWithOutCom ";
                sqliteHelper.select(selectTarget, comboBox1);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                simpleButton5.PerformClick();

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

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if(comboBox2.Text !="" || comboBox4.Text != "") 
            {
                if (MessageBox.Show("هل انت متأكد من إلغاء العملية وتريد المتابعة؟", "مدير المكونات", MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    if (flagFromItem == 1)
                    {
                        if (comboBox2.Text == "الكل")
                        {


                            string BType = sqliteHelper.selectWithReturn("select BType from Box  where BID =" + index);
                            string BItemType = sqliteHelper.selectWithReturn("select BItemType from Box where BID =" + index);
                            if (BType == "اخراج")
                            {
                                if (BItemType == "مادة ثابتة")
                                {
                                    string IID = sqliteHelper.selectWithReturn("select IID from Box where BID =" + index);
                                    string IQuantity = sqliteHelper.selectWithReturn("select BQuantity from Box where BID =" + index);
                                    string updateString = "update ItemsWithOutCom set IQuantity = IQuantity + " + IQuantity + ", ITotal = IPrice * (IQuantity +" + IQuantity + ") where IID =" + IID;
                                    sqliteHelper.upDate(updateString, 0);


                                }
                                else if (BItemType == "مادة مكونات")
                                {
                                    string IID = sqliteHelper.selectWithReturn("select IID from Box where BID =" + index);
                                    string IQuantity = sqliteHelper.selectWithReturn("select BQuantity from Box where BID =" + index);

                                    string selectString = "select IMOCCID from ItemMadeOf where IWCID=" + IID;
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
                                        string q = sqliteHelper.selectWithReturn("select IQuantity from ItemMadeOf where IMOCCID=" + dt.Rows[i][0].ToString() + " and IWCID =" + IID);
                                        string updateString = "update Components set CQuantity = CQuantity +" + (double.Parse(IQuantity) * double.Parse(q)).ToString() + ", CTotal = (CQuantity +" + (double.Parse(IQuantity) * double.Parse(q)).ToString() + ") * CPrice where CID =" + dt.Rows[i][0].ToString();
                                        sqliteHelper.upDate(updateString, 0);

                                        string deleteS = "delete from BoxForComponents where IWCID =" + IID;
                                        sqliteHelper.delete(deleteS, 1);

                                        string resetS = "DBCC CHECKIDENT ('BFCID', reseed, (select max(IWCID) from BoxForComponents))";
                                        sqliteHelper.resetPK(resetS, 0);
                                    }


                                }
                            }
                            else if (BType == "ادخال")
                            {
                                if (BItemType == "مادة ثابتة")
                                {
                                    string IID = sqliteHelper.selectWithReturn("select IID from Box where BID =" + index);
                                    string IQuantity = sqliteHelper.selectWithReturn("select BQuantity from Box where BID =" + index);
                                    string updateString = "update ItemsWithOutCom set IQuantity = IQuantity - " + IQuantity + ", ITotal = IPrice *" + IQuantity + " where IID =" + IID;
                                    sqliteHelper.upDate(updateString, 0);


                                }
                            }
                        }
                        else if (comboBox2.Text == "اخراج")
                        {
                            string BType = sqliteHelper.selectWithReturn("select BType from Box  where BID =" + index);
                            string BItemType = sqliteHelper.selectWithReturn("select BItemType from Box where BID =" + index);
                            if (BType == "اخراج")
                            {
                                if (BItemType == "مادة ثابتة")
                                {
                                    string IID = sqliteHelper.selectWithReturn("select IID from Box where BID =" + index);
                                    string IQuantity = sqliteHelper.selectWithReturn("select BQuantity from Box where BID =" + index);
                                    string updateString = "update ItemsWithOutCom set IQuantity = IQuantity + " + IQuantity + ", ITotal = IPrice *" + IQuantity + " where IID =" + IID;
                                    sqliteHelper.upDate(updateString, 0);



                                }
                                else if (BItemType == "مادة مكونات")
                                {
                                    string IID = sqliteHelper.selectWithReturn("select IID from Box where BID =" + index);
                                    string IQuantity = sqliteHelper.selectWithReturn("select BQuantity from Box where BID =" + index);

                                    string selectString = "select IMOCCID from ItemMadeOf where IWCID=" + IID;
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
                                        string q = sqliteHelper.selectWithReturn("select IQuantity from ItemMadeOf where IMOCCID=" + dt.Rows[i][0].ToString() + " and IWCID =" + IID);
                                        string updateString = "update Components set CQuantity = CQuantity +" + (double.Parse(IQuantity) * double.Parse(q)).ToString() + ", CTotal = (CQuantity +" + (double.Parse(IQuantity) * double.Parse(q)).ToString() + ") * CPrice where CID =" + dt.Rows[i][0].ToString();
                                        sqliteHelper.upDate(updateString, 0);

                                        string deleteS = "delete from BoxForComponents where IWCID =" + IID;
                                        sqliteHelper.delete(deleteS, 1);

                                        string resetS = "DBCC CHECKIDENT ('BFCID', reseed, (select max(BFCID) from BoxForComponents))";
                                        sqliteHelper.resetPK(resetS, 0);
                                    }


                                }
                            }


                        }
                        else if (comboBox2.Text == "ادخال")
                        {
                            string BType = sqliteHelper.selectWithReturn("select BType from Box  where BID =" + index);
                            string BItemType = sqliteHelper.selectWithReturn("select BItemType from Box where BID =" + index);
                            if (BItemType == "مادة ثابتة")
                            {
                                string IID = sqliteHelper.selectWithReturn("select IID from Box where BID =" + index);
                                string IQuantity = sqliteHelper.selectWithReturn("select BQuantity from Box where BID =" + index);
                                string updateString = "update ItemsWithOutCom set IQuantity = IQuantity - " + IQuantity + ", ITotal = IPrice *" + IQuantity + " where IID =" + IID;
                                sqliteHelper.upDate(updateString, 0);


                            }
                        }

                        string deleteString = "delete from Box where BID =" + index;
                        sqliteHelper.delete(deleteString, 1);

                        string resetString = "DBCC CHECKIDENT ('BID', reseed, (select max(BID) from Box))";
                        sqliteHelper.resetPK(resetString, 0);

                        string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                             "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم يحذف عملية من الصندوق" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                        sqliteHelper.insert(InsString, 0);
                        flagFromItem = 0;
                        flagFromCom = 0;
                        threadTrigger = 1;

                    }
                    if (flagFromCom == 1)
                    {
                        if (comboBox4.Text == "الكل")
                        {
                            string BFCType = sqliteHelper.selectWithReturn("select BFCType from BoxForComponents  where BFCID =" + index);
                            if (BFCType == "ادخال")
                            {
                                string BFCCID = sqliteHelper.selectWithReturn("select BFCCID from BoxForComponents where BFCID =" + index);
                                string BFCQuantity = sqliteHelper.selectWithReturn("select BFCQuantity from BoxForComponents where BFCID =" + index);
                                string updateString = "update Components set CQuantity = CQuantity - " + double.Parse(BFCQuantity) + ", CTotal = (CQuantity -" + (double.Parse(BFCQuantity)).ToString() + ") * CPrice where CID =" + BFCCID;
                                sqliteHelper.upDate(updateString, 0);
                                threadTrigger = 2;

                            }
                        }
                        else if (comboBox4.Text == "ادخال")
                        {
                            string BFCType = sqliteHelper.selectWithReturn("select BFCType from BoxForComponents  where BFCID =" + index);
                            if (BFCType == "ادخال")
                            {
                                string BFCCID = sqliteHelper.selectWithReturn("select BFCCID from BoxForComponents where BFCID =" + index);
                                string BFCQuantity = sqliteHelper.selectWithReturn("select BFCQuantity from BoxForComponents where BFCID =" + index);
                                string updateString = "update Components set CQuantity = CQuantity - " + double.Parse(BFCQuantity) + ", CTotal = (CQuantity -" + (double.Parse(BFCQuantity)).ToString() + ") * CPrice where CID =" + BFCCID;
                                sqliteHelper.upDate(updateString, 0);
                                threadTrigger = 2;

                            }
                        }

                        string deleteString = "delete from BoxForComponents where BFCID =" + index;
                        sqliteHelper.delete(deleteString, 1);

                        string resetString = "DBCC CHECKIDENT ('BFCID', reseed, (select max(BFCID) from BoxForComponents))";
                        sqliteHelper.resetPK(resetString, 0);

                        string InsString = "insert into Inspection (InID,InText,InAddingDate) values " +
                                             "((select coalesce(max(InID),0)+1 from Inspection),'" + "قام المستخدم يحذف عملية من الصندوق" + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "')";
                        sqliteHelper.insert(InsString, 0);
                        flagFromCom = 0;
                        flagFromItem = 0;
                        threadTrigger = 2;

                    }
                }
            }else if(comboBox2.Text =="" && comboBox4.Text =="")
            {
                MessageBox.Show("يجب اختيار نوع الفلتر أولاً");
            }
            
           
            
        }
    }
}