using AutoFixture;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTests.Mocking;

namespace UnitTests.Tests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private HouseKeeperService _service;
        private Fake<IStatementGenerator> _statementGenerator;
        private Fake<IEmailSender> _emailSender;
        private Fake<IXtraMessageBox> _messageBox;
        private readonly DateTime _statementDate = new(2017, 1, 1);
        private Housekeeper _houseKeeper;
        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            var fixture = new Fixture();
            _houseKeeper = fixture.Create<Housekeeper>();

            var unitOfWork = new Fake<IUnitOfWork>();
            unitOfWork.CallsTo(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper
            }.AsQueryable());

            _statementFileName = "fileName";
            _statementGenerator = new Fake<IStatementGenerator>();
            _statementGenerator
                .CallsTo(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .ReturnsLazily(() => _statementFileName);

            _emailSender = new Fake<IEmailSender>();
            _messageBox = new Fake<IXtraMessageBox>();

            _service = new HouseKeeperService(
                unitOfWork.FakedObject,
                _statementGenerator.FakedObject,
                _emailSender.FakedObject,
                _messageBox.FakedObject);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.CallsTo(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate)).MustHaveHappened();
        }

        [Test]
        public void SendStatementEmails_HouseKeepersEmailIsNull_ShouldNotGenerateStatement()
        {
            _houseKeeper.Email = null;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.CallsTo(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate)).MustNotHaveHappened();
        }

        [Test]
        public void SendStatementEmails_HouseKeepersEmailIsWhitespace_ShouldNotGenerateStatement()
        {
            _houseKeeper.Email = " ";

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.CallsTo(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate)).MustNotHaveHappened();
        }

        [Test]
        public void SendStatementEmails_HouseKeepersEmailIsEmpty_ShouldNotGenerateStatement()
        {
            _houseKeeper.Email = "";

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.CallsTo(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate)).MustNotHaveHappened();
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailTheStatement()
        {
            _statementFileName = null;

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsEmptyString_ShouldNotEmailTheStatement()
        {
            _statementFileName = "";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsWhitespace_ShouldNotEmailTheStatement()
        {
            _statementFileName = " ";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.CallsTo(es => es.EmailFile(
                A<string>.Ignored,
                A<string>.Ignored,
                A<string>.Ignored,
                A<string>.Ignored
            )).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            _messageBox.CallsTo(mb => mb.Show(A<string>.Ignored, A<string>.Ignored, MessageBoxButtons.OK)).MustHaveHappened();
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.CallsTo(es => es.EmailFile(
                A<string>.Ignored,
                A<string>.Ignored,
                A<string>.Ignored,
                A<string>.Ignored))
                .MustNotHaveHappened();
        }

        private void VerifyEmailSent()
        {
            _emailSender.CallsTo(es => es.EmailFile(
                _houseKeeper.Email,
                _houseKeeper.StatementEmailBody,
                _statementFileName,
                A<string>.Ignored))
                .MustHaveHappened();
        }
    }
}