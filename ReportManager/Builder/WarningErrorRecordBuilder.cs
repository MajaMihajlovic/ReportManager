using ReportManager.Model.Report;
using System.Windows;

namespace ReportManager.Builder
{
    class WarningErrorRecordBuilder : RecordBuilder, IWarningErroRecordBuilder
    {
        private string _path;

        public WarningErrorRecordBuilder(string path) : base(path)
        {
           record = new WarningErrorRecord(path);
            _path = path;
        }

        public WarningErrorRecord WarningErrorRecord
        {
            get { return (WarningErrorRecord) record; }
        }

        public void BuildFileName()
        {
            var array = _path.Split('\\');
            if (array.Length > 0)
            {
               (record as WarningErrorRecord).File = array[array.Length - 1];
            }
            else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BuildFileContent(string content)
        {
            (record as  WarningErrorRecord).FileContent = content.TrimStart();
        }
    }
}
