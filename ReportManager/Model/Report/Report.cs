using System;
using System.Collections.Generic;
using System.Windows;

namespace ReportManager.Model.Report
{
    public abstract class Report
    {
        public string FileState { get; set; }
        public string FileName { get; set; }
        public string LogDirectory { get; set; }
        public string Date { get; set; }
        public string CircuitName { get; set; }

        public abstract List<Record> GetRecords(List<string> collectedFiles);
    
}
}
