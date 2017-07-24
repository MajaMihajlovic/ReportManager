using System.Data.SQLite;

namespace ReportManager.Model
{
    public class StatisticRecord:Record
    {
        public int ErrorCount { get; set; }
        public int WarningCount { get; set; }
        public int SignalsCount { get; set; }
        public string Path { get; set; }

        public StatisticRecord(string path) : base(path) { }

        public override void Accept(IVisitor visitor, SQLiteCommand sqlite_cmd,string tableName)
        {
            visitor.Visit(this,sqlite_cmd,tableName);
        }
    }
}
