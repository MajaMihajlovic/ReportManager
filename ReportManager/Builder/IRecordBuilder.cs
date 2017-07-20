
namespace ReportManager.Builder
{
    public interface IRecordBuilder
    {
        void BuildCircuitName();
        void BuildDate();
        void BuildLogDirectory();
        void BuildFileState(string log);
    }
}
