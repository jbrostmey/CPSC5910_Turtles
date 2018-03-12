using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonCrawler.Services;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
//using Character = DungeonCrawler.Models.Character;
namespace DungeonCrawler.Services

{

        public sealed class SQLDataStore : IDataStore
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static SQLDataStore _instance;

        public static SQLDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SQLDataStore();
                }
                return _instance;
            }
        }

        private SQLDataStore()
        {

            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<Character>().Wait();
            App.Database.CreateTableAsync<Monster>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<Character>().Wait();
            App.Database.CreateTableAsync<Monster>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();

        }

        // Delete the Datbase Tables by dropping them
        private void DeleteTables()
        {
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<Character>().Wait();
            App.Database.DropTableAsync<Monster>().Wait();
           App.Database.DropTableAsync<Score>().Wait();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonsterViewModel.Instance.SetNeedsRefresh(true);
            CharacterViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

        public void InitializeDatabaseNewTables()
        {
            // Delete the tables
            DeleteTables();

            // make them again
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }

        private async void InitilizeSeedData()
        {
            
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", defense = 1, speed = 2, attack = 3, range = 4, position = EquipmentPosition.body });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", defense = 1, speed = 2, attack = 3, range = 4, position = EquipmentPosition.body });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", defense = 1, speed = 2, attack = 3, range = 4, position = EquipmentPosition.body });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", defense = 1, speed = 2, attack = 3, range = 4, position = EquipmentPosition.body });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", defense = 1, speed = 2, attack = 3, range = 4, position = EquipmentPosition.body });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", defense = 1, speed = 2, attack = 3, range = 4, position = EquipmentPosition.body });


            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), name = "First Character", description = "This is an Character description.", characterClass = "Warrior"});
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), name = "Second Character", description = "This is an Character description.", characterClass = "Paladin"});
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), name = "Third Character", description = "This is an Character description.", characterClass = "Wizard"});
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), name = "Fourth Character", description = "This is an Character description.", characterClass = "Ranger"});
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), name = "Fifth Character", description = "This is an Character description.", characterClass = "Animal"});
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), name = "Sixth Character", description = "This is an Character description.", characterClass = "Alchemist"});
            /*
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "First Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Second Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Third Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Fourth Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Fifth Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Sixth Monster", Description = "This is an Monster description." });
*/

            await AddAsync_Monster(new Monster {Id = Guid.NewGuid().ToString(), description = "monster description", equipmentPositions = EquipmentPosition.body, imageSource = "image source", name = "First Monster name" });
            await AddAsync_Monster(new Monster {Id = Guid.NewGuid().ToString(),  description = "monster description", equipmentPositions = EquipmentPosition.body, imageSource = "image source", name = "Second Monster name" });
            await AddAsync_Monster(new Monster {Id = Guid.NewGuid().ToString(),  description = "monster description", equipmentPositions = EquipmentPosition.body, imageSource = "image source", name = "Third Monster name" });
            await AddAsync_Monster(new Monster {Id = Guid.NewGuid().ToString(),  description = "monster description", equipmentPositions = EquipmentPosition.body, imageSource = "image source", name = "Fourth Monster name" });
            await AddAsync_Monster(new Monster {Id = Guid.NewGuid().ToString(),  description = "monster description", equipmentPositions = EquipmentPosition.body, imageSource = "image source", name = "Fifth Monster name" });
            await AddAsync_Monster(new Monster {Id = Guid.NewGuid().ToString(),  description = "monster description", equipmentPositions = EquipmentPosition.body, imageSource = "image source", name = "Sixth Monster name" });

            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 111, GameDate = new DateTime(), AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 222, GameDate = new DateTime(), AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), ScoreTotal = 333, GameDate = new DateTime(), AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 444, GameDate = new DateTime(), AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 555, GameDate = new DateTime(), AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), ScoreTotal = 666 , GameDate = new DateTime(), AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });

        }

        // Item
        public async Task<bool> AddAsync_Item(Item data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            var result = await App.Database.GetAsync<Item>(id);
            return result;
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Item>().ToListAsync();
            return result;
        }


        // Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Character> GetAsync_Character(string id)
        {
            var result = await App.Database.GetAsync<Character>(id);
            return result;
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Character>().ToListAsync();
            return result;
        }


        //Monster
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            var result = await App.Database.GetAsync<Monster>(id);
            return result;
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Monster>().ToListAsync();
            return result;

        }


        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            var result = await App.Database.GetAsync<Score>(id);
            return result;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Score>().ToListAsync();
            return result;

        }

    }
}

  