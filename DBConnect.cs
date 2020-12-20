using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//Add MySql Library
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;
using Cassandra;
using System.Data.SqlClient;

namespace EatInEurope
{
    // https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
    //TODO connect to temp data!! important
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string port;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "192.168.1.105";
            port = "3306";
            database = "rest";
            uid = "root";
            password = "Ssoukou8";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public bool Insert(string values, string table)
        {
            string query = "INSERT INTO " + table + " VALUES " + values;

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Select statement 
        public List<string>[] Select(string table, string whereCond, string orderByValue, string order)
        {
            //retaurants:
            string query = "SELECT * FROM " + table;

            if (whereCond != null)
            {
                query += " WHERE " + whereCond;
            }
            if (orderByValue != null)
            {
                query += " ORDER BY " + orderByValue + " " + order;
            }

            //Create a list to store the result
            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["ID_TA"] + "");
                    list[1].Add(dataReader["Name"] + "");
                    list[2].Add(dataReader["City"] + "");
                    list[3].Add(dataReader["Rating"] + "");
                    list[4].Add(dataReader["Price_Range"] + "");
                    list[5].Add(dataReader["URL_TA"] + "");
                    list[6].Add(dataReader["Owner"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        public List<string> Check_existing(string user_name, string password, string table)
        {
            string query = "SELECT COUNT(1) FROM " + table + " WHERE user_name='" + user_name + "' AND password= '" + password + "'";

            List<string> list = new List<string>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(dataReader["count(1)"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }

        }

        //Update statement
        public bool Update(string table, string set, string whereCond)
        {
            string query = "UPDATE " + table + " SET " + set;
            if (whereCond != null)
            {
                query += " WHERE " + whereCond;
            }
            //Open connection
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();
                    //Assign the query using CommandText
                    cmd.CommandText = query;
                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //Delete statement
        public bool Delete(string table, string whereCond)
        {
            string query = "DELETE FROM " + table + " WHERE " + whereCond;

            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return true;
                }
                catch (MySqlException ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }






        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\MySqlBackup" + year + "-" + month + "-" + day +
                          "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "mysqldump",
                    RedirectStandardInput = false,
                    RedirectStandardOutput = true,
                    Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database),
                    UseShellExecute = false
                };

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException)
            {
                Console.WriteLine("Error , unable to backup!");
                MessageBox.Show("Error , unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "mysql",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = false,
                    Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database),
                    UseShellExecute = false
                };


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException)
            {
                Console.WriteLine("Error , unable to Restore!");
                MessageBox.Show("Error , unable to Restore!");
            }
        }
    }
}
