using MailContainerTest.Data;
using MailContainerTest.Types;
using MailContainerTest.Validators;
using System.Configuration;

namespace MailContainerTest.Services
{
    /*Service only 
       fetches container from the repository
       validate container via container
       update container if validation passes
       return result
      *Moved all configurationManager checks to MailContainerDatastore
      *Removed redudant Switch blocks 
     */
    public class MailTransferService : IMailTransferService
        

    {
        private IMailContainerRepository repository;
        private IMailTransferValidator validator;
        public MailTransferService(IMailContainerRepository _repository, IMailTransferValidator _validator)
        {
        repository = _repository;
        validator = _validator;        
        }
        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var mailContainer = repository.GetMailContainer(request.SourceMailContainerNumber);

            var result=validator.Validate(mailContainer, request);
            
            if (result.Success)
                {
                mailContainer.Capacity -= request.NumberOfMailItems;
                repository.UpdateMailContainer(mailContainer);
               

            }
            return result;


           
        }
    }
}
