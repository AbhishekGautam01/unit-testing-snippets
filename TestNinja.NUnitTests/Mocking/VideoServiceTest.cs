using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking
{
    [TestFixture]
    class VideoServiceTest
    {
        private Mock<IVideoRepository> _videoRepository;
        private Mock<IFileReader> _fileReader;
        private VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _videoRepository = new Mock<IVideoRepository>();
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_videoRepository.Object, _fileReader.Object);
        }
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnsError()
        {
            //Arrange
            _fileReader.Setup(fr => fr.ReadAllText("video.txt")).Returns("");
            //Act
            var result = _videoService.ReadVideoTitle();
            //Assert
            Assert.That(result, Does.Contain("Error").IgnoreCase);
        }

        [Test]
        public void ReadVideoTitle_FileWithVideoInformation_ReturnsVideoTitle()
        {
            //Arrange
            _fileReader.Setup(fr => fr.ReadAllText("video.txt")).Returns("{\r\n  \"id\": \"1\",\r\n  \"title\": \"abc\",\r\n  \"isProcessed\": \"false\"\r\n}\r\n");

            //Act
            var result = _videoService.ReadVideoTitle();

            //Assert
            Assert.That(result, Is.EqualTo("abc"));
        }
        [Test]
        public void GetUnProcessedVideosAsCSV_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            // Arrange
            _videoRepository.Setup(vr => vr.GetUnProcessedVideos()).Returns(new List<Video>());

            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }
        [Test]
        public void GetUnProcessedVideosAsCSV_AFewUnprocessedVideo_ReturnAStringWithIdOfUnproccessedVideo()
        {
            // Arrange
            _videoRepository.Setup(vr => vr.GetUnProcessedVideos()).Returns(new List<Video> {
                new Video{ Id = 1},
                new Video { Id = 2},
                new Video { Id = 3}
            });

            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            // Assert
            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
