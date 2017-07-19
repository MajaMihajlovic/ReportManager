using ReportManager.Model;
using System.Data.SQLite;

namespace ReportManager
{
    public interface IVisitor
    {
        void Visit(StatisticRecord record, SQLiteCommand sqlite_cmd);
        void Visit(ErrorRecord record, SQLiteCommand sqlite_cmd);
        void Visit(WarningRecord record, SQLiteCommand sqlite_cmd);
    }
}
