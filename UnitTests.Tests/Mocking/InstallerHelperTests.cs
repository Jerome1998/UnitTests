using FakeItEasy;
using NUnit.Framework;
using System.Net;
using UnitTests.Mocking;

namespace UnitTests.Tests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Fake<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Fake<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.FakedObject);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            _fileDownloader.CallsTo(fd =>
                fd.DownloadFile(A<string>.Ignored, A<string>.Ignored))
                .Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnTrue()
        {
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }
    }
}