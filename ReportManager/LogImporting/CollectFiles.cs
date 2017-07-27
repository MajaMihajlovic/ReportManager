using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ReportManager.LogImporting
{
    public class CollectFiles
    {
        private List<string> collectedFiles=new List<string>();
        private int _numberOfFiles;
        private int _numberOfInvalidChangesets;
        private int _numberOfInvalidExtracts;
        private int _numberOfPendingChangesets;
        private int _numberOfPendingExtracts;
        private int _numberOfRejectedChangesets;
        private string _path;

        public List<string> CollectAllFiles(string path)
        {
            ReadFiles(path);
            return collectedFiles;
        }
        public void ReadFiles(string path)
        {
            _path = path;
            string[] subDirs = null;
            try
            {
                subDirs = Directory.GetDirectories(path);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            foreach (string fileName in subDirs)
            {
                if (fileName.Contains("ChangeSet") && fileName.Contains("Invalid")) _numberOfInvalidChangesets++;
                if (fileName.Contains("Extract") && fileName.Contains("Invalid")) _numberOfInvalidExtracts++;
                if (fileName.Contains("ChangeSet") && fileName.Contains("Pending")) _numberOfPendingChangesets++;
                if (fileName.Contains("Extract") && fileName.Contains("Pending"))  _numberOfPendingExtracts++;
                if (fileName.Contains("ChangeSet") && fileName.Contains("Rejected")) _numberOfRejectedChangesets++;
            }
            collectedFiles.AddRange(subDirs);
            collectedFiles.AddRange(Directory.GetFiles(path));
            foreach (string subdir in subDirs)
            {
                ReadFiles(subdir);
            }
            _numberOfFiles = _numberOfInvalidExtracts + _numberOfInvalidChangesets+ _numberOfInvalidExtracts+ _numberOfInvalidChangesets+ _numberOfRejectedChangesets;
        }
        public Summary MakeSummary()
        {
            return new Summary(_numberOfFiles, _numberOfInvalidExtracts, _numberOfInvalidChangesets, _numberOfInvalidChangesets, _numberOfPendingExtracts, _numberOfRejectedChangesets);
        }
    }
}
