using System.Collections.Generic;
using System.IO;

namespace ReportManager.LogImporting
{
    public class Summary
    {
        public int NumberOfFiles { get; private set; }
        public int NumberOfInvalidExtracts { get; private set; }
        public int NumberOfInvalidChangesets { get; private set; }

        public Summary(int numberOfFiles, int numberOfInvalidExtracts, int numberOfInvalidChangesets)
        {
            NumberOfFiles = numberOfFiles;
            this.NumberOfInvalidChangesets = numberOfInvalidChangesets;
            this.NumberOfInvalidExtracts = numberOfInvalidExtracts;
        }
        
    }
}
