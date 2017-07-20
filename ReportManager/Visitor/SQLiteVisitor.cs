
using ReportManager.Model;
using System.Data.SQLite;
using ReportManager.Model.Report;

namespace ReportManager
{
    public class SQLiteVisitor : IVisitor
    {
        public void Visit(StatisticRecord record, SQLiteCommand sqlite_cmd,string tableName)
        {
            sqlite_cmd.CommandText = "INSERT INTO "+tableName+" (Circuit,ErrorCount,WarningCount,SignalsCount,Status,ProcessDate,LogDirectory) VALUES (@Circuit,@ErrorCount,@WarningCount,@SignalsCount,@Status,@ProcessDate,@LogDirectory);";
            sqlite_cmd.Parameters.AddWithValue("@Circuit", record.CircuitName);
            sqlite_cmd.Parameters.AddWithValue("@ErrorCount", record.ErrorCount);
            sqlite_cmd.Parameters.AddWithValue("@WarningCount", record.WarningCount);
            sqlite_cmd.Parameters.AddWithValue("@SignalsCount", record.SignalsCount);
            sqlite_cmd.Parameters.AddWithValue("@Status", record.FileState);
            sqlite_cmd.Parameters.AddWithValue("@ProcessDate", record.Date);
            sqlite_cmd.Parameters.AddWithValue("@LogDirectory", record.LogDirectory);
            sqlite_cmd.ExecuteNonQuery();
        }

        public void Visit(WarningErrorRecord record, SQLiteCommand sqlite_cmd,string tableName)
        {
            sqlite_cmd.CommandText = "INSERT INTO "+tableName+" (Circuit,FileContent,File,Date,FileState,LogDirectory) VALUES (@Circuit,@FileContent,@File,@Date,@FileState,@LogDirectory);";
            sqlite_cmd.Parameters.AddWithValue("@Circuit", record.CircuitName);
            sqlite_cmd.Parameters.AddWithValue("@FileContent", record.FileContent);
            sqlite_cmd.Parameters.AddWithValue("@File", record.File);
            sqlite_cmd.Parameters.AddWithValue("@Date", record.Date);
            sqlite_cmd.Parameters.AddWithValue("@FileState", record.FileState);
            sqlite_cmd.Parameters.AddWithValue("@LogDirectory", record.LogDirectory);
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
