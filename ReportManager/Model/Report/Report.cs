using System;
using System.Windows;

namespace ReportManager.Model
{
    public class Report
    {
  
    public string GetFileState(string log, string s)
    {
        var array = s.Split('\\');
            if (log.Contains("ChangeSet"))
            {
                if (array.Length > 2)
                {
                    return array[array.Length - 3] + " ChangeSet";
                }else
                {
                    MessageBox.Show("Unexpected file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return "";
                }
            }
            else if (log.Contains("Extract"))
            {
                if (array.Length > 2)
                {
                    return array[array.Length - 3] + " Extract";
                }
                else
                {
                    MessageBox.Show("Unexpected file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return "";
                }
            }

            else return "";
    }

    public string GetLogDirectory(string s)
    {
        var array = s.Split('\\');
            if (array.Length > 1)
            {
                return array[array.Length - 2];
            }
            else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
    }

    public string GetDate(string s)
    {
        var array1 = s.Split('_');
            if (array1.Length > 1)
            {
                return array1[array1.Length - 2].Split('\\')[0] + "_" + array1[array1.Length - 1].Split('\\')[0];
            }
            else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
    }

    public string GetFileName(string s)
    {
        var array = s.Split('\\');
            if (array.Length > 0)
            {
                return array[array.Length - 1];
            }
        else
            {
                MessageBox.Show("Unexpexted file content!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
    }

    public string GetCircuitName(String s)
    {
        var circuit = "";
        var name = s.Split('_');
        int i = 3;
        while (!name[i].StartsWith("created"))
            circuit += name[i++] + "_";
        int charLocation = circuit.IndexOf("[", StringComparison.Ordinal);
        if (charLocation != -1)
            return circuit.Substring(0, charLocation);
        else return circuit.TrimEnd('_');
    }
}
}
