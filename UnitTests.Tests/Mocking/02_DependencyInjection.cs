using Autofac;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using UnitTests.Mocking;

namespace UnitTests.Tests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Fake<IFileReader> _fileReader;
        private Fake<IVideoRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Fake<IFileReader>();
            _repository = new Fake<IVideoRepository>();

            var builder = new ContainerBuilder();

            builder.RegisterInstance(_fileReader.FakedObject)
                .As<IFileReader>()
                .SingleInstance();
            builder.RegisterInstance(_repository.FakedObject)
                .As<IVideoRepository>()
                .SingleInstance();
            builder.RegisterType<VideoService>()
                .AsSelf()
                .SingleInstance();

            var container = builder.Build();
            var scope = container.BeginLifetimeScope();

            _videoService = scope.Resolve<VideoService>();
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.CallsTo(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            _repository.CallsTo(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnAStringWithIdOfUnprocessedVideos()
        {
            _repository.CallsTo(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 },
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}