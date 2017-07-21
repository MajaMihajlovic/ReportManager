using System;
using ReportManager.Model;

namespace ReportManager.Builder
{
    public class StatisticRecordBuilder : RecordBuilder, IStatisticRecordBuilder
    {
        private string _path;

        public StatisticRecordBuilder(string path) : base(path)
        {
            record = new StatisticRecord(path);
            _path = path;
        }

        public StatisticRecord StatisticRecord
        {
            get { return (StatisticRecord)record; }
        }

        public void BuildErrorCount(int errorCount)
        {
            (record as StatisticRecord).ErrorCount = errorCount;
        }

        public void BuildSignalsCount()
        {
            (record as StatisticRecord).SignalsCount = 0;
        }

        public void BuildWarningCount(int warningCount)
        {
            (record as StatisticRecord).WarningCount = warningCount;
        }
    }
}