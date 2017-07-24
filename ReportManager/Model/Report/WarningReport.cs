using ReportManager.Builder;
using ReportManager.Model.Report;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ReportManager.Model.Report
{
    public class WarningReport:Report
    {
        public override List<Record> GetRecords(List<string> collectedFiles)
        {
            var fileType = "CIMToDMSTranformReports";
            var warningRecords = new List<Record>();
            Director director = new Director();
            foreach (string fileName in collectedFiles)
            {
                if (fileName.Contains(fileType))
                {
                    try
                    {
                        using (var file = new StreamReader(fileName))
                        {
                            string line = null;
                            while ((line = file.ReadLine()) != null)
                            {
                                if (line.StartsWith("\t -"))
                                {
                                    var warningErorRecordBuilder = new WarningErrorRecordBuilder(fileName);
                                    director.Contruct(warningErorRecordBuilder);
                                    director.Contruct(warningErorRecordBuilder,line);
                                    warningRecords.Add(warningErorRecordBuilder.WarningErrorRecord);
                                }
                            }
                        }
                    }catch(IOException ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            return warningRecords;
        }
    }
}
