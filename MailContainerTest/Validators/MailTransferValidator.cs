using MailContainerTest.Types;

namespace MailContainerTest.Validators

{
    /*Moved original switch based validation into MailTransferValidator
     * Validates all mail types
     * Checks for 
         MailContainer is not null
         AllowedMailType flags 
         capacity
         operational status 
     * Returns MakeMailTransferResult with Success=  true/false 
     * 
     */
    public class MailTransferValidator : IMailTransferValidator
    {
        public MakeMailTransferResult Validate(MailContainer source,MailContainer destination, MakeMailTransferRequest request)
        {
            var result = new MakeMailTransferResult { Success = true };
            if (source == null || destination == null)
            return new MakeMailTransferResult { Success = false };
            switch (request.MailType)
            {
                case MailType.StandardLetter:
                    if (!source.AllowedMailType.HasFlag(AllowedMailType.StandardLetter) ||
                !destination.AllowedMailType.HasFlag(AllowedMailType.StandardLetter))
             
                    {
                        result.Success = false;
                    }
                    break;

                case MailType.LargeLetter:
                    if (!source.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) ||
                       !destination.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) ||
                        source.Capacity < request.NumberOfMailItems ||
                        destination.Capacity < request.NumberOfMailItems)
                    {
                        result.Success = false;
                    }
                    break;

                case MailType.SmallParcel:
                    if (!source.AllowedMailType.HasFlag(AllowedMailType.SmallParcel) ||
                        !destination.AllowedMailType.HasFlag(AllowedMailType.SmallParcel) ||
                         source.Status != MailContainerStatus.Operational ||
                         destination.Status != MailContainerStatus.Operational)
                    {
                        result.Success = false;
                    }
                    break;
            

             }
            return result;

          }
    }
}