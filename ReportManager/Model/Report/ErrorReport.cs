using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ReportManager.Model
{
    public class ErrorReport : Report
    {

        public List<ErrorRecord> MakeErrors(List<string> allFiles)
        {
            var errorRecords = new List<ErrorRecord>();
            string fileType = "SummaryReport";
            foreach (string s in allFiles)
            {
                if (s.Contains(fileType))
                {
                    StreamReader file = null;
                    try
                    {
                        file = new StreamReader(s);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    string line = null;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains("ResultType"))
                        {
                            string line1 = null;
                            while ((line1 = file.ReadLine()) != null)
                            {
                                string content = null;
                                if (line1.Contains("Error description"))
                                {
                                    content += line1;
                                    string line2 = null;
                                    while ((line2 = file.ReadLine()) != null && !line2.Contains("Error description"))
                                    {
                                        if (!String.IsNullOrWhiteSpace(line2))
                                        {
                                            if (line2.Contains(';'))
                                            {
                                                line2 = line2.Replace(";", " ");
                                            }
                                            content += line2 + " ";
                                        }
                                    }
                                    if (!String.IsNullOrEmpty(content))
                                    {
                                        ErrorRecord errorRecord = new ErrorRecord(GetCircuitName(s), GetLogDirectory(s), GetDate(s), content, GetFileName(s), GetFileState(GetLogDirectory(s), s));
                                        errorRecords.Add(errorRecord);
                                    }
                                }
                            }
                        }
                    }
                    file.Close();
                }
            }
            return errorRecords;
        }
    }
}
