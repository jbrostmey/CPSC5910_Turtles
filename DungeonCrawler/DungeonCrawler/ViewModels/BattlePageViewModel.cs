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
        private int MaxPartySize = 6;

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

        public BattlePageViewModel()
        {
            Title = "Battle Info List";
         //   battlemessage = "tmp";
            Dataset = new ObservableCollection<Character>();
            DatasetMonster = new ObservableCollection<Monster>();
            //LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

        }

        public void NewParty()
        {
            Dataset.Clear();
            for (int i = 0; i < MaxPartySize; i++)
            {
                Dataset.Add(new Character { name = "Select Character", description = "", characterClass = "" });
            }
        }

        public bool ValidParty()
        {
            foreach (Character member in Dataset)
            {
                if (member.name == "Select Character")
                {
                    return false;
                }
            }
            return true;
        }

    }
}