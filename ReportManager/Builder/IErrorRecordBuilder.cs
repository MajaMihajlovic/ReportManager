using ReportManager.Model;

namespace ReportManager.Builder
{
    public interface IErrorRecordBuilder: IRecordBuilder
    {
        ErrorRecord ErrorRecord { get; }
        void BuildFileContent(string file);
        void BuildFileName();
    }
}
