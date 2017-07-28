using ReportManager.Writing;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ReportManager
{
    public partial class Diagram : Window
    {
        public Diagram()
        {
            InitializeComponent();
            LoadPieChartData();
        }

        private void LoadPieChartData()
        {
            var reportManagerContext = new ReportManagerContext();
            var dt = reportManagerContext.KeyValues.ToList().ToDataTable();
            int totalNumberOfFiles = int.Parse(dt.Rows[0][2].ToString());
            int invalidExtracts = int.Parse(dt.Rows[1][2].ToString());
            int invalidChangesets = int.Parse(dt.Rows[2][2].ToString());
            int pendingChangesets = int.Parse(dt.Rows[3][2].ToString());
            int pendingExtracts = int.Parse(dt.Rows[4][2].ToString());
            int rejectedChangesets = int.Parse(dt.Rows[5][2].ToString());
            ((System.Windows.Controls.DataVisualization.Charting.PieSeries)mcChart.Series[0]).ItemsSource =
            new KeyValuePair<string, int>[]{
            new KeyValuePair<string, int>("Total Rejected Changesets ("+rejectedChangesets.ToString()+")", rejectedChangesets),
            new KeyValuePair<string, int>("Total Invalid Changesets ("+invalidChangesets.ToString()+")", invalidChangesets),
            new KeyValuePair<string, int>("Total Invalid Extracts ("+invalidExtracts.ToString()+")", invalidExtracts),
            new KeyValuePair<string, int>("Total Pending Extracts ("+pendingExtracts.ToString()+")", pendingExtracts),
            new KeyValuePair<string, int>("Total Pending Changesets ("+pendingChangesets.ToString()+")", pendingChangesets)
            };
            textBlock.Text = "Total number of errors is " + totalNumberOfFiles.ToString();
        }
    }
}
