using MailContainerTest.Types;
using MailContainerTest.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MailContainerTest.Tests
{
    [TestClass]
    public class MailTransferValidatorTests
    {
        private MailTransferValidator validator=new MailTransferValidator();
        [TestInitialize]
        public void Setup() => validator = new MailTransferValidator();
        [TestMethod]
        public void ValidateStandardLetterSuccess()
        {
            var source = new MailContainer
            {
                AllowedMailType = AllowedMailType.StandardLetter,
                Capacity = 10,
                Status = MailContainerStatus.Operational
            };
            var destination = new MailContainer
            {
                AllowedMailType = AllowedMailType.StandardLetter,
                Capacity = int.MaxValue,
                Status = MailContainerStatus.Operational
            };

            var request = new MakeMailTransferRequest
            {
                SourceMailContainerNumber = "C1",
                MailType = MailType.StandardLetter,
                NumberOfMailItems = 5
            };
            var result = validator.Validate(source,destination, request);
            Assert.IsTrue(result.Success);

        }
        [TestMethod]
        public void Validate_LargeLetter_Fails_WhenCapacityTooLow()
        {
            var source = new MailContainer
            {
                AllowedMailType = AllowedMailType.LargeLetter,
                Capacity = 2,
                Status = MailContainerStatus.Operational
            };
             var destination = new MailContainer
            {
                AllowedMailType = AllowedMailType.LargeLetter,
                Capacity = int.MaxValue,
                Status = MailContainerStatus.Operational
            };


            var request = new MakeMailTransferRequest
            {
                SourceMailContainerNumber = "C1",
                MailType = MailType.LargeLetter,
                NumberOfMailItems = 5
            };

            var result = validator.Validate(source, destination,request);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void Validate_SmallParcel_Fails_WhenNotOperational()
        {
            var source = new MailContainer
            {
                AllowedMailType = AllowedMailType.SmallParcel,
                Capacity = 10,
                Status = MailContainerStatus.Operational
            };
            var destination = new MailContainer
            {
                AllowedMailType = AllowedMailType.SmallParcel,
                Capacity = int.MaxValue,
                Status = MailContainerStatus.Operational
            };

            var request = new MakeMailTransferRequest
            {
                SourceMailContainerNumber = "C1",
                MailType = MailType.SmallParcel,
                NumberOfMailItems = 1
            };

            var result = validator.Validate(source,destination, request);
            Assert.IsFalse(result.Success);
        }
    }
}
