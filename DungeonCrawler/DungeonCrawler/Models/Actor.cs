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
            // id's are stored as strings.
            actorItemsCorrespondingToLocation = new List<string> { }; 

            var emumLocationNum = Enum.GetNames(typeof(EquipmentPosition)).Length;             int enumLocNum = Convert.ToInt32(emumLocationNum); // for our design there will be 7 possible positions             for (int i = 0; i <= enumLocNum; i++){ // create a list of 8 so numbers correspond to enum values. item at location 0 is a placeholder.                actorItemsCorrespondingToLocation.Add("placeholder"); // initialize item id to 0 for each location                            }  

        }

        public List<string> actorItemsCorrespondingToLocation; // for inventory of a specific actor

        public EquipmentPosition equipmentPositions { get; set; }

        //Name for actor. Actors can be characters or monsters.
        public string name { get; set; }

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
        public virtual List<Item> Die()
        {
            return null;
        }


        public virtual void EquipItem(Character character, Item item)         {             int itemLocation = (int)item.position;             actorItemsCorrespondingToLocation[itemLocation] = item.Id;
         
        } 
    }
}