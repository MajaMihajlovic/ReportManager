using System;
using ReportManager.Model;
using System.Windows;

namespace ReportManager.Builder
{
    class StatisticRecordBuilder : IStatisticRecordBuilder
    {
        StatisticRecord statisticRecord;
        private string _path;

        public StatisticRecordBuilder(string path)
        {
            statisticRecord = new StatisticRecord(path);
            _path = path;
        }

        public StatisticRecord StatisticRecord
        {
            get { return statisticRecord; }
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
                statisticRecord.CircuitName= circuit.Substring(0, charLocation);
            else statisticRecord.CircuitName= circuit.TrimEnd('_');

        }

        public void BuildDate()
        {
            var array1 = _path.Split('_');
            if (array1.Length > 1)
            {
               statisticRecord.Date=array1[array1.Length - 2].Split('\\')[0] + "_" + array1[array1.Length - 1].Split('\\')[0];
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
                    statisticRecord.FileState = array[array.Length - 3] + " ChangeSet";
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
                    statisticRecord.FileState= array[array.Length - 3] + " Extract";
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
              statisticRecord.LogDirectory=array[array.Length - 2];
            }
            else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BuildErrorCount(int errorCount)
        {
            statisticRecord.ErrorCount = errorCount;
        }

        public void BuildSignalsCount()
        {
            statisticRecord.SignalsCount = 0;
        }

        public void BuildWarningCount(int warningCount)
        {
            statisticRecord.WarningCount = warningCount;
        }

    }
}
