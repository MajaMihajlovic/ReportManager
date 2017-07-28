using System.Collections.Generic;

namespace ReportManager.LogImporting
{
    public class Summary
    {
        public List<KeyValue> parts { set; get; }

        public Summary() { }
        public Summary(int numberOfFiles, int numberOfInvalidExtracts, int numberOfInvalidChangesets, int numberOfPendingChangesets, int numberOfPendingExtracts, int numberOfRejectedChangesets)
        {
            parts = new List<KeyValue>();
            parts.Add(new KeyValue("Number Of files", numberOfFiles));
            parts.Add(new KeyValue("Number Of Invalid Extracts", numberOfInvalidExtracts));
            parts.Add(new KeyValue("Number Of Invalid Changesets", numberOfInvalidChangesets));
            parts.Add(new KeyValue("Number Of Pending Extracts", numberOfPendingExtracts));
            parts.Add(new KeyValue("Number Of Pending Changesets", numberOfPendingChangesets));
            parts.Add(new KeyValue("Number Of Rejected Changesets", numberOfRejectedChangesets));
        }
    }
}
