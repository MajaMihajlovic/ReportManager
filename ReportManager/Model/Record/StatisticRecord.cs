using System;
using System.Data.SQLite;

namespace ReportManager.Model
{
    public class StatisticRecord: Record
    {
        public int ErrorCount { get; private set; }
        public int WarningCount { get; private set; }
        public int SignalsCount { get; private set; }

        public StatisticRecord(string circuitName, string log, string date, int errorCount, int warningCount, string fileState) : base(circuitName, log, date, fileState)
        {
            ErrorCount = errorCount;
            WarningCount = warningCount;
            SignalsCount = 0;
        }

        public override void Accept(IVisitor visitor, SQLiteCommand sqlite_cmd)
        {
            visitor.Visit(this,sqlite_cmd);
        }
    }
}
