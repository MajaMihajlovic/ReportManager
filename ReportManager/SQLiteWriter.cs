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

        public void WriteStatistics(string tableName, IEnumerable<StatisticRecord> statisticRecord)
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db" + ";Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS " + tableName+" (Circuit string, ErrorCount int ,WarningCount int , SignalsCount int,Status varchar(255),ProcessDate varchar(255),LogDirectory varchar(255));", sqlite_conn);
            sqlite_cmd.ExecuteNonQuery();
            using (SQLiteTransaction transaction = sqlite_conn.BeginTransaction())
            {
                using (SQLiteCommand cmd = sqlite_conn.CreateCommand())
                {
                    foreach (StatisticRecord record in statisticRecord)
                    {
                        record.Accept(visitor,sqlite_cmd);
                    }
                }
                transaction.Commit();
            }
            sqlite_conn.Close();
        }

        public void WriteSummary(Summary summary)
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db" + ";Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS summary (Category string, Count int );", sqlite_conn);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO summary (Category,Count) VALUES (@string,@numberOfFiles);";
            sqlite_cmd.Parameters.AddWithValue("@string", "Total Number Log Directories Processed");
            sqlite_cmd.Parameters.AddWithValue("@numberOfFiles", summary.NumberOfFiles);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO summary (Category,Count) VALUES (@string,@numberOfInvalidExtracts);";
            sqlite_cmd.Parameters.AddWithValue("@string", "Total Invalid Extracts");
            sqlite_cmd.Parameters.AddWithValue("@numberOfInvalidExtracts", summary.NumberOfInvalidExtracts);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO summary (Category,Count) VALUES (@string,@numberOfInvalidChangesets);";
            sqlite_cmd.Parameters.AddWithValue("@string", "Total Invalid Changesets");
            sqlite_cmd.Parameters.AddWithValue("@numberOfInvalidChangesets", summary.NumberOfInvalidChangesets);
            sqlite_cmd.ExecuteNonQuery();
        }

        public void WriteRecords(string tableName, IEnumerable<Record> records)
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db" + ";Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS " + tableName + " (Circuit string , FileContent string,File varchar(255),Date varchar(255),FileState varchar(255),LogDirectory varchar(255));", sqlite_conn);
            sqlite_cmd.ExecuteNonQuery();
            using (SQLiteTransaction transaction = sqlite_conn.BeginTransaction())
            {
                using (SQLiteCommand cmd = sqlite_conn.CreateCommand())
                {
                    foreach (Record record in records)
                    {
                        record.Accept(visitor, cmd);
                    }
                }
                transaction.Commit();
            }
            sqlite_conn.Close();
        }
    }
}