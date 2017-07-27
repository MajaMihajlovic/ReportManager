using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager.Model
{
    public class WarningRecord :Record
    {
        public string FileContent { get; set; }
        public string File { get; set; }
        public string Path { get; set; }

        public WarningRecord(string path) : base(path) { }
    }
}
