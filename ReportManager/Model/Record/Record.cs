using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;

namespace ReportManager.Model
{
    public class Record
    {
        [Key]
        public static int ID { get; set; }
        [Key]
        public string CircuitName { get;  set; }
        public string LogDirectory { get; set; }
        public string Date { get; set; }
        public string FileState { get; set; }

        public virtual void Accept(IVisitor visitor, SQLiteCommand sqlite_cmd, string tableName) { }

        public Record(string path)
        {
            string _path = path;
            ID++;
        }
    }
}