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
        //used in choosing monsters for a round and player party for autoplay
        private Random RNG;

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
            Dataset = new ObservableCollection<Character>();
            DatasetMonster = new ObservableCollection<Monster>();

            RNG = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        }

        //Resets the dataset to an empty party
        public void NewParty()
        {
            Dataset.Clear();
            for (int i = 0; i < MaxPartySize; i++)
            {
                Dataset.Add(new Character { name = "Select Character", description = "", characterClass = "" });
            }
        }

        public void AutoPlayPartyInitialize()
        {
            NewParty();
            Console.WriteLine(CharacterViewModel.Instance.Dataset.Count);
            int nextPartyMemberIndex;
            int itemIndex;
            for (int i = 0; i < MaxPartySize; i++)
            {
                nextPartyMemberIndex = RNG.Next() % CharacterViewModel.Instance.Dataset.Count;
                Dataset[i].update(CharacterViewModel.Instance.Dataset[nextPartyMemberIndex]);
                while(Dataset[i].inventory.Count != Item.NumberSlots)
                {
                    itemIndex = RNG.Next() % ItemsViewModel.Instance.Dataset.Count;
                    while(!Dataset[i].EquipItem(ItemsViewModel.Instance.Dataset[itemIndex])) 
                        itemIndex = RNG.Next() % ItemsViewModel.Instance.Dataset.Count;
                }
            }
        }

        //grabs the average party level for the members who are alive.
        private int GetPartyAverageLevel()
        {
            int level = 0;
            int numberAlive = 0;
            foreach (Character member in Dataset)
            {
                if (member.IsAlive())
                {
                    level += member.attributes.level;
                    numberAlive++;
                }
            }
            return level / numberAlive;
        }

        //grabs monsters randomly who are not too difficult
        public void ResetMonsters()
        {
            Console.WriteLine(MonsterViewModel.Instance.Dataset.Count);
            DatasetMonster.Clear();
            int partyAverageLevel = GetPartyAverageLevel();
            int nextMonsterIndex = RNG.Next() % MonsterViewModel.Instance.Dataset.Count;
            for (int i = 0; i < MaxPartySize; i++)
            {
                nextMonsterIndex = RNG.Next() % MonsterViewModel.Instance.Dataset.Count;
                DatasetMonster.Add(new Monster());
                while(MonsterViewModel.Instance.Dataset[nextMonsterIndex].attributes.level > (partyAverageLevel+2))
                    nextMonsterIndex = RNG.Next() % MonsterViewModel.Instance.Dataset.Count;
                DatasetMonster[i].Update(MonsterViewModel.Instance.Dataset[nextMonsterIndex]);
            }
        }

        //checks to see that all party members have been chosen
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