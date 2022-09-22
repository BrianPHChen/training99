using UnityEngine;
using System.IO;

public static class CSVManager
{
    private static string directoryName = "Data";
    private static string fileName = "time.csv";
    private static string separator = ",";
    private static string[] headers = new string[1] {"type"};

    private static string timeStampHeader = "time stamp";

#region Interactions
    public static void CreateFile() {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath())) {
            string finalString = "";
            for (int i = 0; i < headers.Length; i++) {
                if (finalString != "") {
                    finalString += separator;
                }
                finalString += headers[i];
            }
            finalString += separator + timeStampHeader;
            sw.WriteLine(finalString);
        }
    }

    public static void AppendToFile(string[] strings) {
        VerifyDirectory();
        VerifyFile();
        using (StreamWriter sw = File.AppendText(GetFilePath())) {
            string finalString = "";
            for (int i = 0; i < strings.Length; i++) {
                if (finalString != "") {
                    finalString += separator;
                }
                finalString += strings[i];
            }
            finalString += separator + GetTimeStamp();
            sw.WriteLine(finalString);
        }

    }
#endregion 

#region Operations
    static void VerifyDirectory() {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }
    }
    static void VerifyFile() {
        string file = GetFilePath();
        if (!File.Exists(file)) {
            CreateFile();
        }
    }
#endregion

#region Queriess
    static string GetDirectoryPath() {
        return Application.dataPath + "/" + directoryName;
    }
    static string GetFilePath() {
        return GetDirectoryPath() + "/" + fileName;
    }
    static string GetTimeStamp() {
        return System.DateTime.Now.ToString();
    }
#endregion
}
