using ReportManager.LogImporting;
using ReportManager.Model;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ReportManager
{
    public class SQLiteWriter
    {
        private SQLiteConnection sqlite_conn;
        private SQLiteCommand sqlite_cmd = new SQLiteCommand();
        private SQLiteVisitor visitor = new SQLiteVisitor();

        public void WriteStatistics(string tableName, IEnumerable<Record> statisticRecord)
        {
            using (sqlite_conn = new SQLiteConnection("Data Source=database.db" + ";Version=3;New=True;Compress=True;"))
            {
                sqlite_conn.Open();
                sqlite_cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS " + tableName + " (Circuit string, ErrorCount int ,WarningCount int , SignalsCount int,Status varchar(255),ProcessDate varchar(255),LogDirectory varchar(255));", sqlite_conn);
                sqlite_cmd.ExecuteNonQuery();
                using (SQLiteTransaction transaction = sqlite_conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = sqlite_conn.CreateCommand())
                    {
                        foreach (StatisticRecord record in statisticRecord)
                        {
                            record.Accept(visitor, sqlite_cmd, tableName);
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        public void WriteSummary(Summary summary)
        {
            using (sqlite_conn = new SQLiteConnection("Data Source=database.db" + ";Version=3;New=True;Compress=True;"))
            {
                sqlite_conn.Open();
                using (sqlite_cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS summary (Category string, Count int );", sqlite_conn))
                {
                    sqlite_cmd.ExecuteNonQuery();
                    foreach(string s in summary.CountedItems.Keys)
                    {
                        int number = 0;
                        summary.CountedItems.TryGetValue(s, out number);
                        sqlite_cmd.CommandText = "INSERT INTO summary (Category,Count) VALUES (@string,@numberOfFiles);";
                        sqlite_cmd.Parameters.AddWithValue("@string",s );
                        sqlite_cmd.Parameters.AddWithValue("@numberOfFiles", number);
                        sqlite_cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void WriteRecords(string tableName, IEnumerable<Record> records)
        {
            using (sqlite_conn = new SQLiteConnection("Data Source=database.db" + ";Version=3;New=True;Compress=True;"))
            {
                sqlite_conn.Open();
                using (sqlite_cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS " + tableName + " (Circuit string , FileContent string,File varchar(255),Date varchar(255),FileState varchar(255),LogDirectory varchar(255));", sqlite_conn))
                {
                    sqlite_cmd.ExecuteNonQuery();
                    using (SQLiteTransaction transaction = sqlite_conn.BeginTransaction())
                    {
                        using (SQLiteCommand cmd = sqlite_conn.CreateCommand())
                        {
                            foreach (Record record in records)
                            {
                                record.Accept(visitor, cmd, tableName);
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
        }
    }
}