using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            if (!IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["estateportalConnectionString"].ConnectionString;
            //server = localhost; user id = root; persistsecurityinfo = True; database = estateporrtal
            //string constr = "User Id = root; Password = Coreldraw1$; Host = localhost; Database = estateporrtal";

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.CommandText = "SELECT FileId, FileName, ContentType, Content FROM Files";
                    cmd.Connection = con;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvImages.DataSource = dt;
                        gvImages.DataBind();
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Start uploading the jpg file to mysql database
            string filename = "testupload.";
            int stringlength;

            filename += "jpg";
            stringlength = filename.Length;

            //MySqlConnection myconnection = new MySqlConnection();

            string connStr = ConfigurationManager.ConnectionStrings["estateportalConnectionString"].ConnectionString;
            //string connStr = "server=localhost;user id=root;password=Coreldraw1$;persistsecurityinfo=True;database=estateporrtal;" + "providerName=MySql.Data.MySqlClient";
            //string connStr = "server=localhost;user id=root;password=Coreldraw1$;persistsecurityinfo=True;database=estateporrtal;" + "provider=MySql.Data.MySqlClient";
            //myconnection.ConnectionString = connStr;

            //string connStr = "server=localhost;user=root;database=images;port=3306;password=Coreldraw1$";
            //string connStr = "server=localhost;user id=root;password=Coreldraw1$;persistsecurityinfo=True;database=estateporrtal" + "providerName = 'MySql.Data.MySqlClient'";
            //< add name = "qbankEntities" connectionString = "metadata=res://*/qbankModel.csdl|res://*/qbankModel.ssdl|res://*/qbankModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=localhost;Initial Catalog=qbank;Persist Security Info=True;User ID=**;Password=****;MultipleActiveResultSets=True&quot;" providerName = "System.Data.EntityClient" />

            //string connStr = "User Id = root; Password = Coreldraw1$; Host = localhost; Database = estateporrtal";
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
                cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = 198;

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

        protected void Button3_Click(object sender, EventArgs e)
        {

            MySql.Data.MySqlClient.MySqlConnection conn;
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySql.Data.MySqlClient.MySqlDataReader myData;

            conn = new MySql.Data.MySqlClient.MySqlConnection();
            cmd = new MySql.Data.MySqlClient.MySqlCommand();

            string SQL;
            UInt64 FileSize;
            byte[] rawData;
            FileStream fs;

            //string connStr = "User Id = root; Password = Coreldraw1$; Host = localhost; Database = estateporrtal";


            conn.ConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=Coreldraw1$;database=estateporrtal";

            SQL = "SELECT imageindex, image FROM images";

            try
            {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = SQL;

//                byte[] bytes = (byte[])cmd.ExecuteScalar();
//                string strbase64 = Convert.ToBase64String(bytes);
//                Image1.ImageUrl = "data:Image/png;base64," + strbase64;

                myData = cmd.ExecuteReader();

                if (!myData.HasRows)
                    throw new Exception("There are no BLOBs to save");

                //myData.Read();
                //byte[] bytes = (byte[])myData.Read("image");
                //byte[] bytes = (byte[])myData.Read[0];

                myData.Read();
                int indeximagerow1 = myData.GetInt32(0);
                UInt64 indeximagerow64 = myData.GetUInt64("image");
                //   string strbase64 = Convert.ToBase64String(indeximagerow64);
                //                Image1.ImageUrl = "data:Image/png;base64," + strbase64;

             //   byte[] imageBytes = (byte[])reader[0];
             //   MemoryStream buf = new MemoryStream(imageBytes);
             //   Image image = Image.FromStream(buf, true);

                myData.Read();
                int indeximagerow2 = myData.GetInt32(0);


                //FileSize = myData.GetUInt32(myData.GetOrdinal("file_size"));
                //FileSize = myData.GetOrdinal("image");

                //FileSize = myData.GetUInt64(myData.GetOrdinal("image"));
                FileSize = 5744000;
                rawData = new byte[FileSize];

                //myData.GetBytes(myData.GetOrdinal("file"), 0, rawData, 0, (int)FileSize);
                myData.GetBytes(myData.GetOrdinal("image"), 0, rawData, 0, (int)FileSize);

                fs = new FileStream(@"C:\\compress\\newfile.jpg", FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(rawData, 0, (int)FileSize);
                fs.Close();

               // MessageBox.Show("File successfully written to disk!",
               //     "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                myData.Close();
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
               // MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
               //     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string contentType = FileUpload1.PostedFile.ContentType;
            using (Stream fs = FileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    using (MySqlConnection con = new MySqlConnection(constr))
                    {
                        string query = "INSERT INTO Files(FileName, ContentType, Content) VALUES (@FileName, @ContentType, @Content)";
                        using (MySqlCommand cmd = new MySqlCommand(query))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@FileName", filename);
                            cmd.Parameters.AddWithValue("@ContentType", contentType);
                            cmd.Parameters.AddWithValue("@Content", bytes);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            Response.Redirect(Request.Url.AbsoluteUri);

        }

        protected void UploadFile(object sender, EventArgs e)
        {
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string contentType = FileUpload1.PostedFile.ContentType;
                int contentsize = FileUpload1.PostedFile.ContentLength;
                using (Stream fs = FileUpload1.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                        string constr = "User Id = root; Password = Coreldraw1$; Host = localhost; Database = estateporrtal";

                        using (MySqlConnection con = new MySqlConnection(constr))
                        {
                            string query = "INSERT INTO Files(FileName, ContentType, Content) VALUES (@FileName, @ContentType, @Content)";
                            using (MySqlCommand cmd = new MySqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@FileName", filename);
                                cmd.Parameters.AddWithValue("@ContentType", contentType);
                                cmd.Parameters.AddWithValue("@Content", bytes);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] bytes = (byte[])(e.Row.DataItem as DataRowView)["Content"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                (e.Row.FindControl("Image1") as Image).ImageUrl = "data:image/png;base64," + base64String;
            }
        }


        protected void gvImages_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

    }
}