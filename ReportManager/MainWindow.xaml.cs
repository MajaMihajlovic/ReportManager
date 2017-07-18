using ReportManager.LogImporting;
using ReportManager.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace ReportManager
{
    public partial class MainWindow : Window
    {
        private HashSet<string> reportTypes = new HashSet<string>();
        private static string  WARNING="warnings";
        private static string ERROR = "errors";
        private static string STATISTICS = "statistics";
        private List<string> allFiles;
        private string _pathForSaving;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void warnings_Checked(object sender, RoutedEventArgs e)
        {
            reportTypes.Add(WARNING);
        }

        private void Statistics_Checked(object sender, RoutedEventArgs e)
        {
            reportTypes.Add(STATISTICS);
        }

        private void saveToCSV_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            _pathForSaving = fbd.SelectedPath.Replace("\\", "/");
            var csvWriter = new WriteToCSV();
            csvWriter.WriteWarningRecords(new WarningReport().MakeWarnings(allFiles), _pathForSaving);
            csvWriter.WriteErrorRecords(new ErrorReport().MakeErrors(allFiles), _pathForSaving);
            csvWriter.WriteStatisticRecords(new StatisticReport().MakeStatistics(allFiles), _pathForSaving);
            System.Windows.Forms.MessageBox.Show("Reports are saved!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void browseButtonClick(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            path.Text = fbd.SelectedPath;
        }
        private void errors_Checked(object sender, RoutedEventArgs e)
        {
            reportTypes.Add(ERROR);
        }

        private void showReports_Click(object sender, RoutedEventArgs e)
        {
            if (errors.IsChecked.Value || statistics.IsChecked.Value || warnings.IsChecked.Value)
            {
                Reports reports = new Reports(new List<string>(reportTypes));
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
                // Todo: Check whether we can move code below
                CollectFiles cf = new CollectFiles();
                allFiles = cf.CollectAllFiles(path.Text.ToString().Replace("\\", "/"));
                cf.MakeSummary();

                // Just save the records to the database
                var sqlWriter = new WriteToSQLite();
                IEnumerable<ErrorRecord> errorRecords = new ErrorReport().MakeErrors(allFiles);
                sqlWriter.WriteRecords(ERROR, errorRecords);
                List<StatisticRecord> statisticRecords = new StatisticReport().MakeStatistics(allFiles);
                sqlWriter.WriteStatistics(STATISTICS, statisticRecords);
                IEnumerable<WarningRecord> warningRecords = new WarningReport().MakeWarnings(allFiles);
                sqlWriter.WriteRecords(WARNING, warningRecords);
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
