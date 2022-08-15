using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeThePeople_ModdingTool.FileUtilities
{
    class PathHelper
    {
        public static string GetBasePath()
        {
            if( CommandLineArgsRepository.Instance.HasCommandLineArgument(CommandLineArgsRepository.RUNS_INSIDE_IDE) )
            {
                return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            }
            else 
            {
                return Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            }
        }
    }
}
