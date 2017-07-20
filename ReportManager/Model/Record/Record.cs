using System.Data.SQLite;

namespace ReportManager.Model
{
    public abstract class Record
    {
        public string CircuitName { get;  set; }
        public string LogDirectory { get; set; }
        public string Date { get; set; }
        public string FileState { get; set; }

        public abstract void Accept(IVisitor visitor, SQLiteCommand sqlite_cmd, string tableName);

        public Record(string path)
        {
            string _path = path;
        }
    }
}