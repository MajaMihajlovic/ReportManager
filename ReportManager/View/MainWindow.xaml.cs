using ReportManager.LogImporting;
using ReportManager.Model;
using ReportManager.Model.Report;
using ReportManager.Writing;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private List<string> collectedFiles;
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
            // var sqlReader = new SQLiteReader();
            var reportManagerContext = new ReportManagerContext();
            System.Windows.Forms.MessageBox.Show(reportManagerContext.StatisticRecords.Find(1).ToString());
            List<StatisticRecord> statistics =new List<StatisticRecord>( reportManagerContext.StatisticRecords.Local);
            System.Windows.Forms.MessageBox.Show(statistics.Count.ToString());
            foreach (var s in statistics)
                System.Windows.Forms.MessageBox.Show(s.ToString());
            
            var errors = reportManagerContext.ErrorRecords.ToListAsync().Result.ToDataTable();
            var warrnings = reportManagerContext.WarningRecords.ToListAsync().Result.ToDataTable();
            //csvWriter.CreateCSVFile(statistics, _pathForSaving);
            csvWriter.CreateCSVFile(errors, _pathForSaving);
            csvWriter.CreateCSVFile(warrnings, _pathForSaving);
            System.Windows.Forms.MessageBox.Show("Reports are saved!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void browseButtonClick(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (!(string.IsNullOrWhiteSpace(fbd.SelectedPath)) && fbd.SelectedPath.Contains("Reports"))
            {
                path.Text = fbd.SelectedPath;
                cf = new CollectFiles();
                collectedFiles = cf.CollectAllFiles(path.Text.ToString().Replace("\\", "/"));
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
            //Database.SetInitializer(new NullDatabaseInitializer<ReportManagerContext>());
            importFolder.IsEnabled = false;
            try
            {
                List<ErrorRecord> errorRecords = new ErrorReport().GetRecords(collectedFiles);
                List<StatisticRecord> statisticRecords = new StatisticReport().GetRecords(collectedFiles);
                List<WarningRecord> warningRecords = new WarningReport().GetRecords(collectedFiles);
                List<StatisticRecord> statisticRecords1 = new StatisticReport().GetRecords(collectedFiles);
                Summary summary = cf.MakeSummary();
                var dbWriter = new ReportManagerContext();
                dbWriter.WriteErrors(errorRecords);
                dbWriter.WriteWarnings(warningRecords);
                dbWriter.WriteSummary(summary);
                dbWriter.WriteStatistic(statisticRecords1);
            }
            finally
            {
                importFolder.IsEnabled = true;
            }
            System.Windows.MessageBox.Show("All data imported!", "Import completed", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void path_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            importFolder.IsEnabled = System.IO.Directory.Exists(path.Text);
        }
    }
}
