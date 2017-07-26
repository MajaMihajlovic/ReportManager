using System.ComponentModel.DataAnnotations;

namespace ReportManager.LogImporting
{
    public class KeyValue
    {
        [Key]
        public string Name { get; set; }
        public int NumberOfFiles { get; set; }
       

        public KeyValue(string v, int numberOfFiles)
        {
            Name = v;
            NumberOfFiles = numberOfFiles;
        }
    }
}
