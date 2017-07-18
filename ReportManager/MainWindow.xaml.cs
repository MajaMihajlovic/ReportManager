
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace ReportManager
{
    public partial class MainWindow : Window
    {
        private List<string> reportTypes = new List<string>();
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
                var sqlWriter = new WriteToSQLite();
                if (reportTypes.Contains(ERROR))
                {
                    IEnumerable<ErrorRecord> errorRecords = new ErrorReport().MakeErrors(allFiles);
                    sqlWriter.WriteRecords(ERROR, errorRecords);
                }
                else if (reportTypes.Contains(STATISTICS))
                {
                    List<StatisticRecord> statisticRecords = new StatisticReport().MakeStatistics(allFiles);
                    sqlWriter.WriteStatistics(STATISTICS, statisticRecords);
                }
                else if (reportTypes.Contains(WARNING))
                {
                   IEnumerable<WarningRecord> warningRecords = new WarningReport().MakeWarnings(allFiles);
                   sqlWriter.WriteRecords(WARNING, warningRecords);
                }
                Reports reports = new Reports(reportTypes);
                reports.Show();
                Close();
            }
            else System.Windows.Forms.MessageBox.Show("Report is not selected!", ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void openFolder_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(path.Text))
            {
                return;
            }
            else
            {
                showReports.IsEnabled = true;
                saveToCSV.IsEnabled = true;
                CollectFiles cf = new CollectFiles();
                allFiles = cf.CollectAllFiles(path.Text.ToString().Replace("\\", "/"));
                cf.MakeSummary();
            }
        }
    }
}
