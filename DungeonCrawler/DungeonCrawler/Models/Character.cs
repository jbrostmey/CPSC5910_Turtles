using System;
using System.Collections.Generic;

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
            MiracleMaxLive = true;
            inventory = new Dictionary<EquipmentPosition, Item>();
            attributes = new Attributes();
        }

        public Character(BaseCharacter character)
        {
            d10 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            inventory = new Dictionary<EquipmentPosition, Item>();
            attributes = new Attributes();

            MiracleMaxLive = character.MiracleMaxLive;
            AttributeString = character.AttributeString;
            ImageURI = character.ImageURI;
            characterClass = character.characterClass;
            Id = character.Id;
            name = character.name;
            number = character.number;
            description = character.description;

            this.attributes.PopulateFromString(this.AttributeString);
        }

        //This will hold the items the character has equiped. It is publically gettable
        //  to allow views to grab the entire inventory of the characters to display on different screens.
        public Dictionary<EquipmentPosition, Item> inventory { get; }

        //Allows getting of Attributes. No setting: must be done through methods.
        public Attributes attributes { get; set; }

        //Drops item being held in the specific item type slot
        public Item DropItem(Enum itemType) { return null; }

        //Equips new item if it can. If it cannot equip the item due to the item slot
        //  being filled, will return false.
        public bool EquipItem(Item item) { return false; }

        public void SaveAttributes()
        {
            AttributeString = attributes.AttributeString();
        }

        //Returns true if the character is still alive.
        public bool IsAlive()
        {
            return attributes.alive;
        }

        //Level up, called from GainXP when a level up is needed.
        private void LevelUp() { 
            //first grab all the attribute changes
            attributes.attack += LevelStats.MasterLevelStats.levels[attributes.level].attack;
            attributes.defense += LevelStats.MasterLevelStats.levels[attributes.level].defense;
            attributes.speed += LevelStats.MasterLevelStats.levels[attributes.level].speed;

            //then update level
            attributes.level++;

            //then roll a d10 and add the new health to the character's max health
            //  and current health.
            int additionalHealth = ((d10.Next() % 10) + 1);
            attributes.currentHealth += additionalHealth;
            attributes.health += additionalHealth;

        }

        public void TakeDamage(int Damage)
        {
            attributes.currentHealth -= Damage;
            if (attributes.currentHealth < 1)
                attributes.alive = false;
        }

        //logic to check if a character is eligable for a levelup
        private bool CheckLevelUp(){
            //if not max level and has enough xp to level up
            return attributes.level < LevelStats.MasterLevelStats.MaxLevel()
                             && attributes.currentExperience >= LevelStats.MasterLevelStats.levels[attributes.level].currentExperience;
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

        public int ItemAttackModifier()
        {
            int attackModifier = 0;
            foreach (Item item in inventory.Values)
            {
                attackModifier += item.attack;
            }

            return attackModifier;
        }

        public int ItemDamageModifier()
        {
            int damageModifier = 0;
            foreach (Item item in inventory.Values)
            {
                damageModifier += item.damage;
            }
            return damageModifier;
        }

        public int ItemDefenseModifer()
        {
            int defenseModifier = 0;
            foreach (Item item in inventory.Values)
            {
                defenseModifier += item.defense;
            }
            return defenseModifier;
        }
       
    }
}