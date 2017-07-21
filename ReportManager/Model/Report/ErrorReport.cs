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
        //get 
public override List<Record> MakeRecords(List<string> allFiles)
{
    var errorRecords = new List<Record>();
    Director director = new Director();
    string fileType = "SummaryReport";
    foreach (string s in allFiles)
    {
    if (s.Contains(fileType))
    {
    try
    {
        using (StreamReader file = new StreamReader(s))
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
                            if (!String.IsNullOrWhiteSpace(line2))
                            {
                                if (line2.Contains(';'))
                                {
                                    line2 = line2.Replace(";", "");
                                }
                                content += line2 + " ";
                            }
                            }
                            if (!String.IsNullOrEmpty(content))
                            {
                                var warningErorRecordBuilder = new WarningErrorRecordBuilder(s);
                                director.Contruct(warningErorRecordBuilder);
                                director.Contruct(warningErorRecordBuilder, line);
                                errorRecords.Add(warningErorRecordBuilder.WarningErrorRecord);
                            }
                        }
                    }
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
