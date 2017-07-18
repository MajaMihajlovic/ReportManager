using System.Collections.Generic;
using System.Data.SQLite;

namespace ReportManager
{
   public class WriteToSQLite
    {
        private SQLiteConnection sqlite_conn;
        private SQLiteCommand sqlite_cmd = new SQLiteCommand();

        public void WriteStatistics(string tableName, IEnumerable<StatisticRecord> statisticRecord)
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db" + ";Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = new SQLiteCommand("DROP TABLE IF EXISTS "+tableName, sqlite_conn);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "CREATE TABLE "+tableName+" (Circuit string, ErrorCount int ,WarningCount int , SignalsCount int,Status varchar(255),ProcessDate varchar(255),LogDirectory varchar(255));";
            sqlite_cmd.ExecuteNonQuery();
            using (SQLiteTransaction transaction = sqlite_conn.BeginTransaction())
            {
                using (SQLiteCommand cmd = sqlite_conn.CreateCommand())
                {
                    foreach (StatisticRecord record in statisticRecord)
                    {
                        string name = record.GetCircutName();
                        int errorCount = record.GetErrorCount();
                        int warningCount = record.GetWarningCount();
                        int signalsCount = record.GetSignalsCount();
                        string date = record.GetDate();
                        string fileState = record.GetFileState();
                        string log = record.GetLog();
                        sqlite_cmd.CommandText = "INSERT INTO "+tableName+" (Circuit,ErrorCount,WarningCount,SignalsCount,Status,ProcessDate,LogDirectory) VALUES (@Circuit,@ErrorCount,@WarningCount,@SignalsCount,@Status,@ProcessDate,@LogDirectory);";
                        sqlite_cmd.Parameters.AddWithValue("@Circuit", name);
                        sqlite_cmd.Parameters.AddWithValue("@ErrorCount", errorCount);
                        sqlite_cmd.Parameters.AddWithValue("@WarningCount", warningCount);
                        sqlite_cmd.Parameters.AddWithValue("@SignalsCount", signalsCount);
                        sqlite_cmd.Parameters.AddWithValue("@Status", fileState);
                        sqlite_cmd.Parameters.AddWithValue("@ProcessDate", date);
                        sqlite_cmd.Parameters.AddWithValue("@LogDirectory", log);
                        sqlite_cmd.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
            sqlite_conn.Close();
        }
        public void WriteRecords(string tableName, IEnumerable<Record> warningRecords)
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db" + ";Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = new SQLiteCommand("DROP TABLE IF EXISTS " + tableName, sqlite_conn);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "CREATE TABLE " + tableName + " (Circuit string , FileContent string,File varchar(255),Date varchar(255),FileState varchar(255),LogDirectory varchar(255));";
            sqlite_cmd.ExecuteNonQuery();
            using (SQLiteTransaction transaction = sqlite_conn.BeginTransaction())
            {
                using (SQLiteCommand cmd = sqlite_conn.CreateCommand())
                {
                    foreach (Record record in warningRecords)
                    {
                        string fileContent=null, file = null;
                        if (record is WarningRecord){
                             fileContent = ((WarningRecord)record).GetFileContent();
                             file = ((WarningRecord)record).GetFile();
                        }
                        else if(record is ErrorRecord)
                        {
                            fileContent = ((ErrorRecord)record).GetFileContent();
                            file = ((ErrorRecord)record).GetFile();
                        }
                            string name = record.GetCircutName();
                            string date = record.GetDate();
                            string fileState = record.GetFileState();
                            string log = record.GetLog();
                            sqlite_cmd.CommandText = "INSERT INTO " + tableName + " (Circuit,FileContent,File,Date,FileState,LogDirectory) VALUES (@Circuit,@FileContent,@File,@Date,@FileState,@LogDirectory);";
                            sqlite_cmd.Parameters.AddWithValue("@Circuit", name);
                            sqlite_cmd.Parameters.AddWithValue("@FileContent", fileContent);
                            sqlite_cmd.Parameters.AddWithValue("@File", file);
                            sqlite_cmd.Parameters.AddWithValue("@Date", date);
                            sqlite_cmd.Parameters.AddWithValue("@FileState", fileState);
                            sqlite_cmd.Parameters.AddWithValue("@LogDirectory", log);
                            sqlite_cmd.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
            sqlite_conn.Close();
        }
    }
}