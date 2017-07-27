using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportManager.Model
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string CircuitName { get; set; }
        public string LogDirectory { get; set; }
        public string Date { get; set; }
        public string FileState { get; set; }

        //public virtual void Accept(IVisitor visitor, SQLiteCommand sqlite_cmd, string tableName) { }
        public Record() { }
        public Record(string path)
        {
            string _path = path;
        }
    }
}