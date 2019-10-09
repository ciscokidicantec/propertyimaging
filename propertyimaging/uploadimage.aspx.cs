using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace propertyimaging
{
    public partial class uploadimage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Start uploading the jpg file to mysql database
            string filename = "testupload.";
            int stringlength;

            filename += "jpg";
            stringlength = filename.Length;

            //MySqlConnection myconnection = new MySqlConnection();

            //string connStr = ConfigurationManager.ConnectionStrings["estateportalConnectionString"].ConnectionString;
            //string connStr = "server=localhost;user id=root;password=Coreldraw1$;persistsecurityinfo=True;database=estateporrtal;" + "providerName=MySql.Data.MySqlClient";
            //string connStr = "server=localhost;user id=root;password=Coreldraw1$;persistsecurityinfo=True;database=estateporrtal;" + "provider=MySql.Data.MySqlClient";
            //myconnection.ConnectionString = connStr;

            //string connStr = "server=localhost;user=root;database=images;port=3306;password=Coreldraw1$";
            //string connStr = "server=localhost;user id=root;password=Coreldraw1$;persistsecurityinfo=True;database=estateporrtal" + "providerName = 'MySql.Data.MySqlClient'";
            //< add name = "qbankEntities" connectionString = "metadata=res://*/qbankModel.csdl|res://*/qbankModel.ssdl|res://*/qbankModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=localhost;Initial Catalog=qbank;Persist Security Info=True;User ID=**;Password=****;MultipleActiveResultSets=True&quot;" providerName = "System.Data.EntityClient" />

            string connStr = "User Id = root; Password = Coreldraw1$; Host = localhost; Database = estateporrtal";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connStr;

            int indeximage;

            try
            {
                //msgbox("Connecting to MySQL...");
                conn.Open();

                string rtn = "loadimage";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", "1");

                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //indeximage = rdr.read("indeximage");
                    indeximage = rdr.GetInt32(0);
                    //Messagebox.show("3333");
                    //Console.WriteLine(rdr[0] + " --- " + rdr[1]);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");
        }
    }
}
