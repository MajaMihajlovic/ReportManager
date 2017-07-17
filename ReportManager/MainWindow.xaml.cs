using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace ReportManager
{
    public partial class MainWindow : Window
    {
        private List<string> reportTypes = new List<string>();
        public PrepareData helper = new PrepareData();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result =fbd.ShowDialog();
            textBox.Text = fbd.SelectedPath;     
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox.Text))
            {
                if (errors.IsChecked.Value || statistics.IsChecked.Value || warnings.IsChecked.Value)
                {
                    var sqlWriter = new WriteToSQLite();
                    helper.ReadFiles(textBox.Text.ToString().Replace("\\", "/"));
                    helper.MakeSummary();
                    List<StatisticRecord> statisticRecords= helper.MakeStatistics();
                    List<WarningRecord> warningRecords = helper.MakeWarnings();
                    List<ErrorRecord> errorRecords = helper.MakeErrors();
                    sqlWriter.WriteToDatabase(statisticRecords,warningRecords,errorRecords);
                    Reports reports = new Reports(reportTypes, helper);
                    reports.Show();
                    Close();
                }
                else System.Windows.Forms.MessageBox.Show("Report is not selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else System.Windows.Forms.MessageBox.Show("Please choose folder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            reportTypes.Add("ERRORS");
        }

        private void warnings_Checked(object sender, RoutedEventArgs e)
        {
            reportTypes.Add("WARNINGS");
        }

        private void Statistics_Checked(object sender, RoutedEventArgs e)
        {
            reportTypes.Add("STATISTICS");
        }
    }
}
