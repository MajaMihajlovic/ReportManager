using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace ReportManager
{
    public partial class Reports : Window
    {
        private List<string> _reportTypes = new List<string>();


        public Reports(List<string> list)
        {
            InitializeComponent();
            this._reportTypes = list;
            foreach (string s in _reportTypes)
            {
                FillTheTable(s);
            }
        }

        public void FillTheTable(string tableName)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            if (tableName.Equals("warnings")) tabWarnings.IsEnabled = true;

            SQLiteCommand sqlite_cmd = new SQLiteCommand("SELECT * FROM " + tableName, sqlite_conn);
            sqlite_cmd.ExecuteNonQuery();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlite_cmd);
            DataTable dt = new DataTable(tableName);
            dataAdapter.Fill(dt);
            if (tableName.Equals("warnings"))
            {
                tabWarnings.IsEnabled = true;
                dataGridWarnings.ItemsSource = dt.DefaultView;
            }
            else if (tableName.Equals("errors"))
            {
                tabErrors.IsEnabled = true;
                dataGridErrors.ItemsSource = dt.DefaultView;
            }
            else if (tableName.Equals("statistics"))
            {
                tabStatistics.IsEnabled = true;
                dataGridStatistics.ItemsSource = dt.DefaultView;
            }
            dataAdapter.Update(dt);
            sqlite_conn.Close();
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
    }
}
