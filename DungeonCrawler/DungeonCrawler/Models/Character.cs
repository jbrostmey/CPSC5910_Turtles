using System;
using SQLite;
namespace DungeonCrawler.Models
{
    /* Team: Turtles 
     * Julia Brostmeyer 
     * Bryan Herr 
     * Denny Tran 
     *  
     *  
     * Compiling Implemented code for character: 
         Properties implementation 
        Method stubs implementation */
    public class Character : BaseCharacter
    {
        //Basic constructor. Each character must have a name on creation.
        // Each character will initialize their own d10 on creation.
        public Character() {
            d10 = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
        }

        [PrimaryKey]
        public string Id { get; set; }

        // local variable to hold the levelstats that change on a per-level base.
        private LevelStats _levelStats = new LevelStats();

        //Allows for getting/setting of the character class. Players can choose from
        //  one of 6 character classes. This is where it is stored. May change
        //  to an enum in the future.
        public string characterClass { get; set; }

        //Drops item being held in the specific item type slot
        public Item DropItem(Enum itemType) { return null; }

        //Equips new item if it can. If it cannot equip the item due to the item slot
        //  being filled, will return false.
        public bool EquipItem(Item item) { return false; }

        public string ImageURI { get; set; }

        //Level up, called from GainXP when a level up is needed.
        private void LevelUp() { 
            //first grab all the attribute changes
            attributes.attack += _levelStats.levels[attributes.level].attack;
            attributes.defense += _levelStats.levels[attributes.level].defense;
            attributes.speed += _levelStats.levels[attributes.level].speed;

            //then update level
            attributes.level++;

            //then roll a d10 and add the new health to the character's max health
            //  and current health.
            int additionalHealth = ((d10.Next() % 10) + 1);
            attributes.currentHealth += additionalHealth;
            attributes.health += additionalHealth;

        }

        public override void TakeDamage(int Damage)
        {
           
           // attributes.currentHealth

            // todo: fix take damage
        }

        //logic to check if a character is eligable for a levelup
        private bool CheckLevelUp(){
            //if not max level and has enough xp to level up
            return attributes.level < _levelStats.MaxLevel()
                             && attributes.currentExperience >= _levelStats.levels[attributes.level].currentExperience;
        }

        //Increases xp based on xp passed as parameter. Can call LevelUp when necessary.
        public void GainExperience(int experience) {
            //add experience
            attributes.currentExperience += experience;

            //Loop while eligable for a level up
            while(CheckLevelUp())
            {
                //level up if eligable
                LevelUp();
            }
        }

        ////Item gets passed into this function to update attributes of character
        //public void updateAttributes(Item someItem, bool flag)
        //{
        //    if (flag == true)
        //    {
        //        attributes.defense += someItem.defense;
        //        attributes.attack += someItem.attack;
        //        attributes.speed += someItem.speed;
        //    }
        //    else 
        //    {
        //        attributes.defense -= someItem.defense;
        //        attributes.attack -= someItem.attack;
        //        attributes.speed -= someItem.speed;   
        //    }
        //}

        public void update(Character c)
        {
            
            name = c.name;
            characterClass = c.characterClass;
            description = c.description;
            imageSource = c.imageSource;

            attributes.Update(c.attributes);

            inventory.Clear();
            foreach (EquipmentPosition position in c.inventory.Keys)
                inventory.Add(position,c.inventory[position]);
        }



       
    }
}