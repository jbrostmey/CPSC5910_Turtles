using System;
using Xamarin.Forms;
using SQLite;
using DungeonCrawler.Models;
namespace DungeonCrawler
{
    /* Team: Turtles 
    *  Julia Brostmeyer 
    *  Bryan Herr 
    *  Denny Tran 
    *  
    * Compiling Implemented code for Item: 
      Properties implementation 
      Method stubs implementation */

    public class Item
    {
        [PrimaryKey]
        public string Id { get; set; } // name of item
        public string Text { get; set; }
        public string Description { get; set; }

        // Attributes of item
       // public int defense;
       // public int speed;
       // public int attack;

        public int defense{ get; set; }
        public int speed{ get; set; }
        public int attack{ get; set; }

        //Base attributes
        public int range { get; set; }
        public EquipmentPosition position { get; set; }


        // To determine where the item goes, just an example of a equip on the body
        public EquipmentPosition ItemLocation(Item item)
        {
            EquipmentPosition equipPos = EquipmentPosition.body;
            return equipPos;
        }

        // To determine what the item modifies on the character.
        // This function calls the updateAttr function in character to update character attributes
        // Takes in the item and current character object
        public void ItemEquipped(Item someItem, Character currentChar)
        {
            bool flag = true;
            currentChar.updateAttributes(someItem, flag);
        }

        //If flag is false, then we remove the item from the character and update attributes
        public void ItemRemoved(Item someItem, Character currentChar)
        {
            bool flag = false;
            currentChar.updateAttributes(someItem, flag);
        }

        //Get attributes of current item in certain position
        public void GetCurrentItem(EquipmentPosition location) { }


        public void Update(Item newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Text = newData.Text;
            Description = newData.Description;
            defense = newData.defense;
            speed = newData.speed;
            attack = newData.attack;
            range = newData.range;
            position = newData.position;
          
        }


    }
}
