namespace ReportManager.Model
{
    public class StatisticRecord:Record
    {
        public int ErrorCount { get; set; }
        public int WarningCount { get; set; }
        public int SignalsCount { get; set; }

        public StatisticRecord() { }
        public StatisticRecord(string path) : base(path) { }
    }
}
