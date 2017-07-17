using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ReportManager
{
    public class WriteToCSV
    {
        private string _CSVSeparator = ";";

        public void WriteStatisticRecords(List<StatisticRecord> statisticRecords, string path)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(path + "/Statistics.csv");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            writer.WriteLine("Circuit" + _CSVSeparator + "Error count" + _CSVSeparator + "Warning count" + _CSVSeparator + "Signals count" + _CSVSeparator + "Status" + _CSVSeparator + "Process Date" + _CSVSeparator + "Log Directory");
            foreach (StatisticRecord s in statisticRecords)
            {
                writer.WriteLine(s.GetCircutName() + _CSVSeparator + s.GetErrorCount() + _CSVSeparator + s.GetWarningCount() + _CSVSeparator + "0" + _CSVSeparator + s.GetFileState() + _CSVSeparator + s.GetDate() + _CSVSeparator + s.GetLog());
                writer.Flush();
            }
        }

        public void WriteWarningRecords(List<WarningRecord> warningRecords, string path)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(path + "/Warnings.csv");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            writer.WriteLine("Circuit" + _CSVSeparator + "File content" + _CSVSeparator + "File" + _CSVSeparator + "Date" + _CSVSeparator + "File State" + _CSVSeparator + "Log Directory");
            foreach (WarningRecord record in warningRecords)
                    writer.WriteLine(record.GetCircutName() + _CSVSeparator + record.GetFileContent() + _CSVSeparator + record.GetFile() + _CSVSeparator + record.GetDate() + _CSVSeparator +
                    record.GetFileState() + _CSVSeparator + record.GetLog());
            writer.Close();
        }

        public void WriteErrorRecords(List<ErrorRecord> errorRecords, string path)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(path + "/Errors.csv");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            writer.WriteLine("Circuit" + _CSVSeparator + "File content" + _CSVSeparator + "File" + _CSVSeparator + "Date" + _CSVSeparator + "File State" + _CSVSeparator + "Log Directory");
            foreach (ErrorRecord record in errorRecords)
                    writer.WriteLine(record.GetCircutName() + _CSVSeparator + record.GetFileContent() + _CSVSeparator + record.GetFile() + _CSVSeparator + record.GetDate() + _CSVSeparator + record.GetFileState() + _CSVSeparator + record.GetLog());
            writer.Close();
        }
    }
}
