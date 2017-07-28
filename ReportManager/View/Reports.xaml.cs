using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using ReportManager.View;
using ReportManager.Model.Report;
using ReportManager.Writing;
using System.Linq;
using System;

namespace ReportManager
{
    public partial class Reports : Window
    {
        private List<string> _reportTypes = new List<string>();
        private DataTable statisticsTable = new DataTable();
        private DataTable errorsTable = new DataTable();
        private DataTable warningsTable = new DataTable();
        private bool buttonPressed = false;

        public Reports(List<string> list)
        {
            InitializeComponent();
            _reportTypes = list;
            var reportManagerContext = new ReportManagerContext();
            foreach (string s in _reportTypes)
            {
                if (s.Equals("errors"))
                {
                    errorsTable = reportManagerContext.ErrorRecords.ToList().ToDataTable();
                    ShowTable(s,errorsTable);
                }
                if (s.Equals("warnings"))
                {
                    warningsTable = reportManagerContext.WarningRecords.ToList().ToDataTable();
                    ShowTable(s,warningsTable);
                }

                if (s.Equals("statistics"))
                {
                    statisticsTable = reportManagerContext.StatisticRecords.ToList().ToDataTable();
                    ShowTable(s,statisticsTable);
                }
            }
        }

        public void ShowTable(string tableName,DataTable dt)
        {
           
            if (tableName.Equals("warnings"))
            {
                dt.SetColumnsOrder("ID", "CircuitName", "FileContent", "File", "LogDirectory", "Date", "FileState");
                tabWarnings.IsEnabled = true;
                warningsTable = dt;
                dataGridWarnings.ItemsSource = dt.DefaultView;
            }
            else if (tableName.Equals("errors"))
            {
                dt.SetColumnsOrder("ID", "CircuitName", "FileContent", "File", "LogDirectory", "Date", "FileState");
                errorsTable = dt;
                tabErrors.IsEnabled = true;
                dataGridErrors.ItemsSource = dt.DefaultView;
            }
            else if (tableName.Equals("statistics"))
            {
                dt.SetColumnsOrder("ID", "CircuitName","ErrorCount","WarningCount","SignalsCount","LogDirectory","Date","FileState");
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
            Diagram diagram = new Diagram();
            if (StatisticReport.errorTypes != null)
            {
                barChartErrors barCharErrors = new barChartErrors();
                BarChartWarnings barChart = new BarChartWarnings();
                barCharErrors.Show();
                barChart.Show();
            }
            diagram.Show();
        }

        private void dataGridStatistics_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dataGridStatistics.SelectedCells.Count > 0)
            {
                DataGridCellInfo cellInfo = dataGridStatistics.SelectedCells[0];
                selectedValue.Text = GetSelectedCellValue(cellInfo);
            }
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

        public void FilterTable(DataTable dataTable,DataGrid dataGrid)
        {
            if (!buttonPressed)
            {
                try
                {
                    dataTable.DefaultView.RowFilter = string.Format("CircuitName Like '{0}'", selectedValue.Text);
                    dataGrid.ItemsSource = dataTable.DefaultView;
                    buttonPressed = true;
                }catch(Exception)
                {
                    MessageBox.Show("Select Circuit name!", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else
            {
                dataTable.DefaultView.RowFilter = string.Empty;
                dataGrid.ItemsSource = dataTable.DefaultView;
                buttonPressed = false;
            }
        }

        private void filter_Click(object sender, RoutedEventArgs e)
        {
            if (tabErrors.IsSelected)
            {
                FilterTable(errorsTable,dataGridErrors);
            }
             else if (tabStatistics.IsSelected)
            {
                FilterTable(statisticsTable, dataGridStatistics);
            }
            else if (tabWarnings.IsSelected)
            {
                FilterTable(warningsTable, dataGridWarnings);
            }
        }

        private void dataGridStatistics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
