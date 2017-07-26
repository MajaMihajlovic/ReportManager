using ReportManager.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ReportManager.Model.Report
{
    public class ErrorReport : Report
    {
        public override List<Record> GetRecords(List<string> collectedFiles)
        {
            var errorRecords = new List<Record>();
            Director director = new Director();
            string fileType = "SummaryReport";
            foreach (string fileName in collectedFiles)
            {
                if (fileName.Contains(fileType))
                {
                    try
                    {
                        using (StreamReader file = new StreamReader(fileName))
                        {
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
                                                var warningErorRecordBuilder = new WarningErrorRecordBuilder(fileName);
                                                director.Contruct(warningErorRecordBuilder);
                                                director.Contruct(warningErorRecordBuilder, line);
                                                errorRecords.Add(warningErorRecordBuilder.WarningErrorRecord);
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
        public  List<WarningErrorRecord> GetRecords1(List<string> collectedFiles)
        {
            var errorRecords = new List<WarningErrorRecord>();
            Director director = new Director();
            string fileType = "SummaryReport";
            foreach (string fileName in collectedFiles)
            {
                if (fileName.Contains(fileType))
                {
                    try
                    {
                        using (StreamReader file = new StreamReader(fileName))
                        {
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
                                                var warningErorRecordBuilder = new WarningErrorRecordBuilder(fileName);
                                                director.Contruct(warningErorRecordBuilder);
                                                director.Contruct(warningErorRecordBuilder, line);
                                                errorRecords.Add(warningErorRecordBuilder.WarningErrorRecord);
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
