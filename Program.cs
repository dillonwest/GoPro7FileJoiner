using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace GoPro7FileJoiner
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileJoiner = new GoPro7FileJoiner();
            fileJoiner.JoinFilesInDirector(@".\");
        }
    }

    public class GoPro7FileJoiner
    {
        public void JoinFilesInDirector(string fullPath)
        {
            var fileExtension = ".mp4";

            var dir = new DirectoryInfo(fullPath);
            var allmp4Files = dir.GetFiles($"*{fileExtension}", SearchOption.AllDirectories).ToList();

            //last four of filename "GH020131.MP4" ie 0131 is the "series"
            var distinctSeries = allmp4Files.Select(f => f.Name.Substring(4, 4)).Distinct().ToList();
            var hashTable = new Dictionary<string, List<FileInfo>>();
            //arrange the data
            foreach (var series in distinctSeries)
            {
                //get all the files in this series and put them in the hashtable
                var filesInSeries = allmp4Files
                    .Where(f => Regex.Replace(f.Name, fileExtension, string.Empty).Contains(series))
                    .OrderBy(f => f.Name).ToList();

                hashTable[series] = filesInSeries;
            }
            
            //create output directory
            var workingDirectory = Path.Combine(fullPath, $"Gopro7FileJoiner-{Guid.NewGuid().ToString()}");
            var outputDirectory = Path.Combine(workingDirectory, $"output");

            //creates working directory as well
            Directory.CreateDirectory(outputDirectory);
            
            var ffmpegBatchCommands = new List<string>();

             workingDirectory = Path.GetFullPath(workingDirectory);
             outputDirectory = Path.GetFullPath(outputDirectory);

            //create input files and FFMPEG batch commands
            foreach (var series in hashTable.Keys)
            {
                //create input file
                var inputFilePath = Path.Combine(workingDirectory, $"{series}.txt");
                using (var fileWriter = File.CreateText(inputFilePath))
                {
                    foreach (var filePath in hashTable[series])
                    {
                        //these files have already been sorted correctly
                        var line = $"file '{Path.Combine(workingDirectory, filePath.FullName)}'";
                        fileWriter.WriteLine(line);
                    }
                }
                
                //create batchCommand
                var outputFileName = $"{hashTable[series][0].LastWriteTime.ToString("s").Replace(":",".")}_GOPRO7_Series_{series}_({hashTable[series].Count}_clips){fileExtension}";
                var outputFilePath = Path.Combine(outputDirectory, outputFileName);
                ffmpegBatchCommands.Add($"ffmpeg -f concat -safe 0 -i \"{inputFilePath}\" -c copy \"{outputFilePath}\"");
            }

            //create batch file with all the batch commands in it.  This batch file will be ran manually.
            var outputBatchFile = Path.Combine(workingDirectory, "run_me_to_join_files.bat");
            using (var batchFileWriter = File.CreateText(outputBatchFile))
            {
                foreach (var ffMpegBatch in ffmpegBatchCommands)                
                {
                    batchFileWriter.WriteLine(ffMpegBatch);
                }

                batchFileWriter.WriteLine("Pause");
            }
        }
    }
}
