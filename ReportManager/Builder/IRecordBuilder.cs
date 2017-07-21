
using ReportManager.Model;

namespace ReportManager.Builder
{
    public interface IRecordBuilder
    {
        Record Record { get; }
        void BuildCircuitName();
        void BuildDate();
        void BuildLogDirectory();
        void BuildFileState(string log);
    }
}
