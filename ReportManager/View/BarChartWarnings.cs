using ReportManager.Model.Report;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ReportManager.View
{
    public partial class BarChart : Form
    {
        public BarChart()
        {
            InitializeComponent();
            LoadData();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            Dictionary<string, int> data = StatisticReport.warningTypes;
            List<string> keyList = new List<string>(data.Keys);
            chart1.Series.Remove(chart1.Series[0]);
            for(int i = 0; i < keyList.Count; i++)
            {
                int number = 0;
                data.TryGetValue(keyList[i], out number);
                chart1.Series.Add(keyList[i] + "=" + number.ToString());
                chart1.Series[keyList[i] + "=" + number.ToString()].Points.AddY(number);
            }
        }
    }
}
