namespace ReportManager
{
    public class Record
    {
        protected string circuitName { get; private set; }
        protected string log;
        protected string date;
        protected string fileState;

        public Record(string circuitName, string log, string date, string fileState)
        {
            this.circuitName = circuitName;
            this.log = log;
            this.date = date;
            this.fileState = fileState;
        }

        public string GetCircutName()
        {
            return circuitName;
        }

        public string GetLog()
        {
            return log;
        }

        public string GetDate()
        {
            return date;
        }

        public string GetFileState()
        {
            return fileState;
        }

    }
}
