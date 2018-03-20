using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using DungeonCrawler.Models;
using DungeonCrawler.Views.Items;
using DungeonCrawler.Views.Scores;

using System.Linq;

namespace DungeonCrawler
{
    public class ItemsViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static ItemsViewModel _instance;
        private static Random RNG;

        public static ItemsViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemsViewModel();
                    RNG = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                }
                return _instance;
            }
        }

        // items dataset
        public ObservableCollection<Item> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public ItemsViewModel()
        {
            Title = "Item List";
            Dataset = new ObservableCollection<Item>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            // to process updating, deleting, adding items
            MessagingCenter.Subscribe<DeleteItemPage, Item>(this, "DeleteData", async (obj, data) =>
            {
                Dataset.Remove(data);
                await DataStore.DeleteAsync_Item(data);
            });

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddData", async (obj, data) =>
            {
                Dataset.Add(data);
                await DataStore.AddAsync_Item(data);
            });

            MessagingCenter.Subscribe<EditItemPage, Item>(this, "EditData", async (obj, data) =>
            {
                // Find the Item, then update it
                var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
                if (myData == null)
                {
                    return;
                }

                myData.Update(data);
                await DataStore.UpdateAsync_Item(myData);

                _needsRefresh = true;

            });
        }

        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }


        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {
            if (data.Id == null)
            {
                Console.WriteLine("data id is null! "); // yes
            }

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id); //here
            if (oldData == null)
            {
                // If it does not exist, add it to the DB
                var InsertResult = await App.Database.InsertAsync(data);
                if (InsertResult == 1)
                {
                    return true;
                }
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                return true;
            }

            return false;
        }

        // update in database
        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

         // get item from database
        public async Task<Item> GetAsync_Item(string id)

        {
            // Try catch added for when searching for something that does not exist in the database.
            try
            {
                BattlePageViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);

                var result = await App.Database.GetAsync<Item>(id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Copys a random item from the dataset
        public Item RandomItem()
        {
            if (Dataset.Count == 0)
                InitializeDataset();
            if (Dataset.Count == 0)
                return new Item();
            int randomIndex = RNG.Next() % Dataset.Count;
            Item returnItem = new Item();
            returnItem.Update(Dataset[randomIndex]);

            Console.WriteLine(returnItem);
            Console.WriteLine(returnItem.Text);
            return returnItem;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }


         
        public async void InitializeDataset()
        {
            await ExecuteLoadDataCommand();
        }

        // loads data from the database
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var dataset = await DataStore.GetAllAsync_Item(true);
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            finally
            {
                IsBusy = false;
            }
        }
    }
}


 