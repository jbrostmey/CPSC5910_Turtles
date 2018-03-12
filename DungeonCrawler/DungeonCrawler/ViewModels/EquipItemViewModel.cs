
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
    public class EquipItemViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static EquipItemViewModel _instance;

        //   public string battlemessage = "dkjfakjldffjd";

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

                var dataset = await DataStore.GetAllAsync_Character(true);
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }

                DatasetItems.Clear();
                var datasetItems = await DataStore.GetAllAsync_Item(true);
                foreach (var dataI in datasetItems)
                {
                    DatasetItems.Add(dataI);
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