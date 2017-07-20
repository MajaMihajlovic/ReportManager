using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Linq;
using System.Windows.Data;
using System.Windows.Controls;
using System;

namespace ReportManager
{
    public partial class Reports : Window
    {
        private List<string> _reportTypes = new List<string>();
        private DataTable statisticsTable = new DataTable();
        private DataTable errorsTable = new DataTable();
        private DataTable warningsTable = new DataTable();

        public Reports(List<string> list)
        {
            InitializeComponent();
            this._reportTypes = list;
            SQLiteReader sqlReader = new SQLiteReader();
            foreach (string s in _reportTypes)
            {
                ShowTable(sqlReader.GetTable(s));
            }
        }

        public void ShowTable(DataTable dt)
        {
           
            if (dt.TableName.Equals("warnings"))
            {
                tabWarnings.IsEnabled = true;
                warningsTable = dt;
                dataGridWarnings.ItemsSource = dt.DefaultView;
            }
            else if (dt.TableName.Equals("errors"))
            {
                errorsTable = dt;
                tabErrors.IsEnabled = true;
                dataGridErrors.ItemsSource = dt.DefaultView;
            }
            else if (dt.TableName.Equals("statistics"))
            {
                statisticsTable = dt;
                tabStatistics.IsEnabled = true;
                dataGridStatistics.ItemsSource = dt.DefaultView;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show();
        }

        private void ShowDiagramClick(object sender, RoutedEventArgs e)
        {
            Diagram diagram = new ReportManager.Diagram();
            diagram.Show();
        }

        private void dataGridStatistics_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridCellInfo cellInfo = dataGridStatistics.SelectedCells[0];
            selectedValue.Text = GetSelectedCellValue(cellInfo);
        }

        public string GetSelectedCellValue(DataGridCellInfo cellInfo)
        {
            if (cellInfo == null) return null;
            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;
            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);
            return element.Tag.ToString();
        }

        private void dataGridErrors_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridCellInfo cellInfo = dataGridErrors.SelectedCells[0];
            selectedValue.Text = GetSelectedCellValue(cellInfo);
        }

        private void dataGridWarnings_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridCellInfo cellInfo = dataGridWarnings.SelectedCells[0];
            selectedValue.Text = GetSelectedCellValue(cellInfo);
        }

        private void filter_Click(object sender, RoutedEventArgs e)
        {
            if (tabErrors.IsSelected)
            {
               //dataGridStatistics.ItemsSource = errorsTable.DefaultView.RowFilter = String.Format("[Circuit] Like {0}", selectedValue.Text);
            }
            else if (tabStatistics.IsSelected)
            {

            }
            else if (tabWarnings.IsSelected)
            {

            }
        }
    }
}
