using ReportManager.Model;

namespace ReportManager.Builder
{
    public interface IStatisticRecordBuilder:IRecordBuilder
    {
        StatisticRecord StatisticRecord { get; }

        void BuildErrorCount(int errorCount);
        void BuildWarningCount(int warningCount);
        void BuildSignalsCount();
    }
}
