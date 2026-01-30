using MailContainerTest.Types;
using System.Configuration;

namespace MailContainerTest.Data
{
    /*
     *Implements IMailContainerRepository
     *Handles Datastores internally.
     *Returns non-null Mail Container
     */
    public class MailContainerDataStore : IMailContainerRepository
    {

        public MailContainer GetMailContainer(string mailContainerNumber)
        {
            // Check which data store to use based on configuration
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];
            if (dataStoreType == "Backup")
            {
                var backupStore = new BackupMailContainerDataStore();
                return backupStore.GetMailContainer(mailContainerNumber);
            }
            return new MailContainer();

        }


        public void UpdateMailContainer(MailContainer mailContainer)
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];
            if (dataStoreType == "Backup")
            {
                var backupStore = new BackupMailContainerDataStore();
                backupStore.UpdateMailContainer(mailContainer);
            }
            else
            {
                //default main data store 
            }
        }

        }
    }
