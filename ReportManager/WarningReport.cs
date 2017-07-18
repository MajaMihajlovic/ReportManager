using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager
{
     public class WarningReport:Report
    {
        public List<WarningRecord> MakeWarnings(List<string> allFiles)
        {
            string fileType = "CIMToDMSTranformReports";
            var warningRecords = new List<WarningRecord>();
            foreach (string s in allFiles)
            {
                if (s.Contains(fileType))
                {
                    var file = new StreamReader(s);
                    string line = null;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.StartsWith("\t -"))
                        {
                            var warningRecord = new WarningRecord(GetCircuitName(s), GetLogDirectory(s), GetDate(s), line.TrimStart(), GetFileName(s), GetFileState(GetLogDirectory(s), s));
                            warningRecords.Add(warningRecord);
                        }
                    }
                    file.Close();
                }
            }
            return warningRecords;
        }
    }
}
