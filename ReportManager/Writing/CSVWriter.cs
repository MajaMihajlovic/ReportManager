using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows;

namespace ReportManager
{
    public class CSVWriter
    {
        private string _CSVSeparator = ";";

        public void CreateCSVFile(DataTable dt, string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath + "/" + dt.TableName + ".csv"))
                {
                    int iColCount = dt.Columns.Count;
                    for (int i = 0; i < iColCount; i++)
                    {
                        sw.Write(dt.Columns[i] + _CSVSeparator);
                    }
                    sw.Write(sw.NewLine);
                    List<string> row = new List<string>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i < iColCount; i++)
                        {
                            if (!Convert.IsDBNull(dr[i]))
                            {
                                row.Add(dr[i].ToString());
                            }
                            else
                            {
                                row.Add("");
                            }
                        }
                        sw.WriteLine(string.Join(_CSVSeparator, row));
                        row.Clear();
                    }
                }
            }catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
