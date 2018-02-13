using System;
using Xamarin.Forms;
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
        public string Id { get; set; } // name of item
        public string Text { get; set; }
        public string Description { get; set; }

        // Attributes of item
        public int defense;
        public int speed;
        public int attack;


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
            currentChar.updateAttr(someItem, flag);
        }

        //If flag is false, then we remove the item from the character and update attributes
        public void ItemRemoved(Item someItem, Character currentChar)
        {
            bool flag = false;
            currentChar.updateAttr(someItem, flag);
        }

        //Get attributes of current item in certain position
        public void GetCurrentItem(EquipmentPosition location) { }
    }
}
