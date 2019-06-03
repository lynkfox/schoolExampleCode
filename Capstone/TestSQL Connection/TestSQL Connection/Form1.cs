using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace TestSQL_Connection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



            static private MySqlConnectionStringBuilder GetConnectionString()
            {
            // To avoid storing the connection string incode, 
            // you can retrieve it from a configuration file.

            // keeps the connection string outside of the main program.

            // can replace this with a file load.


            /*
            MySqlConnectionStringBuilder conString = new MySqlConnectionStringBuilder
            {
                Server = "192.168.1.98",
                Database = "testdatabase",
                UserID = "testAccount",
                Password = "GreenFlowerColorWater2",
                Port = 3306

            };
            */

            MySqlConnectionStringBuilder conString = new MySqlConnectionStringBuilder
            {
                Server = "remotemysql.com",
                Database = "7903HxBTF8",
                UserID = "7903HxBTF8",
                Password = "U89DjsTnQO",
                Port = 3306

            };
            return conString;
            //return @"Server = tcp:192.168.1.98,3306; Database = testdatabase;User ID = testAccount; Password = GreenFlowerColorWater2";
        }



        private void Btn_getAll_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = null;
            DataTable dataTable = new DataTable();
            using (MySqlConnection cnn = new MySqlConnection(GetConnectionString().ConnectionString))
            {

                try
                {
                    string sql = "SELECT * FROM testTable ORDER BY ID";

                    cmd = new MySqlCommand(sql, cnn);

                    cnn.Open();

                    //Debug

                    Console.WriteLine("State: {0}", cnn.State);
                    Console.WriteLine("ConnectionString: {0}",
                        cnn.ConnectionString);
                    Console.WriteLine("Sending Command: {0}", sql);

                    using (MySqlDataAdapter data = new MySqlDataAdapter(cmd))
                    {
                        data.Fill(dataTable);
                    }

                    dataGridView1.DataSource = dataTable;
                    dataGridView1.DataMember = dataTable.TableName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Whoops! Error: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cnn != null) cnn.Close();
                }

            }
        
        }

        private void Btn_AddNew_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = null;
            DataTable dataTable = new DataTable();
            using (MySqlConnection cnn = new MySqlConnection(GetConnectionString().ConnectionString))
            {
                //use a basic class object for this in the future

                int.TryParse(txt_ID.Text, out int ID);

                string firstName = txt_FirstName.Text;
                string lastName = txt_LastName.Text;
                string email = txt_Email.Text;




                try
                {
                    string sql = "INSERT INTO testTable VALUES( '" + ID + "', '" + firstName + "', '" + lastName + "', '" + email + "');";

                    cmd = new MySqlCommand();
                    cmd.Connection = cnn;

                    cmd.CommandText = "INSERT INTO testTable(ID, FIRST_NAME, LAST_NAME, EMAIL) VALUES(?ID, ?FIRST_NAME, ?LAST_NAME, ?EMAIL)";
                    cmd.Parameters.Add("?ID", MySqlDbType.Int32).Value = ID;
                    cmd.Parameters.Add("?FIRST_NAME", MySqlDbType.Text).Value = firstName;
                    cmd.Parameters.Add("?LAST_NAME", MySqlDbType.Text).Value = lastName;
                    cmd.Parameters.Add("?EMAIL", MySqlDbType.Text).Value = email;

                    cnn.Open();

                    //Debug

                    Console.WriteLine("State: {0}", cnn.State);
                    Console.WriteLine("ConnectionString: {0}",
                        cnn.ConnectionString);
                    Console.WriteLine("Sending Command: {0}", cmd);

                    cmd.ExecuteNonQuery();
                    

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Whoops! Error: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cnn != null) cnn.Close();
                }

            }
        }
    }
}
