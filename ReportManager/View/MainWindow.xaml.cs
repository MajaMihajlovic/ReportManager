using ReportManager.LogImporting;
using ReportManager.Model;
using ReportManager.Model.Report;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace ReportManager
{
    public partial class MainWindow : Window
    {
        private HashSet<string> reportTypes = new HashSet<string>();
        private CollectFiles cf;
        private static string  WARNING="warnings";
        private static string ERROR = "errors";
        private static string STATISTICS = "statistics";
        private List<string> allFiles;
        private string _pathForSaving;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void saveToCSV_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            _pathForSaving = fbd.SelectedPath.Replace("\\", "/");
            var csvWriter = new CSVWriter();
            var sqlReader = new SQLiteReader();
            csvWriter.CreateCSVFile(sqlReader.GetTable(WARNING), _pathForSaving);
            csvWriter.CreateCSVFile(sqlReader.GetTable(ERROR), _pathForSaving);
            csvWriter.CreateCSVFile(sqlReader.GetTable(STATISTICS), _pathForSaving);
            System.Windows.Forms.MessageBox.Show("Reports are saved!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void browseButtonClick(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath.Contains("Reports"))
            {
                path.Text = fbd.SelectedPath;
                cf = new CollectFiles();
                allFiles = cf.CollectAllFiles(path.Text.ToString().Replace("\\", "/"));
            }
            else System.Windows.MessageBox.Show("Wrong directory, please check READ ME file.");
  
        }

        private void showReports_Click(object sender, RoutedEventArgs e)
        {
            if (errors.IsChecked.Value || statistics.IsChecked.Value || warnings.IsChecked.Value)
            {
                if (errors.IsChecked.Value) reportTypes.Add(ERROR);
                if(statistics.IsChecked.Value) reportTypes.Add(STATISTICS);
                if (warnings.IsChecked.Value) reportTypes.Add(WARNING);
                var reports = new Reports(new List<string>(reportTypes));
                reports.Show();
                Close();
            }
            else System.Windows.Forms.MessageBox.Show("Report is not selected!", ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void importFolder_Click(object sender, RoutedEventArgs e)
        {
            importFolder.IsEnabled = false;
            try
            {
                var sqlWriter = new SQLiteWriter();
                IEnumerable<Record> errorRecords = new ErrorReport().MakeRecords(allFiles);
                sqlWriter.WriteRecords(ERROR, errorRecords);
                List<Record> statisticRecords = new StatisticReport().MakeRecords(allFiles);
                sqlWriter.WriteStatistics(STATISTICS, statisticRecords);
                IEnumerable<Record> warningRecords = new WarningReport().MakeRecords(allFiles);
                sqlWriter.WriteRecords(WARNING, warningRecords);
                Summary summary = cf.MakeSummary();
                sqlWriter.WriteSummary(summary);
            }
            finally
            {
                importFolder.IsEnabled = true;
            }

            System.Windows.MessageBox.Show("All data imported!", "Imported completed", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void path_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            importFolder.IsEnabled = System.IO.Directory.Exists(path.Text);
        }
    }
}
