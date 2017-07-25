using System;
using System.Collections.Generic;


namespace ReportManager.LogImporting
{
    public class Summary
    {
        private Dictionary<string, int> countedItems = new Dictionary<string, int>();

        public Dictionary<string, int> CountedItems
        {
            get
            {
                return countedItems;
            }
            set
            {
                countedItems = value;
            }
        }

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
