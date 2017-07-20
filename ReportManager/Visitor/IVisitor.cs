using ReportManager.Model;
using ReportManager.Model.Report;
using System.Data.SQLite;

namespace ReportManager
{
    public interface IVisitor
    {
        void Visit(StatisticRecord record, SQLiteCommand sqlite_cmd,string tableName);
        void Visit(WarningErrorRecord record, SQLiteCommand sqlite_cmd,string tableName);
    }
}
