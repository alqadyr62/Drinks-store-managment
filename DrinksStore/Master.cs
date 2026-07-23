using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EslamApp
{
    public partial class Master : DevExpress.XtraEditors.XtraForm
    {
        public Master()
        {
            InitializeComponent();
        }
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
          //  navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        public static int x1 = 0;
        public static int x2 = 0;
        public static int x3 = 0;
        public static int x4 = 0;
        public static int x5 = 0;
        public bool key2 = false;



        private void customersTileBarItem_ItemClick(object sender, TileItemEventArgs e)
        {
            if (x4 == 0)
            {
                x4 = 1;
                Items f1 = new Items();
                f1.Dock = DockStyle.Fill;
                f1.MdiParent = this;
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Text = "مدير المواد";
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].ShowCloseButton = DefaultBoolean.False;

                f1.Show();
                //f3.username = uName;

                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.Header.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.HeaderActive.Font = new Font("Times New Roman", 14, FontStyle.Bold);

            }
        }

        private void employeesTileBarItem_ItemClick(object sender, TileItemEventArgs e)
        {
            if (x1 == 0)
            {
                x1 = 1;
                Units f1 = new Units();
                f1.Dock = DockStyle.Fill;
                f1.MdiParent = this;
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Text = "مدير الوحدات";
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].ShowCloseButton = DefaultBoolean.False;

                f1.Show();
                //f3.username = uName;

                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.Header.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.HeaderActive.Font = new Font("Times New Roman", 14, FontStyle.Bold);

            }
        }

        private void Master_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel5.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (key2 == true)
            {
                this.key2 = false;
                Form1.key2 = false;
             //   th1.Abort();
                //    con.Dispose();
                //    com.Dispose();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
        }

        private void tileBarItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            if (x3 == 0)
            {
                x3 = 1;
                Components f1 = new Components();
                f1.Dock = DockStyle.Fill;
                f1.MdiParent = this;
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Text = "مدير المكونات";
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].ShowCloseButton = DefaultBoolean.False;

                f1.Show();
                //f3.username = uName;

                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.Header.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.HeaderActive.Font = new Font("Times New Roman", 14, FontStyle.Bold);

            }
        }

        private void tileBarItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            if (x5==0)
            {
                x5 = 1;
                Box f1 = new Box();
                f1.Dock = DockStyle.Fill;
                f1.MdiParent = this;
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Text = "مدير الصندوق";
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].ShowCloseButton = DefaultBoolean.False;

                f1.Show();
                //f3.username = uName;

                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.Header.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.HeaderActive.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.key1 = true;
            this.key2 = false;
         
            x1 = 0;
            x2 = 0;
            x3 = 0; 
            x4 = 0; 
            x5 = 0;
            this.Close();
            f1.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}