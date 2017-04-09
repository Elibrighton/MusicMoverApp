using System;
using System.IO;

namespace Mover
{
    public class FileMover
    {
        public void Copy(string sourcePath, string targetPath, string fileName)
        {
            if (string.IsNullOrEmpty(sourcePath)) throw new ArgumentNullException("sourcePath is null or empty");
            if (string.IsNullOrEmpty(targetPath)) throw new ArgumentNullException("targetPath is null or empty");
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName is null or empty");
            if (!Directory.Exists(sourcePath)) throw new DirectoryNotFoundException("sourcePath does not exist");
            if (!Directory.Exists(targetPath)) throw new DirectoryNotFoundException("targetPath does not exist");
            
            var sourceFile = Path.Combine(sourcePath, fileName);
            var destinationFile = Path.Combine(targetPath, fileName);

            if (!File.Exists(sourceFile)) throw new FileNotFoundException("sourceFile does not exist");

            File.Copy(sourceFile, destinationFile, true);

            if (!File.Exists(destinationFile)) throw new FileNotFoundException("destinationFile does not exist");
        }
    }
}
