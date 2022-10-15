namespace ExceptionConditioning.Console;

public static class FileLogger
{
    private static string logFilePath;
    private static string fastlogFilePath;

    static FileLogger()
    {
        logFilePath = Path.Combine(Environment.CurrentDirectory,
                                    string.Format("{0}.txt", DateTime.Now.ToString("yyyy -MM-dd")));

        fastlogFilePath = Path.Combine(Environment.CurrentDirectory,
                                    string.Format("{0}_fast.txt", DateTime.Now.ToString("yyyy -MM-dd")));
    }

    public static void Log(string logMessage)
    {
        logMessage = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - {logMessage}\n";

        if (!File.Exists(logFilePath))
        {
            using var a = File.CreateText(logFilePath);
            a.WriteLine(logMessage);
        }
        else
            File.AppendAllText(logFilePath, logMessage);
    }

    public static void LogFast(string logMessage)
    {
        try
        {
            File.AppendAllText(fastlogFilePath, $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - {logMessage}\n");
        }
        catch (FileNotFoundException)
        {
            using var f = File.Create(fastlogFilePath);
            LogFast(logMessage);
        }
    }
}
