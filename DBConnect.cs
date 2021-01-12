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
//using Cassandra;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.VisualBasic;

namespace EatInEurope
{
    // https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
    //TODO connect to temp data!! important
    class DBConnect
    {
        private MySqlConnection connection;
        private MySqlConnection connection2;
        private string server;
        private string port;
        private string database;
        private string uid;
        private string password;
        private MySqlDataReader m_dataReader;
        private bool m_moreToRead;
        bool connect2Open = false;
        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = Interaction.InputBox("server", "Fill connection detailes ");
            port = Interaction.InputBox("port", "Fill connection detailes ");
            uid = Interaction.InputBox("uid", "Fill connection detailes ");
            password = Interaction.InputBox("password", "Fill connection detailes ");
            database = Interaction.InputBox("database", "Fill connection detailes ");

            /* server = "localhost";
             port = "3306";
             database = "rest";
             uid = "root";
             password = "100798";*/
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection2 = new MySqlConnection(connectionString);
            m_moreToRead = false;
            m_dataReader = null;

        }
        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Close();
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

        public bool OpenConnection2()
        {
            try
            {
                connection2.Close();
                connection2.Open();
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

        private bool CloseConnection2()
        {
            try
            {
                connection2.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public bool InsertSelect(string select, string where, string table, string values)
        {
            //insert into t_style_rest select '1' , t_style.id from t_style where t_style.style = 'French';

            string query = "INSERT INTO " + table;
            if (values != null)
            {
                query += " (" + values + ")";
            }
            query += " Select " + select + " WHERE " + where;
            //open connection
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                    return true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            else
            {
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
                try
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                    return true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public List<string>[] Select(string table, string whereCond, string orderByValue, string order, string select, int limit)
        {
            string query;
            if (select != null)
            {
                query = "SELECT " + select + " from " + table;
            }
            else
            {
                query = "SELECT * FROM " + table;
            }
            if (whereCond != null)
            {
                query += " WHERE " + whereCond;
            }
            if (orderByValue != null)
            {
                query += " ORDER BY " + orderByValue + " " + order;
            }
            if (limit != -1)
            {
                query += " LIMIT " + limit.ToString();
            }

            //Create a list to store the result
            List<string>[] list = new List<string>[13];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();
            list[7] = new List<string>();
            list[8] = new List<string>();
            list[9] = new List<string>();
            list[10] = new List<string>();
            list[11] = new List<string>();
            list[12] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader1 = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader1.Read())
                    {
                        try
                        {
                            list[0].Add(dataReader1["ID_TA"] + "");
                        }
                        catch (Exception e)
                        {
                            list[0].Add(null);
                        }
                        try
                        {
                            list[1].Add(dataReader1["Name"] + "");
                        }
                        catch (Exception e)
                        {
                            list[1].Add(null);
                        }
                        try
                        {
                            list[2].Add(dataReader1["city"] + "");
                        }
                        catch (Exception e)
                        {
                            list[2].Add(null);
                        }
                        try
                        {
                            list[3].Add(dataReader1["Rating"] + "");
                        }
                        catch (Exception e)
                        {
                            list[3].Add(null);
                        }
                        try
                        {
                            list[4].Add(dataReader1["Price_Range"] + "");
                        }
                        catch (Exception e)
                        {
                            list[4].Add(null);
                        }
                        try
                        {
                            list[5].Add(dataReader1["URL_TA"] + "");
                        }
                        catch (Exception e)
                        {
                            list[5].Add(null);
                        }
                        try
                        {
                            list[6].Add(dataReader1["Owner"] + "");
                        }
                        catch (Exception e)
                        {
                            list[6].Add(null);
                        }
                        try
                        {
                            list[7].Add(dataReader1["Numbers_of_Reviews"] + "");
                        }
                        catch (Exception e)
                        {
                            list[7].Add(null);
                        }
                        try
                        {
                            list[8].Add(dataReader1["country"] + "");
                        }
                        catch (Exception e)
                        {
                            list[8].Add(null);
                        }
                        try
                        {
                            list[9].Add(dataReader1["style"] + "");
                        }
                        catch (Exception e)
                        {
                            list[9].Add(null);
                        }
                        try
                        {
                            list[10].Add(dataReader1["reviews"] + "");
                        }
                        catch (Exception e)
                        {
                            list[10].Add(null);
                        }
                        try
                        {
                            list[11].Add(dataReader1["dates"] + "");
                        }
                        catch (Exception e)
                        {
                            list[11].Add(null);
                        }
                        try
                        {
                            list[12].Add(dataReader1["rate"] + "");
                        }
                        catch (Exception e)
                        {
                            list[12].Add(null);
                        }
                    }

                    //close Data Reader
                    dataReader1.Close();

                    //close Connection
                    //this.CloseConnection();

                    //return list to be displayed
                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return list;
            }
    }


        //Select statement 
        public List<string>[] SelectRest(string table, string whereCond, string orderByValue, string order, string select,int limit, bool queryContinue, ref bool endReading)
        {

            string query;
            if (select != null) {
                query = "SELECT " + select + " from " + table;
            }
            else {
                query = "SELECT * FROM " + table;
            }
            if (whereCond != null)   {
                query += " WHERE " + whereCond;
            }
            if (orderByValue != null) {
                query += " ORDER BY " + orderByValue + " " + order;
            }
/*            if (limit != -1)
            {
                query += " LIMIT " + limit.ToString();
            }*/

            //Create a list to store the result
            List<string>[] list = new List<string>[6];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            

            bool breakLoop = false;
            //Open connection
            if (!this.connect2Open) 
            {
                
                if (this.OpenConnection2() != true)
                {
                    return list;
                }
                else
                {
                    this.connect2Open = true;
                }
            }
           
            
            try
            {
                MySqlDataReader dataReader = null;

                if (queryContinue)
                    list = parseSqlData(m_dataReader, limit, list, ref breakLoop);
                else
                {
                    if (m_dataReader != null)
                    {
                        m_dataReader.Close();
                        m_dataReader = null;
                    }
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection2);
                    //Create a data reader and Execute the command
                    dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    list = parseSqlData(dataReader, limit, list, ref breakLoop);
                }

                if (breakLoop != true)
                {
                    endReading = true;
                    //we finish reading
                    m_moreToRead = false;
                }
                if (!m_moreToRead)
                {
                    if (m_dataReader != null)
                    {
                        //close Data Reader
                        m_dataReader.Close();
                    }
                    //close Connection
                    this.CloseConnection2();
                    this.connect2Open = false;

                    m_dataReader = null;

                }
                //return list to be displayed
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private List<string>[] parseSqlData(MySqlDataReader dataReader, int limit, List<string>[] list,ref bool breakLoop)
        {
            int i = 0;

            while (dataReader.Read())
            {
                if (limit != -1)
                {
                    if (i == limit)
                    {
                        m_dataReader = dataReader;
                        m_moreToRead = true;
                        breakLoop = true;
                        break;
                    }
                }
                try
                {
                    list[0].Add(dataReader["ID_TA"] + "");
                }
                catch (Exception e)
                {
                    list[0].Add(null);
                }
                try
                {
                    list[1].Add(dataReader["Name"] + "");
                }
                catch (Exception e)
                {
                    list[1].Add(null);
                }
                try
                {
                    list[2].Add(dataReader["city"] + "");
                }
                catch (Exception e)
                {
                    list[2].Add(null);
                }
                try
                {
                    list[3].Add(dataReader["Rating"] + "");
                }
                catch (Exception e)
                {
                    list[3].Add(null);
                }
                try
                {
                    list[4].Add(dataReader["country"] + "");
                }
                catch (Exception e)
                {
                    list[4].Add(null);
                }
                try
                {
                    list[5].Add(dataReader["owner"] + "");
                }
                catch (Exception e)
                {
                    list[5].Add(null);
                }
                i++;
            }
            return list;
        }

        public int select_last_inserted_id()
        {
            string query = "SELECT LAST_INSERT_ID();";
            int last_id = -1;
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //ExecuteScalar will return one value
                    last_id = int.Parse(cmd.ExecuteScalar() + "");

                    //close Connection
                    this.CloseConnection();

                    return last_id;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                return last_id;
            }
            

        }

        public List<string> SelectColumn(string table, string whereCond, string orderByValue, string order ,string column)
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
            List<string> list = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        list.Add(dataReader[column] + "");
                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return list;
            }
        } 

        public List<string> Check_existing(string user_name, string password, string table)
        {
            string query = "SELECT COUNT(1) FROM " + table + " WHERE user_name='" + user_name + "'";
            if (password != null) {
                query += " AND password= '" + password + "'";
            };

            List<string> list = new List<string>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                try
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
                catch (Exception)
                {
                    return null;
                }
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
                    //MessageBox.Show(ex.ToString());

                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        //Count statement
        public List<string>[] Count(string query)
        { 

            List<string>[] list = new List<string>[2];
            list[0] = new List<string>();
            list[1] = new List<string>();

            
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        list[0].Add(dataReader["name"] + "");
                        list[1].Add(dataReader["COUNT(*)"] + "");
                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return list;

                } catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return list;
            }


            //Open Connection
/*            if (this.OpenConnection() == true)
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
            }*/
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
