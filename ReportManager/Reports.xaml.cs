using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Forms;

namespace ReportManager
{
    public partial class Reports : Window
    {
       private List<string> _reportTypes = new List<string>();
       private string _pathForSaving;
       private PrepareData helper = null;


        public Reports(List<string> list, PrepareData helper)
        {
            InitializeComponent();

            this._reportTypes = list;
            this.helper = helper;

            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            if (_reportTypes.Contains("ERRORS"))
            {
                tabErr.IsEnabled = true;
                SQLiteCommand sqlite_cmd = new SQLiteCommand("SELECT * FROM errors ", sqlite_conn);
                sqlite_cmd.ExecuteNonQuery();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlite_cmd);
                DataTable dt = new DataTable("Errors");
                dataAdapter.Fill(dt);
                dataGridErr.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);
            }
            if (_reportTypes.Contains("WARNINGS"))
            {
                tabWar.IsEnabled = true;
                SQLiteCommand sqlite_cmd = new SQLiteCommand("SELECT * FROM warnings ", sqlite_conn);
                sqlite_cmd.ExecuteNonQuery();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlite_cmd);
                DataTable dt = new DataTable("Warnings");
                dataAdapter.Fill(dt);
                dataGridWar.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);
            }
             if (_reportTypes.Contains("STATISTICS")){
                tabStat.IsEnabled = true;
                SQLiteCommand sqlite_cmd = new SQLiteCommand("SELECT * FROM statistics ", sqlite_conn);
                sqlite_cmd.ExecuteNonQuery();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlite_cmd);
                DataTable dt = new DataTable("Statistics");
                dataAdapter.Fill(dt);
                dataGridStat.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);
            }
            sqlite_conn.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();
            _pathForSaving = fbd.SelectedPath.Replace("\\", "/");
            var csvWriter = new WriteToCSV();
            if (_reportTypes.Contains("WARNINGS")) csvWriter.WriteWarningRecords(helper.MakeWarnings(),_pathForSaving);
            if (_reportTypes.Contains("ERRORS"))  csvWriter.WriteErrorRecords(helper.MakeErrors(),_pathForSaving);
            if (_reportTypes.Contains("STATISTICS")) csvWriter.WriteStatisticRecords(helper.MakeStatistics(),_pathForSaving);
            System.Windows.Forms.MessageBox.Show("Reports are saved!","Info", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Diagram diagram = new ReportManager.Diagram();
            diagram.Show();
        }

        private void dataGridStat_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void dataGridErr_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void dataGridWar_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
