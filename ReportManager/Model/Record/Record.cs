using System.Data.SQLite;

namespace ReportManager.Model
{
    public abstract class Record
    {
        public string CircuitName { get; private set; }
        public string Log { get; private set; }
        public string Date { get; private set; }
        public string FileState { get; private set; }

        public abstract void Accept(IVisitor visitor, SQLiteCommand sqlite_cmd);

        public Record(string circuitName, string log, string date, string fileState)
        {
            CircuitName = circuitName;
            Log = log;
            Date = date;
            FileState = fileState;
        }
    }
}
