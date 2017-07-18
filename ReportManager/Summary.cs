using System.IO;

namespace ReportManager
{
    class Summary
    {
        private int numberOfFiles;
        private int numberOfInvalidExtracts;
        private int numberOfInvalidChangesets;

        public Summary(int numberOfFiles, int numberOfInvalidExtracts, int numberOfInvalidChangesets)
        {
            this.numberOfFiles = numberOfFiles;
            this.numberOfInvalidChangesets = numberOfInvalidChangesets;
            this.numberOfInvalidExtracts = numberOfInvalidExtracts;
        }
        public void WriteToFile(string path)
        {
                var writer = new StreamWriter(path);
                string line = "Category" + ";" + "Count";
                writer.WriteLine(line);
                writer.Write("Total Number Log Directories Processed;");
                writer.WriteLine(numberOfFiles);
                writer.Write("Total Invalid Extracts;");
                writer.WriteLine(numberOfInvalidExtracts);
                writer.Write("Total Invalid Changesets;");
                writer.WriteLine(numberOfInvalidChangesets);
                writer.Flush();
        }
    }
}
