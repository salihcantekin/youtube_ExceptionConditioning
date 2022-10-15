using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionConditioning.Console;
public static class FileLogger
{
    private static string logFilePath;

    static FileLogger()
    {
        logFilePath = Path.Combine(Environment.CurrentDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
    }

    public static void Log(string logMessage)
    {
        logMessage = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] - {logMessage}\n";

        if (File.Exists(logFilePath)) // maliyet 1
        {
            using var text = File.CreateText(logFilePath); // maliyet 1.1
        }
        else
            File.AppendAllText(logFilePath, logMessage); // maliyet 2
    }

    public static void LogFast(string logMessage)
    {
        logMessage = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] - {logMessage}\n";

        // try catch maliyeti
        try
        {
            File.AppendAllText(logFilePath, logMessage); // maliyet 1
        }
        catch(DirectoryNotFoundException)
        {
            using var text = File.CreateText(logFilePath); // maliyet 1.1
            LogFast(logMessage);
        }
    }

}
