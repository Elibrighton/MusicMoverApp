using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mover;
using System.IO;

namespace MoverTests
{
    [TestClass]
    public class FileMoverTests
    {
        private string testSourcePath;
        private string testTargetPath;
        private FileMover testFileMover;
        private string testCopyFileName;
        private string testCopyDestinationFile;
        private string testDeleteFileName;
        private string testDeleteSourceFile;
        private string testDeleteDestinationFile;

        [TestInitialize]
        public void Initialise()
        {
            testSourcePath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Source";
            testTargetPath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Target";
            testFileMover = new FileMover();
            testCopyFileName = "01. DJ Jay Rock - Intro.mp3";
            testCopyDestinationFile = Path.Combine(testTargetPath, testCopyFileName);
            testDeleteFileName = "Porky Pig - That's All Folks.mp3";
            testDeleteSourceFile = Path.Combine(testSourcePath, testDeleteFileName);
            testDeleteDestinationFile = Path.Combine(testTargetPath, testDeleteFileName);

            // create target directory if it doesnt exist
            if (!Directory.Exists(testTargetPath))
            {
                Directory.CreateDirectory(testTargetPath);
            }

            // clean up previously copied file
            if (File.Exists(testCopyDestinationFile))
            {
                File.Delete(testCopyDestinationFile);
            }

            // copy file to be deleted
            if (!File.Exists(testDeleteDestinationFile))
            {
                File.Copy(testDeleteSourceFile, testDeleteDestinationFile);
            }
        }

        [TestMethod]
        public void FileCopy_FileCopied_Test()
        {
            // Arrange

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testCopyFileName);

            // Assert
            Assert.IsTrue(File.Exists(testCopyDestinationFile));
        }

        [ExpectedException(typeof(ArgumentNullException), "sourcePath is null or empty")]
        [TestMethod]
        public void FileCopy_ArgumentNullExceptionRaisedOnSourcePath_Test()
        {
            // Arrange
            testSourcePath = string.Empty;

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testCopyFileName);

            // Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "targetPath is null or empty")]
        [TestMethod]
        public void FileCopy_ArgumentNullExceptionRaisedOnTargetPath_Test()
        {
            // Arrange
            testTargetPath = string.Empty;

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testCopyFileName);

            // Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "fileName is null or empty")]
        [TestMethod]
        public void FileCopy_ArgumentNullExceptionRaisedOnFileName_Test()
        {
            // Arrange
            testCopyFileName = string.Empty;

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testCopyFileName);

            // Assert
        }

        [ExpectedException(typeof(FileNotFoundException), "sourceFile does not exist")]
        [TestMethod]
        public void FileCopy_FileNotFoundExceptionRaisedOnSourceFile_Test()
        {
            // Arrange
            testCopyFileName = "FakeFileName.mp3";

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testCopyFileName);

            // Assert
        }

        [TestMethod]
        public void FileDelete_FileDeleted_Test()
        {
            // Arrange

            // Act
            testFileMover.Delete(testTargetPath, testDeleteFileName);

            // Assert
            Assert.IsFalse(File.Exists(testDeleteDestinationFile));
        }

        [ExpectedException(typeof(ArgumentNullException), "targetPath is null or empty")]
        [TestMethod]
        public void FileDelete_ArgumentNullExceptionRaiseOnTargetPath_Test()
        {
            // Arrange
            testTargetPath = string.Empty;

            // Act
            testFileMover.Delete(testTargetPath, testDeleteFileName);

            // Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "fileName is null or empty")]
        [TestMethod]
        public void FileDelete_ArgumentNullExceptionRaiseOnFileName_Test()
        {
            // Arrange
            testDeleteFileName = string.Empty;

            // Act
            testFileMover.Delete(testTargetPath, testDeleteFileName);

            // Assert
        }

        [ExpectedException(typeof(FileNotFoundException), "destinationFile does not exist")]
        [TestMethod]
        public void FileDelete_FileNotFoundExceptionRaiseOnDestinationFile_Test()
        {
            // Arrange
            testDeleteFileName = "FakeFileName.mp3";

            // Act
            testFileMover.Delete(testTargetPath, testDeleteFileName);

            // Assert
        }
    }
}
