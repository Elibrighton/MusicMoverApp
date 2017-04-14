using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mover;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace MoverTests
{
    [TestClass]
    public class DirectoryMoverTests
    {
        private string testSourcePath;
        private string testTargetPath;
        private DirectoryMover testDirectoryMover;
        private string testDeletePath;
        private string[] testFiles;
        private string testTargetReleaseNamesFile;
        private string testSourceReleaseNamesFile;

        [TestInitialize]
        public void Initialise()
        {
            testSourceReleaseNamesFile = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\TestSourceReleaseNames.txt";
            testTargetReleaseNamesFile = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\TestTargetReleaseNames.txt";
            testSourcePath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Source";
            testTargetPath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Target";
            testDeletePath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\Delete";
            testDirectoryMover = new DirectoryMover(testTargetReleaseNamesFile);

            // delete target directory if it exists
            if (Directory.Exists(testTargetPath))
            {
                testFiles = Directory.GetFiles(testTargetPath);

                foreach (string testFile in testFiles)
                {
                    File.SetAttributes(testFile, FileAttributes.Normal);
                    File.Delete(testFile);
                }

                Directory.Delete(testTargetPath);
            }

            // create Delete folder if it doesnt exist
            if (!Directory.Exists(testDeletePath))
            {
                Directory.CreateDirectory(testDeletePath);
            }

            // copy files to Delete folder
            testFiles = Directory.GetFiles(testSourcePath);

            foreach (string testFile in testFiles)
            {
                var testFileName = Path.GetFileName(testFile);
                var testCopySourceFile = Path.Combine(testSourcePath, testFileName);
                var testCopyDestinationFile = Path.Combine(testDeletePath, testFileName);

                if (!File.Exists(testCopyDestinationFile))
                {
                    File.Copy(testCopySourceFile, testCopyDestinationFile);
                }
            }

            // delete TestReleaseNames file if it exists
            if (File.Exists(testTargetReleaseNamesFile))
            {
                File.SetAttributes(testTargetReleaseNamesFile, FileAttributes.Normal);
                File.Delete(testTargetReleaseNamesFile);
            }

            // create TestReleaseNames file if it does not exist
            if (!File.Exists(testTargetReleaseNamesFile))
            {
                File.Copy(testSourceReleaseNamesFile, testTargetReleaseNamesFile);
            }
        }

        [TestMethod]
        public void DirectoryCopy_DirectoryCopied_Test()
        {
            // Arrange
            var testSourceFiles = Directory.GetFiles(testSourcePath);

            // Act
            testDirectoryMover.Copy(testSourcePath, testTargetPath);

            // Assert
            Assert.IsTrue(Directory.Exists(testTargetPath));

            var testTargetFiles = Directory.GetFiles(testTargetPath);
            Assert.AreEqual(testSourceFiles.Length, testTargetFiles.Length);

            foreach (string testFile in testSourceFiles)
            {
                var sourceFileName = Path.GetFileName(testFile);
                var testDestinationFile = Path.Combine(testTargetPath, sourceFileName);

                Assert.IsTrue(File.Exists(testDestinationFile));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "sourcePath is null or empty")]
        public void DirectoryCopy_ArgumentNullExceptionRaisedOnSourcePath_Test()
        {
            // Arrange
            testSourcePath = string.Empty;

            // Act
            testDirectoryMover.Copy(testSourcePath, testTargetPath);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "targetPath is null or empty")]
        public void DirectoryCopy_ArgumentNullExceptionRaisedOnTargetPath_Test()
        {
            // Arrange
            testTargetPath = string.Empty;

            // Act
            testDirectoryMover.Copy(testSourcePath, testTargetPath);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException), "sourcePath does not exist")]
        public void DirectoryCopy_DirectoryNotFoundExceptionRaisedOnTargetPath_Test()
        {
            // Arrange
            testSourcePath = @"C:\Users\Dj Music\Documents\visual studio 2015\Projects\MusicMoverApp\TestFiles\FakeSource";

            // Act
            testDirectoryMover.Copy(testSourcePath, testTargetPath);

            // Assert
        }

        [TestMethod]
        public void DirectoryDelete_DirectoryDeleted()
        {
            // Arrange

            // Act
            testDirectoryMover.Delete(testDeletePath);

            // Assert
            Assert.IsFalse(Directory.Exists(testDeletePath));
        }

        //[TestMethod]
        //public void MoveAll()
        //{
        //    // Arrange
        //    var testMoveSourcePath = @"C:\Users\Dj Music\Desktop\Test Electro House";
        //    var testMoveDestinationPath = @"C:\Users\Dj Music\Desktop\Test Music Mover";

        //    // Act
        //    testDirectoryMover.MoveAll(testMoveSourcePath, testMoveDestinationPath);

        //    // Assert
        //}

        //[TestMethod]
        //public void LoadReleaseNames_ReleaseNamesLoaded()
        //{
        //    // Arrange
        //    var expectedCount = 1;
        //    var expectedKey = "Beezo BeeHive";
        //    var expectedValue = @"Beezo\sBeeHive";

        //    // Act
        //    var actualReleaseNames = testDirectoryMover.LoadReleaseNames();

        //    // Assert
        //    Assert.IsFalse(actualReleaseNames == null);
        //    Assert.AreEqual(expectedCount, actualReleaseNames.Count);
        //    Assert.AreEqual(expectedKey, actualReleaseNames.Keys.First());
        //    Assert.AreEqual(expectedValue, actualReleaseNames.Values.First());
        //}

        //[TestMethod]
        //public void AddReleaseName_ReleaseNameAdded()
        //{
        //    // Arrange
        //    var testReleaseName = "9Inch Remix";
        //    var testPattern = @"9Inch\sRemix";
        //    var expectedCount = 2;

        //    // Act
        //    testDirectoryMover.AddReleaseName(testReleaseName, testPattern);

        //    // Assert
        //    var actualReleaseNames = testDirectoryMover.LoadReleaseNames();

        //    Assert.IsFalse(actualReleaseNames == null);
        //    Assert.AreEqual(expectedCount, actualReleaseNames.Count);
        //    Assert.IsTrue(actualReleaseNames.Keys.Contains(testReleaseName));
        //    Assert.IsTrue(actualReleaseNames.Values.Contains(testPattern));
        //}

        [TestMethod]
        public void MatchReleaseName_ReleaseNameMatched_Test()
        {
            // Arrange
            var testReleaseName = "9INCH REMIX - 12 Tracks";
            var expectedMatchedReleaseName = "9Inch Remix";

            // Act
            var actualMatchedReleaseName = testDirectoryMover.MatchReleaseName(testReleaseName);

            // Arrange
            Assert.IsTrue(!string.IsNullOrEmpty(actualMatchedReleaseName));
            Assert.AreEqual(expectedMatchedReleaseName, actualMatchedReleaseName);
        }
    }
}
