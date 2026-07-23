using DevExpress.XtraEditors;
using Microsoft.Office.Interop.Excel;
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
    public partial class DataBase : DevExpress.XtraEditors.XtraForm
    {
        public DataBase()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "DataBase Files (*.db)|*.db";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            textEdit1.Text = openFileDialog1.FileName;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source = " + textEdit1.Text.Trim() + ";Version=3;";
                confic con = new confic();
                con.setDataBasePath(connectionString, "MyStore");
            }
            catch
            {

            }
        }

        private void DataBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.x2 = 0;
        }
    }
}