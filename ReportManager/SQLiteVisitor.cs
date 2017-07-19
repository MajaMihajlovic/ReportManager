using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportManager.Model;
using System.Data.SQLite;

namespace ReportManager
{
    public class SQLiteVisitor : IVisitor
    {
        public void Visit(StatisticRecord record, SQLiteCommand sqlite_cmd)
        {
            sqlite_cmd.CommandText = "INSERT INTO statistics (Circuit,ErrorCount,WarningCount,SignalsCount,Status,ProcessDate,LogDirectory) VALUES (@Circuit,@ErrorCount,@WarningCount,@SignalsCount,@Status,@ProcessDate,@LogDirectory);";
            sqlite_cmd.Parameters.AddWithValue("@Circuit", record.CircuitName);
            sqlite_cmd.Parameters.AddWithValue("@ErrorCount", record.ErrorCount);
            sqlite_cmd.Parameters.AddWithValue("@WarningCount", record.WarningCount);
            sqlite_cmd.Parameters.AddWithValue("@SignalsCount", record.SignalsCount);
            sqlite_cmd.Parameters.AddWithValue("@Status", record.FileState);
            sqlite_cmd.Parameters.AddWithValue("@ProcessDate", record.Date);
            sqlite_cmd.Parameters.AddWithValue("@LogDirectory", record.Log);
            sqlite_cmd.ExecuteNonQuery();
        }

        public void Visit(ErrorRecord record, SQLiteCommand sqlite_cmd)
        {
            sqlite_cmd.CommandText = "INSERT INTO errors (Circuit,FileContent,File,Date,FileState,LogDirectory) VALUES (@Circuit,@FileContent,@File,@Date,@FileState,@LogDirectory);";
            sqlite_cmd.Parameters.AddWithValue("@Circuit", record.CircuitName);
            sqlite_cmd.Parameters.AddWithValue("@FileContent", record.FileContent);
            sqlite_cmd.Parameters.AddWithValue("@File", record.File);
            sqlite_cmd.Parameters.AddWithValue("@Date", record.Date);
            sqlite_cmd.Parameters.AddWithValue("@FileState", record.FileState);
            sqlite_cmd.Parameters.AddWithValue("@LogDirectory", record.Log);
            sqlite_cmd.ExecuteNonQuery();
        }
        public void Visit(WarningRecord record, SQLiteCommand sqlite_cmd) 
        {
            sqlite_cmd.CommandText = "INSERT INTO warnings (Circuit,FileContent,File,Date,FileState,LogDirectory) VALUES (@Circuit,@FileContent,@File,@Date,@FileState,@LogDirectory);";
            sqlite_cmd.Parameters.AddWithValue("@Circuit", record.CircuitName);
            sqlite_cmd.Parameters.AddWithValue("@FileContent", record.FileContent);
            sqlite_cmd.Parameters.AddWithValue("@File", record.File);
            sqlite_cmd.Parameters.AddWithValue("@Date", record.Date);
            sqlite_cmd.Parameters.AddWithValue("@FileState", record.FileState);
            sqlite_cmd.Parameters.AddWithValue("@LogDirectory", record.Log);
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
