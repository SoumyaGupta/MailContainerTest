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
            var source = repository.GetMailContainer(request.SourceMailContainerNumber);
             
            var destination = repository.GetMailContainer(request.DestinationMailContainerNumber);


            var result=validator.Validate(source, destination, request);
            
            if (result.Success)
                {
                source.Capacity -= request.NumberOfMailItems;
                repository.UpdateMailContainer(source);
                destination.Capacity += request.NumberOfMailItems;
               

            }
            return result;


           
        }
    }
}
