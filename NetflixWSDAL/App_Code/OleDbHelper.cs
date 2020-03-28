 using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
//////////
using System.Data.OleDb;

namespace DrivingDAL
{
  public  class OleDbHelper
    {
        public const int WRITEDATA_ERROR = -1;

        // Disconnected 
        public static DataSet GetDataSet(string strSql)
            {
                OleDbConnection connection = new OleDbConnection(Connect.GetConnectionString());
                OleDbCommand command = new OleDbCommand(strSql, connection);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            // connected 
            public static object ExecuteScalar(string strSql)// מיועד לפעולות שמחזירות נתון בודד
            {
                OleDbConnection connection = new OleDbConnection(Connect.GetConnectionString());
                OleDbCommand command = new OleDbCommand(strSql, connection);
                connection.Open();
                object obj = command.ExecuteScalar();
                connection.Close();
                return obj;
            }
            // connected 
            static public int ExecuteNonQuery(string strSql)// INSERT UPDATE DELETE מחזיר את מס השורות שהושפעו ע"י הפעולה
            {
                OleDbConnection connection = new OleDbConnection(Connect.GetConnectionString());
                OleDbCommand command = new OleDbCommand(strSql, connection);
                //try
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected; 
                //catch  אם יש EXP אין RETURN
            }
            // Disconnected     
            static public DataSet Fill(string com, string tableName)// שם לוגי לטבלה שתווצר בתוך הDS tableName
        {
            OleDbConnection cn = new OleDbConnection(Connect.GetConnectionString());
            OleDbCommand command = new OleDbCommand();
            command.Connection = cn;
            command.CommandText = com;
            DataSet ds = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            adapter.Fill(ds, tableName);
            return ds;

        }
        // Disconnected     
        //פעולה המעדכנת את הדטהבייס בהתאם לדטהסט
        public static void Update(DataSet ds, string com, string name)
        {
            OleDbConnection cn = new OleDbConnection(Connect.GetConnectionString());
            OleDbCommand command = new OleDbCommand();
            command.Connection = cn;
            command.CommandText = com;

            OleDbDataAdapter adapter = new OleDbDataAdapter(command);

            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand();
            try
            {
                cn.Open();
                adapter.Update(ds, name);
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }
        }
        //Connected
        public static int DoQuery(string sql)
        //הפעולה מקבלת שם מסד נתונים ומחרוזת מחיקה/ הוספה/ עדכון
        //ומבצעת את הפקודה על המסד הפיזי
        {
            OleDbConnection conn =new OleDbConnection(Connect.GetConnectionString());
            conn.Open();
            OleDbCommand com = new OleDbCommand(sql, conn);
            int res = com.ExecuteNonQuery();
            conn.Close();
            return res; //מספר השורות שהושפעו
        }

/// <summary>
///  FULLCONNECTION ב READER עבודה עם  
///  מחזיר הפניה לטבלה הפיסית 
/// </summary>
/// <returns></returns>
        public static OleDbDataReader getReader(string cmd)
        {
            OleDbConnection conn = new OleDbConnection(Connect.GetConnectionString());
            OleDbCommand command = new OleDbCommand(cmd, conn);

            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            return reader;
            //  אלה שישתמשו יעבדו כך בקוד
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader[0].ToString());
            //}
            //reader.Close();
        }

        //This function should be used for inserting a single record into a table in the database with an autonmuber key. the format of the sql must be 
        //INSERT INTO <TableName> (Fields...) VALUES (values...)
        //the function return the autonumber key generated for the new record or WRITEDATA_ERROR (-1) if fail.
        public static int InsertWithAutoNumKey(string sql)
        {
            try
            {
                int newID = WRITEDATA_ERROR;

                //Open connection to the database.
                string connString = Connect.GetConnectionString();
                OleDbConnection conn = new OleDbConnection(connString);
                conn.Open();
                //Execute SQL against the database.
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader rd = cmd.ExecuteReader();
                //Read new ID generated by the database using the same connection!
                cmd = new OleDbCommand(@"SELECT @@Identity", conn);
                rd = cmd.ExecuteReader();
                if (rd != null)
                {
                    while (rd.Read())
                    {
                        newID = (int)rd[0];
                    }
                }
                //close connection
                conn.Close();
                return newID;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

      


