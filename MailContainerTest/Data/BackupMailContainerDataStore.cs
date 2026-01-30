using MailContainerTest.Types;

namespace MailContainerTest.Data

{
    public class BackupMailContainerDataStore: IMailContainerRepository
    {
        /*Implements IMailContainerRepository
         * Always returns a non-null MailContainer
         * Simulates backup container with default values 
              Capacity=100
              Status=Operational
              AllowedMailType= all types 
         */
        public MailContainer GetMailContainer(string mailContainerNumber)
        {
            // Access the database and return the retrieved mail container. Implementation not required for this exercise.
            return new MailContainer
            {
                MailContainerNumber = mailContainerNumber,
                Capacity = 100,
                Status = MailContainerStatus.Operational,
                AllowedMailType = AllowedMailType.StandardLetter | AllowedMailType.LargeLetter | AllowedMailType.SmallParcel
            };

        }

        public void UpdateMailContainer(MailContainer mailContainer)
        {
            // Update mail container in the database. Implementation not required for this exercise.
        }
    }
}
