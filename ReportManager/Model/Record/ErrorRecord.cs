namespace ReportManager.Model
{
    public class ErrorRecord : Record
    {
        public string FileContent { get; set; }
        public string File { get; set; }
        public string Path { get; set; }

        public ErrorRecord(string path) : base(path) { }
    }
}
