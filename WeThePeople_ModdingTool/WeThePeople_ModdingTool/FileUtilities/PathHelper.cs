using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class PathHelper
    {
        public static string AssetPathRelative = @"templates\Assets";
        public static string AssetPathFull = Path.Combine(GetBasePath(), AssetPathRelative);
        public static string AssetPathShort = @"Assets";
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
        public static string GetBasePathReleaseOnly( string pathExtension )
        {
            if (CommandLineArgsRepository.Instance.HasCommandLineArgument(CommandLineArgsRepository.RUNS_INSIDE_IDE))
            {
                return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            }
            else
            {
                return CombinePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, pathExtension);
            }
        }

        public static string GetBasePath_Test()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }
        public static string GetBasePathCombine( string path )
        {
            return Path.Combine(GetBasePath(), path);
        }

        public static string GetFullAssetFileName( string asset )
        {
            string assetPathAbsolute = Path.Combine(GetBasePath(), AssetPathRelative);
            return Path.Combine(assetPathAbsolute, asset);
        }
        public static string GetFullAssetFileName_Test(string asset)
        {
            string assetPathAbsolute = Path.Combine(GetBasePath_Test(), AssetPathRelative);
            return Path.Combine(assetPathAbsolute, asset);
        }


        public static string GetFullAssetPath()
        {
            return Path.Combine(GetBasePath(), AssetPathRelative);
        }

        public static string CombinePathAndFileName( string path, string filename )
        {
            return Path.Combine(path, filename);
        }

        public static string CombineAssetPathShortAndFileName( string filename )
        {
            return Path.Combine( filename, AssetPathShort );
        }

        public static string CombinePath( string pathFirst, string pathSecond )
        {
            return Path.Combine(pathFirst, pathSecond);
        }
        
        public static void CreatePath(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
    }
}
