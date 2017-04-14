using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

namespace Mover
{
    public class DirectoryMover : IDirectoryMover
    {
        private Dictionary<string, string> releaseNames;
        private string releaseNamesFile;

        public DirectoryMover(string releaseNamesFile)           
        {
            this.releaseNamesFile = releaseNamesFile;
        }

        public void Copy(string sourcePath, string targetPath)
        {
            if (string.IsNullOrEmpty(sourcePath)) throw new ArgumentNullException("sourcePath is null or empty");
            if (string.IsNullOrEmpty(targetPath)) throw new ArgumentNullException("targetPath is null or empty");
            if (!Directory.Exists(sourcePath)) throw new DirectoryNotFoundException("sourcePath does not exist");

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            if (!Directory.Exists(targetPath)) throw new DirectoryNotFoundException("targetPath does not exist");

            var files = Directory.GetFiles(sourcePath);

            foreach (string file in files)
            {
                var fileName = Path.GetFileName(file);
                IFileMover fileMover = new FileMover();

                try
                {
                    fileMover.Copy(sourcePath, targetPath, fileName);
                }
                // dont supress the errors yet
                //catch(ArgumentNullException ex)
                //{
                //    // log file copy error
                //}
                //catch (DirectoryNotFoundException ex)
                //{
                //    // log file copy error
                //}
                //catch (FileNotFoundException ex)
                //{
                //    // log file copy error
                //}
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Delete(string targetPath)
        {
            if (string.IsNullOrEmpty(targetPath)) throw new ArgumentNullException("targetPath is null or empty");
            if (!Directory.Exists(targetPath)) throw new DirectoryNotFoundException("targetPath does not exist");

            var files = Directory.GetFiles(targetPath);
            var directories = Directory.GetDirectories(targetPath);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string directory in directories)
            {
                Delete(directory);
            }

            Directory.Delete(targetPath, false);

        }



        //public void MoveAll(string sourcePath, string targetPath)
        //{
        //    if (string.IsNullOrEmpty(sourcePath)) throw new ArgumentNullException("sourcePath is null or empty");
        //    if (string.IsNullOrEmpty(targetPath)) throw new ArgumentNullException("targetPath is null or empty");
        //    if (!Directory.Exists(sourcePath)) throw new DirectoryNotFoundException("sourcePath does not exist");

        //    var directories = Directory.GetDirectories(sourcePath);

        //    foreach (string directory in directories)
        //    {
        //        var directoryName = Path.GetFileName(directory);
        //        var targetDirectoryName = Path.Combine(targetPath, directoryName);

        //        if (!Directory.Exists(targetDirectoryName))
        //        {
        //            Directory.CreateDirectory(targetDirectoryName);
        //        }

        //        if (!Directory.Exists(targetPath)) throw new DirectoryNotFoundException("targetDirectoryName does not exist");

        //        MoveAll(directory, targetDirectoryName);
        //    }

        //    Copy(sourcePath, targetPath);
        //}

        public void LoadReleaseNames()
        {
            if (string.IsNullOrEmpty(releaseNamesFile)) throw new ArgumentNullException("releaseNamesFile is null or empty");

            releaseNames = new Dictionary<string, string>();
            var fileStream = new FileStream(releaseNamesFile, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                var line = string.Empty;

                while ((line = streamReader.ReadLine()) != null)
                {
                    var keyValuePair = line.Split('|');
                    var key = string.Empty;
                    var value = string.Empty;

                    if (keyValuePair.Length == 2)
                    {
                        key = keyValuePair[0];
                        value = keyValuePair[1];
                        releaseNames.Add(key, value);
                    }
                }
            }
        }

        public void AddReleaseName(string pattern, string releaseName)
        {
            if (string.IsNullOrEmpty(releaseNamesFile)) throw new ArgumentNullException("releaseNamesFile is null or empty");
            if (string.IsNullOrEmpty(releaseName)) throw new ArgumentNullException("releaseName is null or empty");
            if (string.IsNullOrEmpty(pattern)) throw new ArgumentNullException("pattern is null or empty");
            if (!File.Exists(releaseNamesFile)) throw new FileNotFoundException("releaseNamesFile does not exist");

            using (StreamWriter streamWriter = File.AppendText(releaseNamesFile))
            {
                streamWriter.WriteLine(string.Concat(pattern, "|", releaseName));
            }
        }

        public string MatchReleaseName(string releaseName)
        {
            if (string.IsNullOrEmpty(releaseNamesFile)) throw new ArgumentNullException("releaseNamesFile is null or empty");
            if (string.IsNullOrEmpty(releaseName)) throw new ArgumentNullException("releaseName is null or empty");
            if (releaseName == null) throw new ArgumentNullException("releaseNames is null");
            if (!File.Exists(releaseNamesFile)) throw new FileNotFoundException("releaseNamesFile does not exist");
            
            var matchedReleaseName = string.Empty;

            foreach (var release in releaseNames)
            {
                var pattern = release.Key;

                var match = RegexHelper.GetRegexMatchValue(releaseName, pattern);

                if (!string.IsNullOrEmpty(match))
                {
                    matchedReleaseName = release.Value;
                    break;
                }
            }

            return matchedReleaseName;
        }
    }
}
