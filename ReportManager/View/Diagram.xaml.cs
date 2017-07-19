using System.Collections.Generic;
using System.Data;
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
            SQLiteReader sqlReader = new SQLiteReader();
            DataTable dt=sqlReader.GetTable("summary");
            int totalNumberOfFiles = int.Parse(dt.Rows[0][1].ToString());
            int invalidExtracts = int.Parse(dt.Rows[1][1].ToString());
            int invalidChangesets= int.Parse(dt.Rows[2][1].ToString());
            ((System.Windows.Controls.DataVisualization.Charting.PieSeries)mcChart.Series[0]).ItemsSource =
            new KeyValuePair<string, int>[]{
            new KeyValuePair<string, int>("Total Invalid Extracts ("+invalidExtracts.ToString()+")", invalidExtracts),
            new KeyValuePair<string, int>("Total Invalid Changesets ("+invalidChangesets.ToString()+")", invalidChangesets),
            };
            textBlock.Text = "Total number of errors is " + totalNumberOfFiles.ToString();
        }
    }
}
