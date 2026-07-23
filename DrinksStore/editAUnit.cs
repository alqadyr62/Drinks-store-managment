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
    public partial class editAUnit : DevExpress.XtraEditors.XtraForm
    {
        public editAUnit()
        {
            InitializeComponent();
        }
        public   string id = "";
        private void simpleButton4_Click(object sender, EventArgs e)
        {
          
        }

        private void editAUnit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Units.x1 = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}