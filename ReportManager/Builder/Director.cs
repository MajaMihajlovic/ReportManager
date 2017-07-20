namespace ReportManager.Builder
{
    public class Director
    {
        public void Construct(IStatisticRecordBuilder recordBuilder,int warningCount,int errorCount)
        {
            recordBuilder.BuildCircuitName();
            recordBuilder.BuildLogDirectory();
            recordBuilder.BuildDate();
            recordBuilder.BuildSignalsCount();
            recordBuilder.BuildWarningCount(warningCount);
            recordBuilder.BuildErrorCount(errorCount);
        }
        public void Contruct(IWarningErroRecordBuilder recordBuilder, string content)
        {
            recordBuilder.BuildCircuitName();
            recordBuilder.BuildLogDirectory();
            recordBuilder.BuildDate();
            recordBuilder.BuildFileName();
            recordBuilder.BuildFileContent(content);
        }
    }
}
