using MailContainerTest.Types;

namespace MailContainerTest.Validators
{
    public interface IMailTransferValidator
    {
        MakeMailTransferResult Validate(MailContainer source, MailContainer destination, MakeMailTransferRequest request);
        
    }
}