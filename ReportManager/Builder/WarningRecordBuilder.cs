using ReportManager.Builder;
using ReportManager.Model;
using System.Windows;
using System;

namespace ReportManager
{
    class WarningRecordBuilder : RecordBuilder, IWarningRecordBuilder
    {
        private string _path;

        public WarningRecordBuilder(string path) : base(path)
        {
            record = new WarningRecord(path);
            _path = path;
        }

        public WarningRecord WarningRecord
        {
            get { return (WarningRecord)record; }
        }

        public void BuildFileName()
        {
            var array = _path.Split('\\');
            if (array.Length > 0)
            {
                (record as WarningRecord).File = array[array.Length - 1];
            }
            else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BuildFileContent(string content)
        {
            (record as WarningRecord).FileContent = content.TrimStart();
        }
    }
}
