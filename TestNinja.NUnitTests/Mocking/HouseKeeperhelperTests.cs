using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private readonly string statementFileName = "filename";
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _msgBox;
        private HouseKeeperService _service;
        private DateTime statementDate = new DateTime(2017, 1, 1);
        private Housekeeper _housekeeper;


        [SetUp]
        public void SetUp()
        {
             _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(uOw => uOw.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _msgBox = new Mock<IXtraMessageBox>();
            _service = new HouseKeeperService(_unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _msgBox.Object);

        }
        //Interaction Tests
        [Test]
        public void SendStatementEmail_WhenCalled_ShouldGenerateStatements()
        {
            _service.SendStatementEmails(statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, statementDate));
        }

        [Test]
        public void SendStatementEmail_WhereHouseKeeperEmailIsNull_ShouldNotGenerateStatements()
        {
            //arrange
            _housekeeper.Email = null;

            //act
            _service.SendStatementEmails(statementDate);

            //Verify has a 2nd param which can define how many times a method is called. 
            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmail_WhereHouseKeeperEmailIsWhiteSpace_ShouldNotGenerateStatements()
        {
            //arrange
            _housekeeper.Email = " ";

            //act
            _service.SendStatementEmails(statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmail_WhereHouseKeeperEmailIsEmptyString_ShouldNotGenerateStatements()
        {
            //arrange
            _housekeeper.Email = String.Empty;

            //act
            _service.SendStatementEmails(statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmail_WhenCalled_ShouldEmailStatement()
        {
            //arrange
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, statementDate))
                .Returns(statementFileName);

            //act
            _service.SendStatementEmails(statementDate);

            _emailSender.Verify(es => es.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, statementFileName, It.IsAny<string>()));
        }

        [Test]
        public void SendStatementEmail_StatementFileNameIsNull_ShouldNotEmailStatement()
        {
            //arrange
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, statementDate))
                .Returns(() => null);

            //act
            _service.SendStatementEmails(statementDate);

            _emailSender.Verify(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void SendStatementEmail_StatementFileNameIsEmptyString_ShouldNotEmailStatement()
        {
            //arrange
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, statementDate))
                .Returns(() => String.Empty);

            //act
            _service.SendStatementEmails(statementDate);

            _emailSender.Verify(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void SendStatementEmail_StatementFileNameIsWhiteSpace_ShouldNotEmailStatement()
        {
            //arrange
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, statementDate))
                .Returns(() => " ");

            //act
            _service.SendStatementEmails(statementDate);

            _emailSender.Verify(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        

    }
}
