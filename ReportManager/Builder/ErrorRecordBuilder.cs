using ReportManager.Model;
using System.Windows;

namespace ReportManager.Builder
{
    class ErrorRecordBuilder : RecordBuilder, IErrorRecordBuilder
    {
        private string _path;

        public ErrorRecordBuilder(string path) : base(path)
        {
           record = new ErrorRecord(path);
            _path = path;
        }

        public ErrorRecord ErrorRecord
        {
            get { return (ErrorRecord) record; }
        }

        public void BuildFileName()
        {
            var array = _path.Split('\\');
            if (array.Length > 0)
            {
               (record as ErrorRecord).File = array[array.Length - 1];
            }
            else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BuildFileContent(string content)
        {
            (record as  ErrorRecord).FileContent = content.TrimStart();
        }
    }
}
