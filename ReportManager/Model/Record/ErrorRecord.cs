using System;
using System.Data.SQLite;

namespace ReportManager.Model
{
    public class ErrorRecord : Record
    {
        public string FileContent { get; private set; }
        public string File { get; private set; }

        public ErrorRecord(string circuitName, string log, string date, string fileContent, string file, string fileState) : base(circuitName, log, date, fileState)
        {
            FileContent = fileContent;
            File = file;
        }

        public override void Accept(IVisitor visitor, SQLiteCommand sqlite_cmd)
        {
            visitor.Visit(this, sqlite_cmd);
        }
    }
}

