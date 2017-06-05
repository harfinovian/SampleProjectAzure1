using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using System.Collections.ObjectModel;
using SampleMobileAzure1;

namespace SampleMobileAzure1
{
    public class SavingManager
    {
        private IMobileServiceTable<Saving> _SavingTable;

        public SavingManager()
        {
            var client = new MobileServiceClient(Constants.ApplicationURL);
            _SavingTable = client.GetTable<Saving>();
        }


        public async Task<List<Saving>> GetSavingAsync2()
        {
            try
            {
                IEnumerable<Saving> Savings = await _SavingTable.ToEnumerableAsync();
                List<Saving> temp = Savings.ToList<Saving>();
                
                return temp;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("@Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<Saving>> GetSavingAsync()
        {
            try
            {
                IEnumerable<Saving> Savings = await _SavingTable.ToEnumerableAsync();
                List<Saving> temp = Savings.ToList<Saving>();
                Debug.WriteLine(temp.ElementAt(0).Description);
                for (int i = 0; i < temp.Count; i++) {
                    Debug.WriteLine(temp.ElementAt(i).Description);
                }
                return new ObservableCollection<Saving>(Savings);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("@Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SaveTaskAsync(Saving Saving)
        {
            if (Saving.Id == null)
            {
                await _SavingTable.InsertAsync(Saving);
            }
            else
            {
                await _SavingTable.UpdateAsync(Saving);
            }
        }
    }
}