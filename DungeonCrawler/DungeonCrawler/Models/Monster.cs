using System;
namespace DungeonCrawler.Models
{
    /* Team: Turtles 
    * Julia Brostmeyer 
    * Bryan Herr 
    * Denny Tran 
    *  
    *  
    * Compiling Implemented code for monster: 
        Properties implementation 
        Method stubs implementation */
    public class Monster : Actor
    {
        //Basic constructor. Each monster must have a name on creation.
        // Each monster will initialize their own d10 on creation.
        public Monster()
        {
            d10 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        }


        // local variable to hold the levelstats that change on a per-level base.
        private LevelStats LEVELSTATS = new LevelStats();

        //Allows for getting/setting of the monster class. Players can choose from
        //  one of 6 monster classes. This is where it is stored. May change
        //  to an enum in the future.
        public String monsterClass { get; set; }

        //Inventory size is 7 for all monsters. enum will be created for location:number specifications.
        public const int inventorySize = 7;

        //This will hold the items the monster has equiped. It is publically gettable
        //  to allow views to grab the entire inventory of the monsters to display on different screens.
        public Item[] inventory { get; }

        //Drops item being held in the specific item type slot
        public Item DropItem(Enum itemType) { }

        //Equips new item if it can. If it cannot equip the item due to the item slot
        //  being filled, will return false.
        public bool EquipItem(Item item) { }

        //Level up, called from GainXP when a level up is needed.
        private void LevelUp()
        {
            //first grab all the attribute changes
            attributes.attack += LEVELSTATS.levels[attributes.level].attack;
            attributes.defense += LEVELSTATS.levels[attributes.level].defense;
            attributes.speed += LEVELSTATS.levels[attributes.level].speed;

            //then update level
            attributes.level++;

            //then roll a d10 and add the new health to the monster's max health
            //  and current health.
            int additionalHealth = ((d10.Next() % 10) + 1);
            attributes.currentHealth += additionalHealth;
            attributes.health += additionalHealth;

        }

        // logic to check if a monster is eligable for a levelup
        private bool CheckLevelUp()
        {
            //if not max level and has enough xp to level up
            return attributes.level < LEVELSTATS.MaxLevel()
                             && attributes.currentExperience >= LEVELSTATS.levels[attributes.level].currentExperience;
        }

        // Increases experience points based on experience points passed as parameter.
        // Can call LevelUp when necessary.
        public void GainExperience(int experience)
        {
            //add experience
            attributes.currentExperience += experience;

            //Loop while eligable for a level up
            while (CheckLevelUp())
            {
                //level up if eligable
                LevelUp();
            }
        }

        // Either damages or kills the character. 
        // Damage inflicted is based on the level and weapon damage.
        public void DamageAndDie(Character character, LevelStats levelDamage, Item weaponDamage)
        {
          // int currentDamage = weaponDamage + levelDamage;
            // if the damage causes the health points of a character to reach 0
            // or below, the character dies.

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