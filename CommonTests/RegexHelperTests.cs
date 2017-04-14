using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Common
{
    [TestClass]
    public class RegexHelperTests
    {
        private string testSearchResponse;

        [TestInitialize]
        public void Initialize()
        {
            testSearchResponse = File.ReadAllText(@"C:\Users\Dj Music\Documents\Visual Studio 2015\Projects\Property Finder App\Supplied files\TestSearchResponse.html");
        }

        [TestMethod]
        public void GetRegexMatchValue_MatchValueReturned_Test()
        {
            // Arrange
            var testText = "11308 total results";
            var testPattern = @"^\d*";
            var expectedMatchValue = 11308;

            // Act
            var actualMatchValue = Convert.ToInt32(RegexHelper.GetRegexMatchValue(testText, testPattern));

            // Assert
            Assert.AreEqual(expectedMatchValue, actualMatchValue);
        }

        [TestMethod]
        public void GetMatchesList_MatchListReturned_Test()
        {
            // Arrange
            var testPattern = @"/property-house-qld-\D*-\d*";
            var expectedMatchListCount = 319;

            // Act
            var actualMatchList = RegexHelper.GetMatchesList(testSearchResponse, testPattern);

            // Assert
            Assert.AreEqual(expectedMatchListCount, actualMatchList.Count);
        }
    }
}
