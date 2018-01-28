﻿using System;
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
        public Character(String name) {
            d10 = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
        }

        // d10 used for calculations with
        private Random d10;

        // local variable to hold the levelstats that change on a per-level base.
        private LevelStats LEVELSTATS = new LevelStats();

        //User must name their character.
        public String name { get; set; }

        //Allows getting of Attributes. No setting: must be done through methods.
        public Attributes attributes { get; }

        //Allows for getting/setting of the character class. Players can choose from
        //  one of 6 character classes. This is where it is stored. May change
        //  to an enum in the future.
        public String characterClass { get; set; }

        //Inventory size is 7 for all characters. enum will be created for location:number specifications.
        public const int inventorySize = 7;

        //This will hold the items the character has equiped. It is publically gettable
        //  to allow views to grab the entire inventory of the characters to display on different screens.
        public Item[] inventory { get; }

        //Drops item being held in the specific item type slot
        public Item DropItem(Enum itemType) { }

        //Equips new item if it can. If it cannot equip the item due to the item slot
        //  being filled, will return false.
        public bool EquipItem(Item item) { }


        //Decreases attributes.currentHealth based on damage parameter.
        //public void TakeDamage(int damage) { }

        //Level up, called from GainXP when a level up is needed.
        private void LevelUp() { 
            //first grab all the attribute changes
            attributes.attack += LEVELSTATS.levels[attributes.level].attack;
            attributes.defense += LEVELSTATS.levels[attributes.level].defense;
            attributes.speed += LEVELSTATS.levels[attributes.level].speed;

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
            return attributes.level < LEVELSTATS.MaxLevel()
                             && attributes.currentExperience >= LEVELSTATS.levels[attributes.level].currentExperience;
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

        //Returns true if the character is still alive.
        public bool IsAlive()
        {
            return attributes.alive;
        }

        //If a character has died, it will drop all of its equipment and return it to
        // the field as an array of items.
        public Item[] Die(){
            
        }
/*
        //Calculates damage, taking into account attack stats, attack modifiers, and item attack values
        public int Attack(){}

        //Calculates defense, taking into account defense stats, defense modifiers, and item defense values
        public int Defense(){}

        //Calculates accuracy, taking into account speed stats, speed modifiers, and item speed values.
        public int Accuracy(){}
*/
    }
}




