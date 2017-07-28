namespace ReportManager.Model
{
    public class WarningRecord :Record
    {
        public string FileContent { get; set; }
        public string File { get; set; }

        public WarningRecord() { }
        public WarningRecord(string path) : base(path) { }
    }
}
