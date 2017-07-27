using ReportManager.Builder;
using ReportManager.Model;

namespace ReportManager
{
    public interface IWarningRecordBuilder : IRecordBuilder
    {
        WarningRecord WarningRecord { get; }
        void BuildFileContent(string file);
        void BuildFileName();
    }
}