using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportManager.Model;
using ReportManager.Model.Report;
using System.Windows;

namespace ReportManager.Builder
{
    class WarningErrorRecordBuilder : IWarningErroRecordBuilder
    {
        WarningErrorRecord warningErrorRecord;
        private string _path;

        public WarningErrorRecordBuilder(string path)
        {
            warningErrorRecord = new WarningErrorRecord(path);
            _path = path;
        }

        public WarningErrorRecord WarningErrorRecord
        {
            get { return warningErrorRecord; }
        }

        public void BuildCircuitName()
        {
            var circuit = "";
            var name = _path.Split('_');
            int i = 3;
            while (!name[i].StartsWith("created"))
                circuit += name[i++] + "_";
            int charLocation = circuit.IndexOf("[", StringComparison.Ordinal);
            if (charLocation != -1)
                warningErrorRecord.CircuitName = circuit.Substring(0, charLocation);
            else warningErrorRecord.CircuitName = circuit.TrimEnd('_');
        }

        public void BuildDate()
        {
            var array1 = _path.Split('_');
            if (array1.Length > 1)
            {
                warningErrorRecord.Date = array1[array1.Length - 2].Split('\\')[0] + "_" + array1[array1.Length - 1].Split('\\')[0];
            }
            else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BuildFile(string file)
        {
            warningErrorRecord.File = file;
        }

        public void BuildFileContent(string content)
        {
            warningErrorRecord.FileContent = content;
    }

        public void BuildFileName()
        {
            var array = _path.Split('\\');
            if (array.Length > 0)
            {
                warningErrorRecord.File = array[array.Length - 1];
            }
            else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BuildFileState(string log)
        {
            var array = _path.Split('\\');
            if (log.Contains("ChangeSet"))
            {
                if (array.Length > 2)
                {
                    warningErrorRecord.FileState = array[array.Length - 3] + " ChangeSet";
                }
                else
                {
                    MessageBox.Show("Unexpected file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (log.Contains("Extract"))
            {
                if (array.Length > 2)
                {
                    warningErrorRecord.FileState = array[array.Length - 3] + " Extract";
                }
                else
                {
                    MessageBox.Show("Unexpected file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void BuildLogDirectory()
        {
            var array = _path.Split('\\');
            if (array.Length > 1)
            {
                BuildFileState(array[array.Length - 2]);
                warningErrorRecord.LogDirectory = array[array.Length - 2];
            }
            else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
