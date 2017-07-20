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
            NumberOfInvalidChangesets = numberOfInvalidChangesets;
            NumberOfInvalidExtracts = numberOfInvalidExtracts;
        }
        
    }
}
