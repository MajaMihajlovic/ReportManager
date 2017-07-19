namespace ReportManager.Model
{
    public class WarningRecord : Record
    {
        protected string fileContent;
        protected string file;
        protected string type;

        public WarningRecord(string circuitName, string log, string date, string fileContent, string file, string fileState) : base(circuitName, log, date, fileState)
        {
            this.fileContent = fileContent;
            this.file = file;
        }

       public string GetFileContent()
        {
            return fileContent;
        }

        public string GetFile()
        {
            return file;
        }
    }
}
