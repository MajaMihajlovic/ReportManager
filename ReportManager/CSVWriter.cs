using ReportManager.Model;
using System;
using System.Collections.Generic;
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
            StreamWriter sw = new StreamWriter(filePath+"/"+dt.TableName+".csv");
            int iColCount = dt.Columns.Count;
            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(_CSVSeparator);
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write(_CSVSeparator);
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();

        }
    }
}
