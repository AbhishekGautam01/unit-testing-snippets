using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public  void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            //this behaviour occours when only the method is called with exact params 
            _fileDownloader.Setup(fr => fr.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadSuccede_ReturnsTrue()
        {
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }

    }
}
