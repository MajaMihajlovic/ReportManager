using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ReportManager.Model
{
    public class StatisticReport:Report
    {
        public List<StatisticRecord> MakeStatistics(List<string> allFiles)
        {
            string fileType = "CIMToDMSTranformReports";
            List<StatisticRecord> statisticRecords = new List<StatisticRecord>();
            foreach (string s in allFiles)
            {
                if (s.Contains(fileType))
                {
                    int errorCount = 0;
                    int warningCount = 0;
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
                                MessageBox.Show("Unexpected file content!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
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
                    file.Close();
                    var statistics = new StatisticRecord(GetCircuitName(s), GetLogDirectory(s), GetDate(s), errorCount, warningCount, GetFileState(GetLogDirectory(s), s));
                    statisticRecords.Add(statistics);
                }
            }
            return statisticRecords;
        }
    }
}
