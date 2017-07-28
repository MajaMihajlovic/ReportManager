namespace ReportManager.Model
{
    public class ErrorRecord : Record
    {
        public string FileContent { get; set; }
        public string File { get; set; }

        public ErrorRecord() { }
        public ErrorRecord(string path) : base(path) { }
    }
}
