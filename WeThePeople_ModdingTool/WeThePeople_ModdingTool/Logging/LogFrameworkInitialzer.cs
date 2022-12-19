using System;
using Serilog;
using Serilog.Sinks.RichTextBox.Themes;

namespace WeThePeople_ModdingTool.FileUtilities
{
    class LogFrameworkInitialzer
    {
        public static void Init(MainWindow mainWindow)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(GenerateLogFileName(), rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
            CreateInitialLogMessage();
        }

        private static string GenerateLogFileName()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string logFileName = "logs/";
            logFileName += dateTime.ToString("yyyy-MM-dd");
            logFileName += "_";
            logFileName += System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            logFileName += ".log";
            return logFileName;
        }

        private static void CreateInitialLogMessage()
        {
            string initialLogMessage = ">>>>>>>>>> Started logging application: ";
            initialLogMessage += System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            initialLogMessage += ":";
            initialLogMessage += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            initialLogMessage += " <<<<<<<<<<";

            string separatorLine = new string('>', initialLogMessage.Length);

            Log.Information(separatorLine);
            Log.Information(initialLogMessage);
        }
    }
}
