namespace ReportManager.Model.Report
{
    public class WarningErrorRecord:Record
    {
        public string FileContent { get; set; }
        public string File { get; set; }
        public string Path { get; set; }

        public WarningErrorRecord(string path) : base(path) { }

        //public override void Accept(IVisitor visitor, SQLiteCommand sqlite_cmd,string tableName)
        //{
        //    visitor.Visit(this, sqlite_cmd,tableName);
        //}
    }
}
