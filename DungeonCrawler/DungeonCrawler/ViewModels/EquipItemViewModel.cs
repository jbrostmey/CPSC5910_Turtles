
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using DungeonCrawler.Models;
using DungeonCrawler.Views.Items;
using DungeonCrawler.Views.Scores;

using System.Linq;
using System.Collections.Generic;

namespace DungeonCrawler
{
    public class EquipItemViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static EquipItemViewModel _instance;

        public static EquipItemViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EquipItemViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Character> Dataset { get; set; }
        public ObservableCollection<Item> DatasetItems { get; set; }


        // change dataset items to all items fallen itno fiesled
        // pull items from field, pulling from battle itself.
        // pupulate from field, take in a list of items in field load into dataset. 
        // popuilate everytime round ends , going to round over screen. 
        // call on transiiton from game to gam/ropund over
        // use function to send all items in game to dataset items.
        // inventory should reset. dataset reset with what's coming from the battle field.


        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public EquipItemViewModel()
        {
            Title = "Equip Items";
            //   battlemessage = "tmp";
            Dataset = new ObservableCollection<Character>();
            DatasetItems = new ObservableCollection<Item>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

        }

        public void InitializeItemsCollection( List<Item> items )
        {
            DatasetItems.Clear();
            foreach(var itemNew in items){
                DatasetItems.Add(itemNew);
            }
        }


        public void InitializeCharacterCollection(List<Character> characters)
        {
            Dataset.Clear();
            foreach (var charNew in characters)
            {
                Dataset.Add(charNew);
            }
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

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();

                if (DataStore == null)
                {
                    SetDataStore(DataStoreEnum.Sql); // initialize to sql
                }

                // Get all characters from database
                var dataset = await DataStore.GetAllAsync_Character(true);
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }

                DatasetItems.Clear();

                // Get all items from database
               // var datasetItems = await DataStore.GetAllAsync_Item(true);
              //  foreach (var dataI in BattlePageViewModel.Instance.)
               // {
               //  }
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