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

        //max party size for this set of game rules
        private int MaxPartySize = 6;

        //used in choosing monsters for a round and player party for autoplay
        public static Random RNG { get; set; }

        //singleton
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

        //a games set of datasets for parties and on the floor items
        public ObservableCollection<Character> Dataset { get; set; }
        public ObservableCollection<Monster> DatasetMonster { get; set; }
        public ObservableCollection<Item> DatasetItem { get; set; }

        //used for grabbing all the items
        public Command LoadDataCommand { get; set; }

        public BattlePageViewModel()
        {
            //title for the pages using this view
            Title = "Battle Info List";

            //initalize datasets
            Dataset = new ObservableCollection<Character>();
            DatasetMonster = new ObservableCollection<Monster>();
            DatasetItem = new ObservableCollection<Item>();

            //set load command
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            //set up RNG
            RNG = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        }

        //Resets the dataset to an empty party
        public void NewParty()
        {
            Dataset.Clear();
            // creates new characters to update from the master list of characters
            for (int i = 0; i < MaxPartySize; i++)
            {
                Dataset.Add(new Character { name = "Select Character", description = "", characterClass = "" });
            }
        }

        // initializes a party randomly for autoplay from the main menu
        public void AutoPlayPartyInitialize()
        {
            //creates the new party
            NewParty();
            int nextPartyMemberIndex;
            //attempts to avoid Divide by Zero errors
            if (CharacterViewModel.Instance.Dataset.Count != 0)
                for (int i = 0; i < MaxPartySize; i++)
                {
                //randomly chooses characters to copy from the master list of characters
                    nextPartyMemberIndex = RNG.Next() % CharacterViewModel.Instance.Dataset.Count;
                    Dataset[i].update(CharacterViewModel.Instance.Dataset[nextPartyMemberIndex]);
                }
            // give all characters random equipment
            GivePartyEquipment();
        }

        // Foreach character in the party, gives them all equipment to start off with
        public void GivePartyEquipment()
        {
            int itemIndex;
            //for each member
            for (int i = 0; i < MaxPartySize; i++)
                //while they do not have a full set of gear
                while (Dataset[i].inventory.Count != Item.NumberSlots)
                {
                //randomly choose an item, if they have a piece in that location already, loop until you find one they dont have
                    itemIndex = RNG.Next() % ItemsViewModel.Instance.Dataset.Count;
                    while (!Dataset[i].EquipItem(ItemsViewModel.Instance.Dataset[itemIndex]))
                    {
                    //goes forward by one index to avoid long wait times, as we guarantee in our sql initialization that they have one of every piece of equipment
                        itemIndex = (itemIndex + 1) % ItemsViewModel.Instance.Dataset.Count;
                    }
                }
        }
        //grabs the average party level for the members who are alive.
        private int GetPartyAverageLevel()
        {
            int level = 0;
            int numberAlive = 0;
            //count alive members and get their levels
            foreach (Character member in Dataset)
            {
                if (member.IsAlive())
                {
                    level += member.attributes.level;
                    numberAlive++;
                }
            }
            // avoid divide by zero
            if (numberAlive == 0) 
                return level;
            //truncated value
            return level / numberAlive;
        }

        //grabs monsters randomly who are not too difficult
        public void ResetMonsters()
        {
            //clear out the dead monsters
            DatasetMonster.Clear();
            //get the party level to only get monsters who are not too much higher than average level
            int partyAverageLevel = GetPartyAverageLevel();
            //will crash if no monsters...
            int nextMonsterIndex;
            for (int i = 0; i < MaxPartySize; i++)
            {
                //generates a new random index
                nextMonsterIndex = RNG.Next() % MonsterViewModel.Instance.Dataset.Count;
                DatasetMonster.Add(new Monster()); //adds a monster to update with the random monster
                //while too high of a level, choose a new monster
                while(MonsterViewModel.Instance.Dataset[nextMonsterIndex].attributes.level > (partyAverageLevel+2))
                    nextMonsterIndex = RNG.Next() % MonsterViewModel.Instance.Dataset.Count;
                //copy the monster from the master monster list
                DatasetMonster[i].Update(MonsterViewModel.Instance.Dataset[nextMonsterIndex]);
            }
        }

        //checks to see that all party members have been chosen
        public bool ValidParty()
        {
            foreach (Character member in Dataset)
            {
                //assuming no one makes a custom character named "Select Character"
                if (member.name == "Select Character")
                {
                    return false;
                }
            }
            return true;
        }

        //used for loading items?
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DatasetItem.Clear();
                var dataset = await DataStore.GetAllAsync_Item(true);
                foreach (var data in dataset)
                {
                    DatasetItem.Add(data);
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