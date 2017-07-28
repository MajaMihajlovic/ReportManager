using ReportManager.LogImporting;
using ReportManager.Model;
using ReportManager.Model.Report;
using ReportManager.Writing;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private DataTable errorsTable;
        private DataTable warningsTable;
        private DataTable statisticsTable;

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
            MakeDataTables();
            _pathForSaving = fbd.SelectedPath.Replace("\\", "/");
            var csvWriter = new CSVWriter();
            csvWriter.CreateCSVFile(errorsTable, _pathForSaving+"/errors.csv");
            csvWriter.CreateCSVFile(warningsTable, _pathForSaving+"/warrnings.csv");
            csvWriter.CreateCSVFile(statisticsTable, _pathForSaving+"/statistics.csv");
            System.Windows.Forms.MessageBox.Show("Reports are saved!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MakeDataTables()
        {
            var reportManagerContext = new ReportManagerContext();
            errorsTable = reportManagerContext.ErrorRecords.ToList().ToDataTable();
            warningsTable = reportManagerContext.WarningRecords.ToList().ToDataTable();
            statisticsTable = reportManagerContext.StatisticRecords.ToList().ToDataTable();
            errorsTable.SetColumnsOrder("ID", "CircuitName", "FileContent", "File", "LogDirectory", "Date", "FileState");
            warningsTable.SetColumnsOrder("ID", "CircuitName", "FileContent", "File", "LogDirectory", "Date", "FileState");
            statisticsTable.SetColumnsOrder("ID", "CircuitName", "ErrorCount", "WarningCount", "SignalsCount", "LogDirectory", "Date", "FileState");
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
                importToDB();
            }
            finally
            {
                importFolder.IsEnabled = true;
            }
            System.Windows.MessageBox.Show("All data imported!", "Import completed", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void importToDB()
        {
            var dbWriter = new ReportManagerContext();
            List<ErrorRecord> errorRecords = new ErrorReport().GetRecords(collectedFiles);
            List<StatisticRecord> statisticRecords = new StatisticReport().GetRecords(collectedFiles);
            List<WarningRecord> warningRecords = new WarningReport().GetRecords(collectedFiles);
            Summary summary = cf.MakeSummary();
            dbWriter.Write(errorRecords, dbWriter.ErrorRecords);
            dbWriter.Write(warningRecords, dbWriter.WarningRecords);
            dbWriter.WriteSummary(summary);
            dbWriter.Write(statisticRecords, dbWriter.StatisticRecords);
        }

        private void path_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            importFolder.IsEnabled = System.IO.Directory.Exists(path.Text);
        }
    }
}
