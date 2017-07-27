using ReportManager.Builder;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ReportManager.Model.Report
{
    public class ErrorReport : Report
    {
        public static string FILETYPE = "SummaryReport";
        private Director director = new Director();
        public ErrorReport() { }

        public  List<ErrorRecord> GetRecords(List<string> collectedFiles)
        { 
            var errorRecords = new List<ErrorRecord>();
            foreach (string fileName in collectedFiles)
            {
                if (!fileName.Contains(FILETYPE))
                {
                    MessageBox.Show("Error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }else
                {
                try
                {
                    using (StreamReader file = new StreamReader(fileName))
                    {
                        string line = null;
                        while ((line = file.ReadLine()) != null)
                        {
                            if (!line.Contains("ResultType"))
                            {
                                MessageBox.Show("Error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else { 
                                string line1 = null;
                                while ((line1 = file.ReadLine()) != null)
                                {
                                    string content = null;
                                    if (line1.Contains("Error description"))
                                    {
                                        MessageBox.Show("Error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }else { 
                                        content += line1;
                                    string line2 = null;
                                    while ((line2 = file.ReadLine()) != null && !line2.Contains("Error description"))
                                    {
                                        if (!string.IsNullOrWhiteSpace(line2))
                                        {
                                            if (line2.Contains(';'))
                                            {
                                                line2 = line2.Replace(";", "");
                                            }
                                            content += line2 + " ";
                                        }
                                    }//while ((line2 = file.ReadLine()) != null && !line2.Contains("Error description"))
                                    if (!string.IsNullOrEmpty(content))
                                    {
                                        var warningErorRecordBuilder = new ErrorRecordBuilder(fileName);
                                        director.Contruct(warningErorRecordBuilder);
                                        director.Contruct(warningErorRecordBuilder, line);
                                        errorRecords.Add(warningErorRecordBuilder.ErrorRecord);
                                    }//if (!string.IsNullOrEmpty(content))
                                }//if (line1.Contains("Error description"))
                                }//while ((line1 = file.ReadLine()) != null)
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        return errorRecords;
    }
}
}
