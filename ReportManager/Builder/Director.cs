namespace ReportManager.Builder
{
    public class Director
    {
        public void Construct(IStatisticRecordBuilder recordBuilder, int warningCount, int errorCount)
        {
            recordBuilder.BuildSignalsCount();
            recordBuilder.BuildWarningCount(warningCount);
            recordBuilder.BuildErrorCount(errorCount);
        }

        public void Contruct(IErrorRecordBuilder recordBuilder, string content)
        {
            recordBuilder.BuildFileName();
            recordBuilder.BuildFileContent(content);
        }

        public void Contruct(IWarningRecordBuilder recordBuilder, string content)
        {
            recordBuilder.BuildFileName();
            recordBuilder.BuildFileContent(content);
        }

        public void Contruct(IRecordBuilder recordBuilder)
        {
            recordBuilder.BuildCircuitName();
            recordBuilder.BuildLogDirectory();
            recordBuilder.BuildDate();
        }
    }
}
