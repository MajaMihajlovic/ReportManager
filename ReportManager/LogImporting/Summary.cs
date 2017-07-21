namespace ReportManager.LogImporting
{
    public class Summary
    {
        public int NumberOfFiles { get; private set; }
        public int NumberOfInvalidExtracts { get; private set; }
        public int NumberOfInvalidChangesets { get; private set; }
        public int NumberOfPendingExtracts { get; private set; }
        public int NumberOfPendingChangesets { get; private set; }
        public int NumberOfRejectedChangesets { get; private set; }

        public Summary(int numberOfFiles, int numberOfInvalidExtracts, int numberOfInvalidChangesets,int numberOfPendingChangesets,int numberOfPendingExtracts, int numberOfRejectedChangesets)
        {
            NumberOfFiles = numberOfFiles;
            NumberOfInvalidChangesets = numberOfInvalidChangesets;
            NumberOfInvalidExtracts = numberOfInvalidExtracts;
            NumberOfPendingChangesets = numberOfPendingChangesets;
            NumberOfPendingExtracts = numberOfPendingExtracts;
            NumberOfRejectedChangesets = numberOfRejectedChangesets;
}
        
    }
}
