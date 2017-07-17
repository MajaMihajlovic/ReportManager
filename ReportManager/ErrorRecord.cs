using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager
{
    public class ErrorRecord : Record
    {
        protected string fileContent;
        protected string file;
        protected string type;

        public ErrorRecord(string circuitName, string log, string date, string fileContent, string file, string fileState) : base(circuitName, log, date, fileState)
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

