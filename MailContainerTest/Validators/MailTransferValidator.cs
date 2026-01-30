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
        public MakeMailTransferResult Validate(MailContainer mailContainer, MakeMailTransferRequest request)
        {
            var result = new MakeMailTransferResult { Success = true };
            switch (request.MailType)
            {
                case MailType.StandardLetter:
                    if (mailContainer == null || !mailContainer.AllowedMailType.HasFlag(AllowedMailType.StandardLetter))
                    {
                        result.Success = false;
                    }
                    break;

                case MailType.LargeLetter:
                    if (mailContainer == null ||
                        !mailContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) ||
                        mailContainer.Capacity < request.NumberOfMailItems)
                    {
                        result.Success = false;
                    }
                    break;

                case MailType.SmallParcel:
                    if (mailContainer == null ||
                        !mailContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel) ||
                        mailContainer.Status != MailContainerStatus.Operational)
                    {
                        result.Success = false;
                    }
                    break;
            

             }
            return result;

          }
    }
}