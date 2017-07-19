using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReportManager.LogImporting
{
    public class CollectFiles
    {
        private List<string> allFiles=new List<string>();
        private int _numerOfFiles;
        private int _numberOfInvalidChangesets;
        private int _numberOfInvalidExtracts;
        private string _pathToSum = "Summary.csv";
        private string _path;

        public object CotextBox { get; internal set; }

        public List<string> CollectAllFiles(string path)
        {
            ReadFiles(path);
            return allFiles;
        }
        public void ReadFiles(String path)
        {
            this._path = path;
            string[] subDirs = null;
            try
            {
                subDirs = Directory.GetDirectories(path);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            foreach (string s in subDirs)
            {
                if (s.Contains("ChangeSet")) _numberOfInvalidChangesets++;
                if (s.Contains("Extract")) _numberOfInvalidExtracts++;
            }
            allFiles.AddRange(subDirs);
            allFiles.AddRange(Directory.GetFiles(path));
            foreach (string subdir in subDirs)
            {
                ReadFiles(subdir);
            }
            _numerOfFiles = _numberOfInvalidExtracts + _numberOfInvalidChangesets;
        }
        public Summary MakeSummary()
        {
            return new Summary(_numerOfFiles, _numberOfInvalidExtracts, _numberOfInvalidChangesets);
            //sum.WriteToFile(_pathToSum);
        }
    }
}
