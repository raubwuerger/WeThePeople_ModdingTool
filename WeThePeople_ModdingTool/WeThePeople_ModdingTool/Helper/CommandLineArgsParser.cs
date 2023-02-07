using Serilog;
using System;

namespace WeThePeople_ModdingTool
{
    class CommandLineArgsParser
    {
        public static void Parse()
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                CommandLineArgsRepository.Instance.RegisterCommandLineArgument(args[i]);
            }
            Log.Debug("Number command line arguments registered: " + CommandLineArgsRepository.Instance.CommandLineArgs.Count.ToString());
        }
    }
}
