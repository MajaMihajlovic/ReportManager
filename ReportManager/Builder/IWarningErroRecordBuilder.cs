using ReportManager.Model.Report;

namespace ReportManager.Builder
{
    public interface IWarningErroRecordBuilder: IRecordBuilder
    {
        WarningErrorRecord WarningErrorRecord { get; }

        void BuildFileContent(string file);
        void BuildFileName();
    }
}
