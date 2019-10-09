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
using System.IO;

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

                string rtn = "spGETIMAGEBYID";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", "198");

                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //indeximage = rdr.GetInt32(0);
                    indeximage = rdr.GetInt32("imageindex");

                    //Messagebox.show("3333");                    //Console.WriteLine(rdr[0] + " --- " + rdr[1]);
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

    protected void Button2_Click(object sender, EventArgs e)
    {
            string conStr = "User Id = root; Password = Coreldraw1$; Host = localhost; Database = estateporrtal";
            MySqlConnection con = new MySqlConnection();

            con.ConnectionString = conStr;

            string fileName = Path.GetTempFileName() + ".jpg";
            con.Open();
            using (MySqlCommand cmd = con.CreateCommand())
            {
                // you have to distinguish here which document, I assume that there is an `id` column
                cmd.CommandText = "select imageindex,image from images where imageindex=@Id";
                cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = 1;

                int myimageindex = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myimageindex = dr.GetInt32("imageindex");

                        UInt32 size;
                        byte[] buffer;
                        long readBytes = 0;
                        long index = 0;
                        using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            size = dr.GetUInt32((int)fs.Length);
                            buffer = new byte[size];
                            while ((readBytes = (long)dr.GetBytes(0, index, buffer, 0, (Int32)size)) > 0)
                            {
                                fs.Write(buffer, 0, (int)readBytes);
                                index += readBytes;
                            }
                        }
                    }
                }
            }
        }

    }
}