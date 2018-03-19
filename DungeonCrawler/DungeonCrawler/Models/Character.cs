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
        public Item DropItem(EquipmentPosition itemType) 
        {
            if (inventory.ContainsKey(itemType))
            {
                Item toDrop = inventory[itemType];
                inventory.Remove(itemType);
                return toDrop;
            }
            return null;
        }

        //Equips new item if it can. If it cannot equip the item due to the item slot
        //  being filled, will return false.
        public bool EquipItem(Item item) 
        {
            if(inventory.ContainsKey(item.position))
                return false;
            Item toEquip = new Item();
            toEquip.Update(item);
            inventory.Add(toEquip.position, toEquip);
            return true;
        }

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
        private string LevelUp() {
            //set up strings for if any new level stats happen
            string attackUp = "", defenseUp = "", speedUp = "";

            //first grab all the attribute changes
            if (LevelStats.MasterLevelStats.levels[attributes.level].attack > 0)
            {
                attributes.attack += LevelStats.MasterLevelStats.levels[attributes.level].attack;
                attackUp = "Attack Up: +" + LevelStats.MasterLevelStats.levels[attributes.level].attack + "! ";
            }

            if (LevelStats.MasterLevelStats.levels[attributes.level].defense > 0)
            {
                attributes.defense += LevelStats.MasterLevelStats.levels[attributes.level].defense;
                defenseUp = "Defense Up: +" + LevelStats.MasterLevelStats.levels[attributes.level].defense + "! ";
            }

            if (LevelStats.MasterLevelStats.levels[attributes.level].speed > 0)
            {
                attributes.speed += LevelStats.MasterLevelStats.levels[attributes.level].speed;
                speedUp = "Speed Up: +" + LevelStats.MasterLevelStats.levels[attributes.level].speed + "! ";
            }


            //then update level
            attributes.level++;

            //then roll a d10 and add the new health to the character's max health
            //  and current health.
            int additionalHealth = ((d10.Next() % 10) + 1);
            attributes.currentHealth += additionalHealth;
            attributes.health += additionalHealth;


            return "\nLevel Up! Level: " + attributes.level + "! " + attackUp + defenseUp + speedUp + "Health Up: +" +additionalHealth + "!";
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
        public string GainExperience(int experience) {
            //add experience
            attributes.currentExperience += experience;

            string returnMessage = "";
            //Loop while eligable for a level up
            while(CheckLevelUp())
            {
                
                //level up if eligable
                returnMessage += LevelUp();
            }
            return returnMessage;
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
            {
                Item toEquip = new Item();
                toEquip.Update(c.inventory[position]);
                inventory.Add(position, toEquip);
            }
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
                if (item.damage > 0) 
                    damageModifier += (d10.Next() % item.damage) + 1;
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

        public string DeadState()
        {
            var myReturn = string.Empty;
            myReturn += name;
            myReturn += " , " + description;
            myReturn += " , " + attributes.StringOutput();
            myReturn += " , Items : " + ItemSlotsFormatOutput();

            return myReturn;
        }

        private string ItemSlotsFormatOutput()
        {
            var returnString = string.Empty;
            returnString += "[ ";
            foreach(EquipmentPosition key in inventory.Keys)
            {
                returnString += key.ToString() + " :" + inventory[key].ItemString() + ", ";
            }
            returnString += "]";
            return returnString;
        }
       
    }
}