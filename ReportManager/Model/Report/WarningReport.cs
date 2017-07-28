using ReportManager.Builder;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ReportManager.Model.Report
{
    public class WarningReport : Report
    {
        public WarningReport() { }

        public List<WarningRecord> GetRecords(List<string> collectedFiles)
        {
            var fileType = "CIMToDMSTranformReports";
            var warningRecords = new List<WarningRecord>();
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
                                    var warningErorRecordBuilder = new WarningRecordBuilder(fileName);
                                    director.Contruct(warningErorRecordBuilder);
                                    director.Contruct(warningErorRecordBuilder, line);
                                    warningRecords.Add(warningErorRecordBuilder.WarningRecord);
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
            return warningRecords;
        }
}
}
