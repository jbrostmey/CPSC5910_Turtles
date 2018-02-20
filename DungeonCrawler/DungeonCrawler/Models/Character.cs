using System;
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
    public class Character : Actor
    {
        //Basic constructor. Each character must have a name on creation.
        // Each character will initialize their own d10 on creation.
        public Character() {
            d10 = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
        }


        // local variable to hold the levelstats that change on a per-level base.
        private LevelStats _levelStats = new LevelStats();

        //Allows for getting/setting of the character class. Players can choose from
        //  one of 6 character classes. This is where it is stored. May change
        //  to an enum in the future.
        public string characterClass { get; set; }

        //Inventory size is 8 for all characters. enum will be created for location:number specifications.
        //  Slot 0 is unused.
        public const int inventorySize = 8;

        //This will hold the items the character has equiped. It is publically gettable
        //  to allow views to grab the entire inventory of the characters to display on different screens.
        public Item[] inventory { get; }

        //Drops item being held in the specific item type slot
        public Item DropItem(Enum itemType) { return null; }

        //Equips new item if it can. If it cannot equip the item due to the item slot
        //  being filled, will return false.
        public bool EquipItem(Item item) { return false; }

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

        //Item gets passed into this function to update attributes of character
        public void updateAttributes(Item someItem, bool flag)
        {
            if (flag == true)
            {
                attributes.defense += someItem.defense;
                attributes.attack += someItem.attack;
                attributes.speed += someItem.speed;
            }
            else 
            {
                attributes.defense -= someItem.defense;
                attributes.attack -= someItem.attack;
                attributes.speed -= someItem.speed;   
            }
        }

        public void update(Character c)
        {
            
            name = c.name;
            characterClass = c.characterClass;
            description = c.description;
            imageSource = c.imageSource;

            attributes.Update(c.attributes);

            for (int i = 0; i < inventorySize; i++){
                inventory[i].Update(c.inventory[i]);
            }
        }


/*
 * We may reinstate the following code if/when we start making subclasses.
 * 
        //Calculates damage, taking into account attack stats, attack modifiers, and item attack values
        public int Attack(){}
        //Calculates defense, taking into account defense stats, defense modifiers, and item defense values
        public int Defense(){}
        //Calculates accuracy, taking into account speed stats, speed modifiers, and item speed values.
        public int Accuracy(){}
*/
    }
}