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
    public class BattlePageViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static BattlePageViewModel _instance;

     //   public string battlemessage = "dkjfakjldffjd";

        public static BattlePageViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattlePageViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Character> Dataset { get; set; }
        public ObservableCollection<Monster> DatasetMonster { get; set; }

        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public BattlePageViewModel()
        {
            Title = "Battle Info List";
         //   battlemessage = "tmp";
            Dataset = new ObservableCollection<Character>();
            DatasetMonster = new ObservableCollection<Monster>();
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

                DatasetMonster.Clear();
                var datasetMonster = await DataStore.GetAllAsync_Monster(true);
                foreach (var dataM in datasetMonster)
                {
                    DatasetMonster.Add(dataM);
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