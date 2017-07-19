using System;

namespace ReportManager.Model
{
    public class Report
    {
  
    public string GetFileState(string log, string s)
    {
        String[] array = s.Split('\\');
        if (log.Contains("ChangeSet"))
            return array[array.Length - 3] + " ChangeSet";
        else if (log.Contains("Extract"))
            return array[array.Length - 3] + " Extract";
        else return "";
    }

    public string GetLogDirectory(string s)
    {
        string[] array = s.Split('\\');
        return array[array.Length - 2];
    }

    public string GetDate(string s)
    {
        string[] array1 = s.Split('_');
        return array1[array1.Length - 2].Split('\\')[0] + "_" + array1[array1.Length - 1].Split('\\')[0];
    }

    public string GetFileName(string s)
    {
        string[] array = s.Split('\\');
        return array[array.Length - 1];
    }

    public static string GetCircuitName(String s)
    {
        var circuit = "";
        String[] name = s.Split('_');
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
