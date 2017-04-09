using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mover;
using System.IO;

namespace MoverTests
{
    [TestClass]
    public class MoverTests
    {
        private string testTargetPath;
        private FileMover testFileMover;

        [TestInitialize]
        public void Initialise()
        {
            testTargetPath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Target";
            testFileMover = new FileMover();

            // clean up previously copied file
            if (File.Exists(testTargetPath))
            {
                File.Delete(testTargetPath);
            }
        }

        [TestMethod]
        public void FileCopy_FileCopied_Test()
        {
            // Arrange
            var testSourcePath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Source";
            var testFileName = "01. DJ Jay Rock - Intro.mp3";
            var testDestinationFile = Path.Combine(testSourcePath, testFileName);

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testFileName);

            // Assert
            Assert.IsTrue(File.Exists(testDestinationFile));
        }

        [ExpectedException(typeof(ArgumentNullException), "sourcePath is null or empty")]
        [TestMethod]
        public void FileCopy_ArgumentNullExceptionRaisedOnSourcePath_Test()
        {
            // Arrange
            var testSourcePath = string.Empty;
            var testFileName = "01. DJ Jay Rock - Intro.mp3";

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testFileName);

            // Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "targetPath is null or empty")]
        [TestMethod]
        public void FileCopy_ArgumentNullExceptionRaisedOnTargetPath_Test()
        {
            // Arrange
            var testSourcePath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Source";
            var testFileName = "01. DJ Jay Rock - Intro.mp3";
            testTargetPath = string.Empty;

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testFileName);

            // Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "fileName is null or empty")]
        [TestMethod]
        public void FileCopy_ArgumentNullExceptionRaisedOnFileName_Test()
        {
            // Arrange
            var testSourcePath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Source";
            var testFileName = string.Empty;

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testFileName);

            // Assert
        }

        [ExpectedException(typeof(FileNotFoundException), "sourceFile does not exist")]
        [TestMethod]
        public void FileCopy_ArgumentNullExceptionRaisedOnSourceFile_Test()
        {
            // Arrange
            var testSourcePath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Source";
            var testFileName = "FakeFileName.mp3";

            // Act
            testFileMover.Copy(testSourcePath, testTargetPath, testFileName);

            // Assert
        }
    }
}
