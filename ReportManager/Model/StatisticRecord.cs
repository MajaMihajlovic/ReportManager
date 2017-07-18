namespace ReportManager.Model
{
    public class StatisticRecord: Record
    {
        protected int errorCount;
        protected int warningCount;
        protected int signalsCount = 0;
        public StatisticRecord(string circuitName, string log, string date, int errorCount, int warningCount, string fileState) : base(circuitName, log, date, fileState)
        {
            this.errorCount = errorCount;
            this.warningCount = warningCount;
        }

        public int GetErrorCount()
        {
            return errorCount;
        }

        public int GetWarningCount()
        {
            return warningCount;
        }

        public int GetSignalsCount()
        {
            return signalsCount;
        }
    }
}
