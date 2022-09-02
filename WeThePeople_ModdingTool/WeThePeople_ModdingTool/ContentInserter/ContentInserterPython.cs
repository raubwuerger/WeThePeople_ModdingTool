using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool.ContentInserter
{
    public class ContentInserterPython : ContentInserterBase
    {
        private static string DEF = "def ";
        private bool LastLineEmpty = true; 
        public override bool Insert(string content)
        {
            List<string> linesToInsert = SplitToLines(content);
            List<string> defsToInsert = GetOnlyLinesStartWith(DEF,linesToInsert);

            List<string> linesTarget = TextFileUtility.LoadLineByLine(FileName);
            List<string> defsExisting = GetOnlyLinesStartWith(DEF, linesTarget);

            if( true == Contains(defsToInsert, defsExisting) )
            {
                return false;
            }

            LastLineEmpty = IsLastLineEmpty(linesTarget);
            return Append(linesToInsert);
        }

        private List<string> SplitToLines( string content )
        {
            List<string> lines = new List<string>();
            string[] strings = content.Split("\r\n");
            foreach (var line in strings)
            {
                lines.Add(line);
            }
            return lines;
        }

        private List<string> GetOnlyLinesStartWith( string startWith, List<string> lines )
        {
            List<string> startingWith = new List<string>();
            foreach( string line in lines )
            {
                if( line.StartsWith(startWith) )
                {
                    startingWith.Add(line);
                }
            }
            return startingWith;
        }

        private bool Contains( string contains, List<string> lines )
        {
            foreach( string line in lines )
            {
                if( line.Equals(contains) )
                {
                    return true;
                }
            }
            return false;
        }

        private bool Contains( List<string> contains, List<string> lines )
        {
            foreach (string line in contains)
            {
                if (Contains(line, lines) )
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsLastLineEmpty( List<string> lines )
        {
            if( lines.Count == 0 )
            {
                return true;
            }
            string lastLine = lines[lines.Count - 1];
            return StringValidator.IsNullOrEmpty(lastLine);
        }

        private bool Append( List<string> toInsert )
        {
            using (StreamWriter sw = File.AppendText(FileName))
            {
                if( LastLineEmpty == false )
                {
                    sw.WriteLine("\r\n");
                }
                foreach (string line in toInsert)
                {
                    sw.WriteLine(line);
                }
            }
            return true;
        }

        public override bool Insert(XmlNodeList content)
        {
            return false;
        }

    }
}
