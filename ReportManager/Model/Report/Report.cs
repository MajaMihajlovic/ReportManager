namespace ReportManager.Model.Report
{
    public abstract class Report
    {
        public string FileState { get; set; }
        public string FileName { get; set; }
        public string LogDirectory { get; set; }
        public string Date { get; set; }
        public string CircuitName { get; set; }
    }
}
