using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools;
using MailContainerTest.Services;
using MailContainerTest.Data;
using MailContainerTest.Validators;
using MailContainerTest.Types;

namespace MailContainerTest.Tests
{

    [TestClass]
    public class MailContainerServiceTest
    {

        private MailTransferService service;
        [TestInitialize]
        public void Setup()
        {
            // Use the real repository and validator
            var repository = new MailContainerDataStore();
            var validator = new MailTransferValidator();

            service = new MailTransferService(repository, validator);
        }

        [TestMethod]
        public void MakeMailTransfer_StandardLetter_Success()
        {
          
            var request = new MakeMailTransferRequest
            {
                SourceMailContainerNumber = "C1",
                MailType = MailType.StandardLetter,
                NumberOfMailItems = 5
            };

            
            var result = service.MakeMailTransfer(request);

           
            Assert.IsTrue(result.Success, "Standard letter transfer should succeed.");
        }

        [TestMethod]
        public void MakeMailTransfer_SmallParcel_Fails_WhenNotOperational()
        {
            
            // simulate a container that is not operational by overriding the repository
            var repository = new BackupMailContainerDataStoreOverride();
            var validator = new MailTransferValidator();

            var service = new MailTransferService(repository, validator);

            var request = new MakeMailTransferRequest
            {
                SourceMailContainerNumber = "C1",
                MailType = MailType.SmallParcel,
                NumberOfMailItems = 1
            };

            
            var result = service.MakeMailTransfer(request);
            Assert.IsFalse(result.Success, "Small parcel transfer should fail if container not operational.");
        }
    }

    // Helper class to simulate a non-operational container
    public class BackupMailContainerDataStoreOverride : BackupMailContainerDataStore
    {
        public new MailContainer GetMailContainer(string mailContainerNumber)
        {
            return new MailContainer
            {
                MailContainerNumber = mailContainerNumber,
                Capacity = 10,
                Status = MailContainerStatus.Operational, // Not operational
                AllowedMailType = AllowedMailType.SmallParcel
            };
        }
    }
}

    

