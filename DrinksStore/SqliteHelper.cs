using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Windows.Forms;

using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Runtime.CompilerServices;

namespace myClinic
{
    internal class sqliteHelper
    {

        //  private static Configuration confic = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); 
        private static DataTable dt1 = new DataTable();

        public static void select(string selectString, Object o)
        {
            try
            {
               DataTable dt = new DataTable();

                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(selectString, con);
                SQLiteDataAdapter ada = new SQLiteDataAdapter();
                ada.SelectCommand = com;
                con.DefaultTimeout = 5000;
                con.Open();


                if (o is GridControl)
                {
                    GridControl b1 = (GridControl)o;

                    b1.DataSource = dt;
                    dt = new DataTable();
                    ada.Fill(dt);
                    b1.DataSource = dt;


                }else if (o is DataGridView)
                {
                    DataGridView b1 = (DataGridView)o;

                    b1.DataSource = dt;
                    dt = new DataTable();
                    ada.Fill(dt);
                    b1.DataSource = dt;


                }
                else if (o is System.Windows.Forms.ComboBox)
                {
                    ada.Fill(dt);

                    System.Windows.Forms.ComboBox b2 = (System.Windows.Forms.ComboBox)o;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        b2.Items.Add(dt.Rows[i][0].ToString().Trim());
                    }

                }else if (o is System.Windows.Forms.DataGridViewComboBoxColumn)
                {
                    System.Windows.Forms.DataGridViewComboBoxColumn b1 = (System.Windows.Forms.DataGridViewComboBoxColumn)o;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        b1.Items.Add(dt.Rows[i][0].ToString().Trim());
                    }
                }
            /*   else if (o is LookUpEdit)
                {
                    ada.Fill(dt);

                    LookUpEdit b3 = (LookUpEdit)o;
                    b3.Properties.DataSource = dt;
                    b3.Properties.DisplayMember= "اسم اليوزر";
                    b3.Properties.ValueMember = "رقم اليوزر";





                }*/
                else if (o is ToolStripComboBox)
                {
                    ada.Fill(dt);

                    ToolStripComboBox b2 = (ToolStripComboBox)o;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        b2.Items.Add(dt.Rows[i][0].ToString().Trim());
                    }
                }
                else if (o is TextEdit)
                {
                    ada.Fill(dt);
                    TextEdit b2 = (TextEdit)o;
                    b2.Text = dt.Rows[0][0].ToString().Trim();

                }


