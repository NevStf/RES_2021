using Contracts.Logger;
using Moq;
using NUnit.Framework;

namespace ContractsTest
{
    [TestFixture]
    public class LoggerTest
    {
        [Test]
        [TestCase("Neka poruka")]
        public void WriteToFileMockVerify(string message)
        {
            Mock<ILogger> loggerMock = new Mock<ILogger>();
            // sa verifiable kazemo da hocemo posle da mozemo da verifikujemo
            // da li se poziv zaista desio
            loggerMock.Setup(_logger => _logger.WriteToFile(message)).Verifiable();
            LoggerWrapper helper = new LoggerWrapper(loggerMock.Object);
            helper.WriteToFile(message);
            loggerMock.Verify(_logger => _logger.WriteToFile(message), Times.Once);
        }
    }
}
