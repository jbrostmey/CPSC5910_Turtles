using System;
using System.Collections.Generic;
using SQLite;

namespace DungeonCrawler.Models
{
    /*
     * Base class of Character and Monster class
     */

    public class Actor
    {
        public Actor()
        {
            attributes = new Attributes();
            imageSource = "DapperDino.png";


            // contains item id's for each location
            // there are 7 item locations.
            // Items are stored, correspond to locations.
            actorItemsCorrespondingToLocation = new List<Item> { }; 
             for (int i = 0; i < ENUMLOCATIONS; i++){               
                Item itemNew = new Item();                 actorItemsCorrespondingToLocation.Add(itemNew);                         }

        }
        private int ENUMLOCATIONS = 7;

        public List<Item> actorItemsCorrespondingToLocation; // for inventory of a specific actor

        public EquipmentPosition equipmentPositions { get; set; }

        //Name for actor. Actors can be characters or monsters.
        public string name { get; set; }
        //Holds character/monster number (0-6) since EntityOrder overwrites base order, need to keep track of original ordering. 
        public int number { get; set; } 



        public string imageSource { get; set; }

        //The description for the actor. 
        public string description { get; set; }

        // d10 used for calculations
        public Random d10;

        //Allows getting of Attributes. No setting: must be done through methods.
        public Attributes attributes { get; }

        //Decreases attributes.currentHealth based on damage parameter.
        public virtual void TakeDamage(int damage)
        {
            // todo: character is not taking damage
        }

        //Returns true if the character is still alive.
        public bool IsAlive()
        {
            return attributes.alive;
        }

        //If a character has died, it will drop all of its equipment and return it to
        // the field as an array of items.
        public virtual List<Item> Die(Character character)
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < ENUMLOCATIONS; i++){
                items.Add(character.actorItemsCorrespondingToLocation[i]);
            }
            return items;

        }
        // todo: combine monster and character die
        public virtual List<Item> Die(Monster monster)
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < ENUMLOCATIONS; i++)
            {
                items.Add(monster.actorItemsCorrespondingToLocation[i]);
            }
            return items;

        }

        public virtual void EquipItem(Character character, Item item)         {             int itemLocation = (int)item.position;             actorItemsCorrespondingToLocation[itemLocation-1] = item;
         
        } 
    }
}