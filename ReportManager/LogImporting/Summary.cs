using System.Collections.Generic;

namespace ReportManager.LogImporting
{
    public class Summary
    {
        public Dictionary<string, int> CountedItems = new Dictionary<string, int>();

        public Summary(int numberOfFiles, int numberOfInvalidExtracts, int numberOfInvalidChangesets, int numberOfPendingChangesets, int numberOfPendingExtracts, int numberOfRejectedChangesets)
        {
            CountedItems.Add("Number Of files", numberOfFiles);
            CountedItems.Add("Number Of Invalid Extracts", numberOfInvalidExtracts);
            CountedItems.Add("Number Of Invalid Changesets", numberOfInvalidChangesets);
            CountedItems.Add("Number Of Pending Extracts", numberOfPendingExtracts);
            CountedItems.Add("Number Of Pending Changesets", numberOfPendingChangesets);
            CountedItems.Add("Number Of Rejected Changesets", numberOfRejectedChangesets);
        }
    }
}
