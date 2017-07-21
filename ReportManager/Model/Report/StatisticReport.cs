using ReportManager.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ReportManager.Model.Report
{
    public class StatisticReport:Report
    {
        public override List<Record> MakeRecords(List<string> allFiles)
        {

            Director director = new Director();
            string fileType = "CIMToDMSTranformReports";
            List<Record> statisticRecords = new List<Record>();
            foreach (string s in allFiles)
            {
                if (s.Contains(fileType))
                {
                    int errorCount = 0;
                    int warningCount = 0;
                    try
                    {
                        using (StreamReader file = new StreamReader(s))
                        {
                            string line = null;
                            int lineNumber = 0;
                            while ((line = file.ReadLine()) != null)
                            {
                                lineNumber++;
                                if (lineNumber == 4)
                                {
                                    if (line.Contains(":"))
                                    {
                                        errorCount = Int32.Parse(line.Split(':')[1]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Unexpected file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                                else if (lineNumber == 5)
                                {
                                    if (line.Contains(":"))
                                    {
                                        warningCount = Int32.Parse(line.Split(':')[1]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Unexpected file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    var staticticBuilder = new StatisticRecordBuilder(s);
                    director.Contruct(staticticBuilder);
                    director.Construct(staticticBuilder, warningCount, errorCount);
                    statisticRecords.Add(staticticBuilder.StatisticRecord);
                }
            }
            return statisticRecords;
        }
    }
}
