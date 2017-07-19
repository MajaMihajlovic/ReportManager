using System;
using System.Data;
using System.Data.SQLite;

namespace ReportManager
{
    public class SQLiteReader
    {
        public DataTable GetTable(String tableName)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = new SQLiteCommand("SELECT * FROM " + tableName, sqlite_conn);
            sqlite_cmd.ExecuteNonQuery();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlite_cmd);
            sqlite_conn.Close();
            DataTable dt = new DataTable(tableName);
            dataAdapter.Fill(dt);
            return dt;
        }
    }
}