                con.Dispose();
                com.Dispose();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static string selectWithReturn(string selectString)
        {
            DataTable dt = new DataTable();
            SQLiteConnection con = new SQLiteConnection();
            con.ConnectionString = loadConnectionString("MyStore");
            SQLiteCommand com = new SQLiteCommand(selectString, con);


            SQLiteDataAdapter ada = new SQLiteDataAdapter();
            ada.SelectCommand = com;
            ada.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();

            }
            else return "";

        }

        public static void insert(string insertString, int showMessage)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(insertString, con);
                con.Open();

                int z = com.ExecuteNonQuery();

                if (z > 0)
                {
                    if (showMessage == 1)
                    {
                        MessageBox.Show("تمت الاضافة بنجاح");
                    }
                }
                con.Dispose();
                com.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public static void delete(string deleteString, int showMessage)
        {
            try
            {

                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(deleteString, con);
                con.Open();
                int z = com.ExecuteNonQuery();
                if (z > 0)
                {
                    if (showMessage == 1)
                    {
                        MessageBox.Show("تم الحذف بنجاح");
                    }

                }
                con.Dispose();
                com.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public static void resetPK(string resetString, int showMessage)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(resetString, con);
                con.Open();
                int z = com.ExecuteNonQuery();
                if (z > 0)
                {
                    if (showMessage == 1)
                    {
                        MessageBox.Show("تم التعديل بنجاح");
                    }
                }
                con.Dispose();
                com.Dispose();
            }
            catch { }
        }

        public static void upDate(string upDateString, int showMessage)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(upDateString, con);
                con.Open();

                int z = com.ExecuteNonQuery();
                if (z > 0)
                {
                    if (showMessage == 1)
                    {
                        MessageBox.Show("تم التعديل بنجاح");
                    }
                }
                con.Dispose();
                com.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static bool isFound(string selectString)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(selectString, con);
                con.Open();
                SQLiteDataAdapter ada = new SQLiteDataAdapter();
                ada.SelectCommand = com;
                DataTable dt = new DataTable();
                ada.Fill(dt);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    return true;
                }
                con.Dispose();
                com.Dispose();

            }
            catch { }
            return false;
        }

        public static void backup(string backupString)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(backupString, con);
                con.Open();

                com.ExecuteNonQuery();


                MessageBox.Show("Backup is Done .... ");

                con.Dispose();
                com.Dispose();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }

        public static void restore(string backupString)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(backupString, con);
                con.Open();

                com.ExecuteNonQuery();


                MessageBox.Show("restore is Done .... ");
                con.Dispose();
                com.Dispose();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }

        public static void EnableStyle(DataGridView dgv)
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;

            dgv.VirtualMode = true;
            dgv.AutoGenerateColumns = true;

            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;


            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            //dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgv.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.EnableHeadersVisualStyles = false;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            //dgv.Dock = DockStyle.Fill;
        }

        public static void EnableStyle2(DataGridView dgv)
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            dgv.AllowUserToAddRows = true;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;

            dgv.VirtualMode = true;
            dgv.AutoGenerateColumns = true;

            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;


            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            //dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgv.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.EnableHeadersVisualStyles = false;
            dgv.MultiSelect = false;
            dgv.ReadOnly = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

        }

           public static string loadConnectionString(string id)
           {
               return ConfigurationManager.ConnectionStrings[id].ConnectionString;
           }

           public static string loadReq(string id)
           {
               Configuration conficfile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
               KeyValueConfigurationCollection settings = conficfile.AppSettings.Settings;
               return settings[id].Value;
           }

           public static void saveReg(string id, string value)
           {
               Configuration conficfile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
               KeyValueConfigurationCollection settings = conficfile.AppSettings.Settings;
               settings[id].Value = value;
               conficfile.Save(ConfigurationSaveMode.Modified);
               ConfigurationManager.RefreshSection(conficfile.AppSettings.SectionInformation.Name);
           }

           public static void lookupEditFill(string selectString,string displayMember,string valueMember,Object o)
           {

               DataTable dt = new DataTable();
               LookUpEdit b3 = (LookUpEdit)o;
               b3.Properties.DataSource = dt;
               SQLiteConnection con = new SQLiteConnection();
               con.ConnectionString = loadConnectionString("MyStore");
               SQLiteCommand com = new SQLiteCommand(selectString, con);
               SQLiteDataAdapter ada = new SQLiteDataAdapter();
               ada.SelectCommand = com;
               con.DefaultTimeout = 5000;
               con.Open();

               ada.Fill(dt);

             //  LookUpEdit b3 = (LookUpEdit)o;
               b3.Properties.DataSource = dt;
               b3.Properties.DisplayMember= displayMember;
               b3.Properties.ValueMember = valueMember;






           }


        /**/
        public static bool comOutput(int id, double q)
        {
           
             
                string selectString = "select IMOCCID from ItemMadeOf where IWCID=" + id;
                DataTable dt = new DataTable();

                SQLiteConnection con = new SQLiteConnection();
                con.ConnectionString = loadConnectionString("MyStore");
                SQLiteCommand com = new SQLiteCommand(selectString, con);
                SQLiteDataAdapter ada = new SQLiteDataAdapter();
                ada.SelectCommand = com;
                con.DefaultTimeout = 5000;
                con.Open();
                ada.Fill(dt);
                int i = 0;
                while (i < dt.Rows.Count)
                {
                    if (sqliteHelper.selectWithReturn("select isItWithOtherCom from Components where CID = " + dt.Rows[i][0].ToString()) == "1")
                    {

                        string comWith = sqliteHelper.selectWithReturn("select CNote from Components where CID = " + dt.Rows[i][0].ToString());
                        double supQ1 = double.Parse(sqliteHelper.selectWithReturn("select ITotal from ItemMadeOf where IMOCCID = " + dt.Rows[i][0].ToString() + " and IWCID = " + id)) * q;

                        if (double.Parse(sqliteHelper.selectWithReturn("select CTotal from Components where CName = '" + comWith + "'")) <= 0 || double.Parse(selectWithReturn("select CTotal from Components where CName ='" + comWith + "'")) < supQ1)
                        {

                            break;
                        }



                        if (i + 1 == dt.Rows.Count)
                        {
                            return true;
                        }
                        i++;


                    }
                    else if (sqliteHelper.selectWithReturn("select isItWithOtherCom from Components where CID = " + dt.Rows[i][0].ToString()) == "0")
                    {
                        string comWith = sqliteHelper.selectWithReturn("select CName from Components where CID =" + dt.Rows[i][0].ToString());
                        double supQ = double.Parse(selectWithReturn("select IQuantity from ItemMadeOf where IMOCCID = " + dt.Rows[i][0].ToString() + " and IWCID = " + id)) * q;
                        if (double.Parse(selectWithReturn("select CQuantity from Components where CID =" + dt.Rows[i][0].ToString())) <= 0 || double.Parse(selectWithReturn("select CQuantity from Components where CID =" + dt.Rows[i][0].ToString())) < supQ)
                        {

                            break;
                        }


                        if (i + 1 == dt.Rows.Count)
                        {


                            return true;
                        }
                        i++;
                    }

                }

                return false;

        }
        public static void comOutPutWithNames(int id, double q)
        {
            string message = "لا يوجد كمية كافية للمواد التالية:";
            string selectString = "select IMOCCID from ItemMadeOf where IWCID=" + id;
            DataTable dt = new DataTable();
            SQLiteConnection con = new SQLiteConnection();
            con.ConnectionString = loadConnectionString("MyStore");
            SQLiteCommand com = new SQLiteCommand(selectString, con);
            SQLiteDataAdapter ada = new SQLiteDataAdapter();
            ada.SelectCommand = com;
            con.DefaultTimeout = 5000;
            con.Open();
            ada.Fill(dt);
            int i = 0;
            while (i < dt.Rows.Count)
            {
                if (sqliteHelper.selectWithReturn("select isItWithOtherCom from Components where CID = " + dt.Rows[i][0].ToString()) == "1")
                {

                    string comWith = sqliteHelper.selectWithReturn("select CNote from Components where CID = " + dt.Rows[i][0].ToString());
                    double supQ1 = double.Parse(sqliteHelper.selectWithReturn("select ITotal from ItemMadeOf where IMOCCID = " + dt.Rows[i][0].ToString() + " and IWCID = " + id)) * q;

                    if (double.Parse(sqliteHelper.selectWithReturn("select CTotal from Components where CName = '" + comWith + "'")) <= 0 || double.Parse(selectWithReturn("select CTotal from Components where CName ='" + comWith + "'")) < supQ1)
                    {

                        message += "-" + comWith;
                    }



                    if (i + 1 == dt.Rows.Count)
                    {
                        MessageBox.Show(message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    i++;


                }
                else if (sqliteHelper.selectWithReturn("select isItWithOtherCom from Components where CID = " + dt.Rows[i][0].ToString()) == "0")
                {
                    string comWith = sqliteHelper.selectWithReturn("select CName from Components where CID =" + dt.Rows[i][0].ToString());
                    double supQ = double.Parse(selectWithReturn("select IQuantity from ItemMadeOf where IMOCCID = " + dt.Rows[i][0].ToString() + " and IWCID = " + id)) * q;
                    if (double.Parse(selectWithReturn("select CQuantity from Components where CID =" + dt.Rows[i][0].ToString())) <= 0 || double.Parse(selectWithReturn("select CQuantity from Components where CID =" + dt.Rows[i][0].ToString())) < supQ)
                    {

                        message += "-" + comWith;

                    }


                    if (i + 1 == dt.Rows.Count)
                    {


                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    }
                    i++;
                }

            }
        }

    }
}
