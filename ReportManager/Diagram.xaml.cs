using System;
using System.Collections.Generic;
using System.IO;
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
            StreamReader file = null;
            try
            {
               file = new StreamReader("Summary.csv");
            }catch(IOException ex)
            {
                MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            string line = null;
            int num = 0;
            int total=0, invalidChangesets=0, invalidExtracts = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (num == 1) total = Int32.Parse(line.Split(';')[1]);
                if(num==2) invalidExtracts = Int32.Parse(line.Split(';')[1]);
                if (num == 3) invalidChangesets = Int32.Parse(line.Split(';')[1]);
                num++;
            }
            file.Close();
            ((System.Windows.Controls.DataVisualization.Charting.PieSeries)mcChart.Series[0]).ItemsSource =
            new KeyValuePair<string, int>[]{
            new KeyValuePair<string, int>("Total Invalid Extracts ("+invalidExtracts.ToString()+")", invalidExtracts),
            new KeyValuePair<string, int>("Total Invalid Changesets ("+invalidChangesets.ToString()+")", invalidChangesets),
            };
            textBlock.Text = "Total number of errors is " + total.ToString();
        }
    }
}
