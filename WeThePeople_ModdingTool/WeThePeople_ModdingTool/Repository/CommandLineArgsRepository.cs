using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace WeThePeople_ModdingTool
{
    public sealed class CommandLineArgsRepository
    {
        public static string RUNS_INSIDE_IDE = "RUNS_INSIDE_IDE";

        private static readonly CommandLineArgsRepository instance = new CommandLineArgsRepository();

        static CommandLineArgsRepository()
        {
        }

        public static CommandLineArgsRepository Instance
        {
            get
            {
                return instance;
            }
        }
        
        private List<String> commandLineArgs = new List<string>();

        public List<string> CommandLineArgs
        {
            get => commandLineArgs;
        }

        public bool RegisterCommandLineArgument( string commandLineArg )
        {
            if( commandLineArgs.Contains(commandLineArg) )
            {
                Log.Debug("Command line argument already registered: " + commandLineArg);
                return false;
            }
            Log.Debug("Registered command line argument: " + commandLineArg);
            commandLineArgs.Add(commandLineArg);
            return true;
        }

        public bool HasCommandLineArgument( string commandLineArgument )
        {
            return commandLineArgs.Contains(commandLineArgument);
        }
    }
}
