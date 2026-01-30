using MailContainerTest.Types;

namespace MailContainerTest.Data
{
    /*
     Replaced direct dependency on MailContainerDatastore or BackupMailContainerDatastore 
     Returns non - nullable MailContainer, simplifying service logic 
     */
    public interface IMailContainerRepository
    {
        MailContainer? GetMailContainer(string containerNumber);
        void UpdateMailContainer(MailContainer container);
    }
}