using ReportManager.Builder;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ReportManager.Model.Report
{
    public class StatisticReport:Report
    {
        public static Dictionary<string, int> warningTypes;
        public static Dictionary<string, int> errorTypes;

        public override List<Record> GetRecords(List<string> collectedFiles)
        {
            Director director = new Director();
            warningTypes = new Dictionary<string, int>();
            errorTypes = new Dictionary<string, int>();
            string fileType = "CIMToDMSTranformReports";
            List<Record> statisticRecords = new List<Record>();
            string fileContent = null;
            foreach (string fileName in collectedFiles)
            {
                if (fileName.Contains(fileType))
                {
                int errorCount = 0;
                int warningCount = 0;
                try
                {
                    using (StreamReader file = new StreamReader(fileName))
                    {
                    string line = null;
                    int lineNumber = 0;
                    do {
                    if (!string.IsNullOrEmpty(fileContent)) {
                        line = fileContent;
                        fileContent = null;
                    } else
                    {
                        line = file.ReadLine();
                    }
                    lineNumber++;
                    if (lineNumber == 4)
                    {
                        if (line.Contains(":"))
                        {
                            errorCount = int.Parse(line.Split(':')[1]);
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
                            warningCount = int.Parse(line.Split(':')[1]);
                        }
                        else
                        {
                            MessageBox.Show("Unexpected file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else if (line!=null && line.Contains("Warning with code"))
                    {
                        fileContent = GetWarningTypes(line, file);
                    }
                    else if(line != null  && line.Contains("Error with code"))
                    {
                        fileContent = GetErrorTypes(line, file);
                    }
                } while ((line != null));
              }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            var staticticBuilder = new StatisticRecordBuilder(fileName);
            director.Contruct(staticticBuilder);
            director.Construct(staticticBuilder, warningCount, errorCount);
            statisticRecords.Add(staticticBuilder.StatisticRecord);
            }
        }
            return statisticRecords;
        }

        public List<StatisticRecord> GetRecords1(List<string> collectedFiles)
        {
            Director director = new Director();
            warningTypes = new Dictionary<string, int>();
            errorTypes = new Dictionary<string, int>();
            string fileType = "CIMToDMSTranformReports";
            List<StatisticRecord> statisticRecords = new List<StatisticRecord>();
            string fileContent = null;
            foreach (string fileName in collectedFiles)
            {
                if (fileName.Contains(fileType))
                {
                    int errorCount = 0;
                    int warningCount = 0;
                    try
                    {
                        using (StreamReader file = new StreamReader(fileName))
                        {
                            string line = null;
                            int lineNumber = 0;
                            do
                            {
                                if (!string.IsNullOrEmpty(fileContent))
                                {
                                    line = fileContent;
                                    fileContent = null;
                                }
                                else
                                {
                                    line = file.ReadLine();
                                }
                                lineNumber++;
                                if (lineNumber == 4)
                                {
                                    if (line.Contains(":"))
                                    {
                                        errorCount = int.Parse(line.Split(':')[1]);
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
                                        warningCount = int.Parse(line.Split(':')[1]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Unexpected file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                                else if (line != null && line.Contains("Warning with code"))
                                {
                                    fileContent = GetWarningTypes(line, file);
                                }
                                else if (line != null && line.Contains("Error with code"))
                                {
                                    fileContent = GetErrorTypes(line, file);
                                }
                            } while ((line != null));
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    var staticticBuilder = new StatisticRecordBuilder(fileName);
                    director.Contruct(staticticBuilder);
                    director.Construct(staticticBuilder, warningCount, errorCount);
                    statisticRecords.Add(staticticBuilder.StatisticRecord);
                }
            }
            return statisticRecords;
        }

        public string GetWarningTypes(string line, StreamReader file)
        {
            string errorType = line.Split(' ')[3];
            int numberOfErrors = 0;
            while ((line = file.ReadLine()) != null && line.StartsWith("\t -"))
            {
                numberOfErrors += 1; ;
            }
            string fileContent = line;
            if (!warningTypes.ContainsKey(errorType))
            {
                warningTypes.Add(errorType, numberOfErrors);
            }
            else
            {
                int number = 0;
                warningTypes.TryGetValue(errorType, out number);
                warningTypes.Remove(errorType);
                int total = number + numberOfErrors;
                warningTypes.Add(errorType, total);
            }
            return fileContent;
        }

        public string GetErrorTypes(string line,StreamReader file)
        {
            string errorType = line.Split(' ')[3];
            int numberOfErrors = 0;
            while ((line = file.ReadLine()) != null && line.StartsWith("\t -"))
            {
                numberOfErrors += 1; ;
            }
            string fileContent = line;
            if (!errorTypes.ContainsKey(errorType))
            {
                errorTypes.Add(errorType, numberOfErrors);
            }
            else
            {
                int number = 0;
                errorTypes.TryGetValue(errorType, out number);
                errorTypes.Remove(errorType);
                int total = number + numberOfErrors;
                errorTypes.Add(errorType, total);
            }
            return fileContent;
        }
    }
}
