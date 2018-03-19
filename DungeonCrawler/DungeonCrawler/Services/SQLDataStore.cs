using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using DungeonCrawler.Services;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
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
            App.Database.CreateTableAsync<BaseCharacter>().Wait();
            App.Database.CreateTableAsync<BaseMonster>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();

        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<BaseCharacter>().Wait();
            App.Database.CreateTableAsync<BaseMonster>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();

        }

        // Delete the Datbase Tables by dropping them
        private void DeleteTables()
        {
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<BaseCharacter>().Wait();
            App.Database.DropTableAsync<BaseMonster>().Wait();
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


            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Base Hat", Description = "Basic hat.", damage = 0, defense = 2, speed = 0, attack = 0, range = 4, position = EquipmentPosition.head });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Base Shirt", Description = "Basic shirt.", damage = 0, defense = 3, speed = 0, attack = 0, range = 4, position = EquipmentPosition.body });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Base Right Hand", Description = "Basic right handed weapon.", damage = 3, defense = 0, speed = 0, attack = 5, range = 4, position = EquipmentPosition.rightHand });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Base Left Hand", Description = "Basic left handed weapon.", damage = 2, defense = 3, speed = 0, attack = 2, range = 4, position = EquipmentPosition.leftHand });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Base Shoes", Description = "Basic shoes.", damage = 0, defense = 1, speed = 2, attack = 0, range = 4, position = EquipmentPosition.feet });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Base Right Finger", Description = "Basic right finger ring.", damage = 1, defense = 1, speed = 0, attack = 2, range = 4, position = EquipmentPosition.rightFinger });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Base Left Finger", Description = "Basic left finger ring.", damage = 2, defense = 2, speed = 0, attack = 1, range = 4, position = EquipmentPosition.leftFinger });

            //Set up our base classes
            Character toBeAdded = new Character { Id = Guid.NewGuid().ToString(), name = "Arnold", description = "Schwarzenegger got nothing on him.", characterClass = "Warrior" };
            toBeAdded.attributes.attack = 4;
            toBeAdded.attributes.alive = true;
            toBeAdded.SaveAttributes();
            await AddAsync_Character(toBeAdded);

            toBeAdded = new Character { Id = Guid.NewGuid().ToString(), name = "Harold", description = "Abused, but a stone wall.", characterClass = "Paladin" };
            toBeAdded.attributes.defense = 4;
            toBeAdded.SaveAttributes();
            await AddAsync_Character(toBeAdded);

            toBeAdded = new Character { Id = Guid.NewGuid().ToString(), name = "ChunChunMaru", description = "Rough childhoods lead to mystical habits.", characterClass = "Wizard" };
            toBeAdded.attributes.attack = 3;
            toBeAdded.attributes.speed = 2;
            toBeAdded.SaveAttributes();
            await AddAsync_Character(toBeAdded);

            toBeAdded = new Character { Id = Guid.NewGuid().ToString(), name = "Cow XD", description = "Lurkin in the shadows, used to do target practice on cow tippers.", characterClass = "Ranger" };
            toBeAdded.attributes.attack = 2;
            toBeAdded.attributes.speed = 3;
            toBeAdded.SaveAttributes();
            await AddAsync_Character(toBeAdded);

            toBeAdded = new Character { Id = Guid.NewGuid().ToString(), name = "Kankles the Chinchilla", description = "Fastest Chinchilla around!", characterClass = "Animal"};
            toBeAdded.attributes.speed = 4;
            toBeAdded.SaveAttributes();
            await AddAsync_Character(toBeAdded);

            toBeAdded = new Character { Id = Guid.NewGuid().ToString(), name = "Laurance", description = "Master of matter and space.", characterClass = "Alchemist" };
            toBeAdded.attributes.attack = 3;
            toBeAdded.attributes.defense = 2;
            toBeAdded.SaveAttributes();
            await AddAsync_Character(toBeAdded);


            //Add monsters to monster list

            // Ewwy Ooze
            Monster monsterToBeAdded = new Monster { Id = Guid.NewGuid().ToString(), 
                description = "Gelatinous goo that seems to corrode and consume any living thing it touches. Eww!", 
                equipmentPositions = EquipmentPosition.finger, 
                name = "Ewwy Ooze" };    
            monsterToBeAdded.attributes.Update(new Attributes { attack = 2, defense = 1, speed = 1, 
                health = 10, currentHealth = 2, 
                currentExperience = LevelStats.MasterLevelStats.levels[1].currentExperience, level = 1, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);

            // change level/ add to documentation?
            // current health?
            // equipment positions?

            // base level, alter the current experience

            // Spider Knight
            monsterToBeAdded = new Monster { Id = Guid.NewGuid().ToString(), 
                description = "Gallant knight of the arachnid order. To him, chivalry is never dead.",
                equipmentPositions = EquipmentPosition.leftHand, name = "Spider Knight" };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 3, defense = 2, speed = 2, health = 3, currentHealth = 3, 
                currentExperience = LevelStats.MasterLevelStats.levels[2].currentExperience, 
                level = 2, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);


            // Spooky Skeleton
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "Though its bones may be brittle, its speed and power pack a punch! Not to mentions its excellent clarinet skills!",
                equipmentPositions = EquipmentPosition.leftHand,
                name = "Spooky Skeleton"
            };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 5, defense = 0, speed = 3, health = 5, currentHealth = 5, 
                currentExperience = LevelStats.MasterLevelStats.levels[3].currentExperience, level = 3, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);


            // Fire Sprite
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "Though its bones may be brittle, its speed and power pack a punch! Not to mentions its excellent clarinet skills!",
                equipmentPositions = EquipmentPosition.leftHand,
                name = "Fire Sprite"
            };
            monsterToBeAdded.attributes.Update(new Attributes {
                attack = 2, defense = 0, speed = 3, health = 4, currentHealth = 4, 
                currentExperience = LevelStats.MasterLevelStats.levels[3].currentExperience, level = 3, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);

            // Goblin Wizard
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "Wizard in training, this goblin tries hard to learn the arcane arts. Armed with her trusty wand, she casts novice spells at any intruder.",
                equipmentPositions = EquipmentPosition.finger,
                name = "Goblin Wizard"
            };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 6, 
                defense = 1, speed = 5, health = 12, currentHealth = 12, 
                currentExperience = LevelStats.MasterLevelStats.levels[4].currentExperience, level = 4, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);

            // Bear Grillz
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "This bear is CRAZY! Armed with portable grill gloves, anyone who approaches risks 3rd degree burns!",
                equipmentPositions = EquipmentPosition.feet,
                name = "Bear Grillz"
            };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 3, 
                defense = 5, speed = 1, health = 20, currentHealth = 20, 
                currentExperience = LevelStats.MasterLevelStats.levels[5].currentExperience, level = 5, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);


            // Mad Duck
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "As crazy as a newly hatched chick, this duck has hit its head one too many times. Armed with its bow and arrows and trusty helmet, this duck is arrow crazy!",
                equipmentPositions = EquipmentPosition.feet,
                name = "Mad Duck"
            };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 3, defense = 2, speed = 6, health = 15, 
                currentHealth = 15, 
                currentExperience = LevelStats.MasterLevelStats.levels[5].currentExperience, level = 5, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);


            // Tim the Timid Goldfish
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "Tim may not be the biggest or bravest, but he certainly packs a punch! Armed with his fishing spear, he is ready to fight even the toughest fisherman!",
                equipmentPositions = EquipmentPosition.body,
                name = "Tim the Timid Goldfish"
            };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 7, defense = 2, speed = 5, health = 25, currentHealth = 25, 
                currentExperience = LevelStats.MasterLevelStats.levels[7].currentExperience, level = 7, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);

            // Dapper Dragon
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "Dapper Dragon is always ready for the wedding bells to ring. Just don't let the bride know where he was during his bachelor party...",
                equipmentPositions = EquipmentPosition.head,
                name = "Dapper Dragon"
            };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 10, defense = 5, speed = 3, health = 40, currentHealth = 40, 
                currentExperience = LevelStats.MasterLevelStats.levels[10].currentExperience, level = 10, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);


            // Snoring Stone Giant
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "Sleeping peacefully, this stone giant loves to snuggle up with its teddy bear.",
                equipmentPositions = EquipmentPosition.head,
                name = "Snoring Stone Giant"
            };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 1, defense = 10, speed = 1, health = 55, currentHealth = 55, 
                currentExperience = LevelStats.MasterLevelStats.levels[12].currentExperience, level = 12, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);

            // Grandmaster Wizard
            monsterToBeAdded = new Monster { Id = Guid.NewGuid().ToString(), 
                description = "Head of the Wizardly College, the Grandmaster Wizard is always keen to experiment on newcomers.", 
                equipmentPositions = EquipmentPosition.leftFinger, 
                name = "Grandmaster Wizard" };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 15, defense = 0, speed = 7, health = 45, currentHealth = 45, 
                currentExperience = LevelStats.MasterLevelStats.levels[15].currentExperience, level = 15, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);

         
            // Rapping Rhino
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "This rhino is too cool for school. In its oversized fleece hoody, it drops rhymes like you have never seen before.",
                equipmentPositions = EquipmentPosition.leftHand,
                name = "Rapping Rhino"
            };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 5, defense = 10, speed = 6, health = 50, currentHealth = 50, 
                currentExperience = LevelStats.MasterLevelStats.levels[15].currentExperience, level = 15, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);

            //Vloging Velociraptor
            monsterToBeAdded = new Monster { Id = Guid.NewGuid().ToString(), 
                description = "Vloging Velociraptor will do ANYTHING for a view on Velocitube.", 
                equipmentPositions = EquipmentPosition.leftFinger, 
                name = "Vloging Velociraptor" };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 8, defense = 5, speed = 5, health = 35, currentHealth = 35, 
                currentExperience = LevelStats.MasterLevelStats.levels[17].currentExperience, level = 17, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);

         
            // Grim Reaper
            monsterToBeAdded = new Monster { Id = Guid.NewGuid().ToString(), 
                description = "The gate keeper between the mortal world and the afterlife. Some say it is the debt collector for the soul.", 
                equipmentPositions = EquipmentPosition.rightHand, 
                name = "Grim Reaper" };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 20, defense = 10, speed = 15, health = 60, 
                currentHealth = 60, currentExperience = LevelStats.MasterLevelStats.levels[20].currentExperience, 
                level = 20, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);

            // Warrior of the Light
            monsterToBeAdded = new Monster
            {
                Id = Guid.NewGuid().ToString(),
                description = "Here to vanquish evil wherever it hides, and let's face it: you haven't been on the best of behaviors.",
                equipmentPositions = EquipmentPosition.leftHand,
                name = "Warrior of the Light"
            };
            monsterToBeAdded.attributes.Update(new Attributes { attack = 50, defense = 25, speed = 30, health = 3, currentHealth = 3, 
                currentExperience = LevelStats.MasterLevelStats.levels[20].currentExperience, 
                level = 20, alive = true });
            monsterToBeAdded.SaveAttributes();
            await AddAsync_Monster(monsterToBeAdded);




            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 111, GameDate = DateTime.Now, AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 222, GameDate = DateTime.Now, AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), ScoreTotal = 333, GameDate = DateTime.Now, AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 444, GameDate = DateTime.Now, AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(),  ScoreTotal = 555, GameDate = DateTime.Now, AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), ScoreTotal = 666 , GameDate = DateTime.Now, AutoBattle = false, TurnNumber = 1, MonsterSlainNumber = 2, ExperienceGainedTotal = 3, CharacterAtDeathList = "death list", MonstersKilledList = "monsters killed", ItemsDroppedList = "sword" });

        }


        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id); 
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
            // Try catch added to catch when looking for something that does not exist in the database.
            try
            {
                var result = await App.Database.GetAsync<Item>(id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Item>().ToListAsync();
            return result;
        }


        // Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            var result = await App.Database.InsertAsync(new BaseCharacter(data));
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var result = await App.Database.UpdateAsync(new BaseCharacter(data));
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var result = await App.Database.DeleteAsync(new BaseCharacter(data));
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Character> GetAsync_Character(string id)
        {
            var result = await App.Database.GetAsync<BaseCharacter>(id);
            return new Character(result);
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var myResult = new List<Character>();

            var DataResult = await App.Database.Table<BaseCharacter>().ToListAsync();
            foreach (var dataBaseItem in DataResult)
            {
                myResult.Add(new Character(dataBaseItem));
            }

            return myResult;
        }


        //Monster
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            var result = await App.Database.InsertAsync(new BaseMonster(data));
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var result = await App.Database.UpdateAsync(new BaseMonster(data));
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var result = await App.Database.DeleteAsync(new BaseMonster(data));
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            var dataBase = await App.Database.GetAsync<BaseMonster>(id);

            var result = new Monster(dataBase);

            return result;
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            var myResult = new List<Monster>();

            var DataResult = await App.Database.Table<BaseMonster>().ToListAsync();
            foreach (var dataBaseItem in DataResult)
            {
                myResult.Add(new Monster(dataBaseItem));
            }

            return myResult;

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

  