using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportManager.LogImporting
{
    public class KeyValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string Category { get; set; }
        public int Count { get; set; }

        public KeyValue() { }
        public KeyValue(string v, int numberOfFiles)
        {
            Category = v;
            Count = numberOfFiles;
        }
    }
}
