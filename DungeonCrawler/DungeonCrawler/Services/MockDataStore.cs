
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DungeonCrawler.Models;
using Character = DungeonCrawler.Models.Character;

namespace DungeonCrawler.Services
{
    public sealed class MockDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MockDataStore _instance;

        public static MockDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockDataStore();
                }
                return _instance;
            }
        }

        private List<Item> _itemDataset = new List<Item>();
        private List<Character> _characterDataset = new List<Character>();
        private List<Monster> _monsterDataset = new List<Monster>();
        private List<Score> _scoreDataset = new List<Score>();

        private MockDataStore()
        {
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", defense = 1, speed = 2, attack = 3, range = 4, position = EquipmentPosition.body },
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", defense = 1, speed = 2, attack = 3, range = 4, position = EquipmentPosition.body },
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", defense = 1, speed = 2, attack = 3, range = 4, position = EquipmentPosition.body },
            };

            foreach (var data in mockItems)
            {
                _itemDataset.Add(data);
            }

            var mockCharacters = new List<Character>
            {
                new Character { Id = Guid.NewGuid().ToString(), name = "First Character", description = "This is an Character description.", characterClass = "Warrior"},
                new Character { Id = Guid.NewGuid().ToString(), name = "First Character", description = "This is an Character description.", characterClass = "Warrior"},
            new Character { Id = Guid.NewGuid().ToString(), name = "First Character", description = "This is an Character description.", characterClass = "Warrior" }
            };

            foreach (var data in mockCharacters)
            {
                _characterDataset.Add(data);
            }

            var mockMonsters = new List<Monster>
            {
                new Monster { Id = Guid.NewGuid().ToString(), description = "monster description", equipmentPositions = EquipmentPosition.body, imageSource = "image source", name = "First Monster name" },
                new Monster { Id = Guid.NewGuid().ToString(), description = "monster description", equipmentPositions = EquipmentPosition.body, imageSource = "image source", name = "First Monster name" },
                new Monster { Id = Guid.NewGuid().ToString(), description = "monster description", equipmentPositions = EquipmentPosition.body, imageSource = "image source", name = "First Monster name"}
            };

            foreach (var data in mockMonsters)
            {
                _monsterDataset.Add(data);
            }

            var mockScores = new List<Score>
            {
                new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 111, GameDate = DateTime.Now, AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword"},
                new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 111, GameDate = DateTime.Now, AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword"},
                new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 111, GameDate = DateTime.Now, AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword"}
            };

            foreach (var data in mockScores)
            {
                _scoreDataset.Add(data);
            }

        }

        // Item
        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
        }


        // Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            _characterDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _characterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Character> GetAsync_Character(string id)
        {
            return await Task.FromResult(_characterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            return await Task.FromResult(_characterDataset);
        }


        //Monster
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            _monsterDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _monsterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            return await Task.FromResult(_monsterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            return await Task.FromResult(_monsterDataset);
        }

        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            _scoreDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _scoreDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            return await Task.FromResult(_scoreDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            return await Task.FromResult(_scoreDataset);
        }

    }
}
