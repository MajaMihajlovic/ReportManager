using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ReportManager
{
    public class PrepareData
    {
        private int _numerOfFiles;
        private int _numberOfInvalidChangesets;
        private int _numberOfInvalidExtracts;
        private List<string> _allFiles = new List<string>();
        private List<StatisticRecord> statisticRecords = new List<StatisticRecord>();
        private string _pathToSum = "Summary.csv";
        private string _path;

        public void MakeSummary()
        {
            Summary sum = new Summary(_numerOfFiles, _numberOfInvalidExtracts, _numberOfInvalidChangesets);
            sum.WriteToFile(_pathToSum);
        }

        public void ReadFiles(String path)
        {
            this._path = path;
            string[] subDirs = null;
            try
            {
                subDirs = Directory.GetDirectories(path);
            }catch(IOException ex) {
                MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            foreach (string s in subDirs)
            {
                if (s.Contains("ChangeSet")) _numberOfInvalidChangesets++;
                if (s.Contains("Extract")) _numberOfInvalidExtracts++;
            }
            _allFiles.AddRange(subDirs);
            _allFiles.AddRange(Directory.GetFiles(path));
            foreach (string subdir in subDirs)
                ReadFiles(subdir);
            _numerOfFiles = _numberOfInvalidExtracts + _numberOfInvalidChangesets;
        }

        public List<StatisticRecord> MakeStatistics()
        {
            string fileType = "CIMToDMSTranformReports";
            foreach (string s in _allFiles)
            {
                if (s.Contains(fileType))
                {
                    int errorCount = 0;
                    int warningCount = 0;
                    StreamReader file=null;
                    try
                    {
                       file = new StreamReader(s);
                    }
                    catch (IOException ex)
                    {
                       MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    string line = null;
                    int lineNumber = 0;
                    while ((line = file.ReadLine()) != null)
                    {
                        lineNumber++;
                        if (lineNumber == 4)
                        {
                            errorCount = Int32.Parse(line.Split(':')[1]);
                        }
                        else if (lineNumber == 5)
                        {
                            warningCount = Int32.Parse(line.Split(':')[1]);
                        }
                    }
                    file.Close();
                    var statistics = new StatisticRecord(GetCircuitName(s), GetLogDirectory(s), GetDate(s), errorCount, warningCount, GetFileState(GetLogDirectory(s), s));
                    statisticRecords.Add(statistics);
                }
            }
            return statisticRecords;
        }

        public List<ErrorRecord> MakeErrors()
        {
            var errorRecords = new List<ErrorRecord>();
            string fileType = "SummaryReport";
            foreach (string s in _allFiles)
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
                        MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public List<WarningRecord> MakeWarnings()
        {
            string fileType = "CIMToDMSTranformReports";
            var warningRecords = new List<WarningRecord>();
            foreach (string s in _allFiles)
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

        public string GetFileState(string log, string s)
        {
            String[] array = s.Split('\\');
            if (log.Contains("ChangeSet"))
                return array[array.Length - 3] + " ChangeSet";
            else if (log.Contains("Extract"))
                return array[array.Length - 3] + " Extract";
            else return "";
        }

        public string GetLogDirectory(string s)
        {
            string[] array = s.Split('\\');
            return array[array.Length - 2];
        }

        public string GetDate(string s)
        {
            string[] array1 = s.Split('_');
            return array1[array1.Length - 2].Split('\\')[0] + "_" + array1[array1.Length - 1].Split('\\')[0];
        }

        public string GetFileName(string s)
        {
            string[] array = s.Split('\\');
            return array[array.Length - 1];
        }

        public static string GetCircuitName(String s)
        {
            var circuit = "";
            String[] name = s.Split('_');
            int i = 3;
            while (!name[i].StartsWith("created"))
                circuit += name[i++] + "_";
            int charLocation = circuit.IndexOf("[", StringComparison.Ordinal);
            if (charLocation != -1)
                return circuit.Substring(0, charLocation);
            else return circuit.TrimEnd('_');
        }
    }
}
