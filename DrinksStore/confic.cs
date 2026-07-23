using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EslamApp
{
    internal class confic
    {

        private Configuration myCon;

        public confic()
        {
            this.myCon = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        public void setDataBasePath(string value, string id)
        {
            /*try
            {  */
            //  confic.ConnectionStrings.ConnectionStrings[name: "Dentist"].Name = "1";
            myCon.ConnectionStrings.ConnectionStrings[name: "MyStore"].ConnectionString = value;
            myCon.ConnectionStrings.ConnectionStrings[name: "MyStore"].ProviderName = "System.Data.SqlClient";
            myCon.Save();
            MessageBox.Show("Done");
            /*  }
              catch (Exception ex) 
              {
                  MessageBox.Show(ex.Message);
              }*/
            //   Configuration con = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // confic.ConnectionStrings.ConnectionStrings[id].Name = value;

            /*  ConfigurationManager.ConnectionStrings[id].ConnectionString = value;
              ConfigurationManager.ConnectionStrings[id].ProviderName = "System.Data.SqlClient";*/

        }1x                                             
    }
}
