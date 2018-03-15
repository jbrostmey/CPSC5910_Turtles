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

        public string Guid { get; set; }

        // Attributes of item
        public int defense { get; set; }
        public int speed { get; set; }
        public int attack { get; set; }
        public int damage { get; set; }

        //Base attributes
        public int range { get; set; }
        public EquipmentPosition position { get; set; }

        public string ImageURI { get; set; }


        public Item()
        {
            CreateDefaultItem();
        }

        // Create a default item for the instantiation
        private void CreateDefaultItem()
        {
            Id = "Unknown";
            Text = "Unknown";
            Description = "Unknown";

            ImageURI = "Item.png";

            defense = 0;
            speed = 0;
            attack = 0;
            damage = 0;
            range = 0;

            position = EquipmentPosition.body;

        }

        // To determine where the item goes, just an example of a equip on the body
        public EquipmentPosition ItemLocation(Item item)
        {
            EquipmentPosition equipPos = EquipmentPosition.body;
            return equipPos;
        }

        //// To determine what the item modifies on the character.
        //// This function calls the updateAttr function in character to update character attributes
        //// Takes in the item and current character object
        //public ItemEquipped(Item someItem, Character currentChar)
        //{
        //    bool flag = true;
        //    currentChar.updateAttributes(someItem, flag);
        //}

        ////If flag is false, then we remove the item from the character and update attributes
        //public void ItemRemoved(Item someItem, Character currentChar)
        //{
        //    bool flag = false;
        //    currentChar.updateAttributes(someItem, flag);
        //}

        //Get attributes of current item in certain position
        public void GetCurrentItem(EquipmentPosition location) { }

 




        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = Text;// + " , " +
                                //     Description + " for " +
                                //     Location.ToString() + " with " +
                                //    Attribute.ToString() +
                                //   "+" + Value + " , " +
                                //   "Range:" + Range;


            return myReturn.Trim();
        }

        // Constructor for Item called if needed to create a new item with set values.
        public Item(string text, string description, string guid, string imageuri, int _speed, int _attack, int _range, int _defense, EquipmentPosition _position)
        {

            Text = text;
            Guid = guid;
            ImageURI = imageuri;
            Description = description;
            defense = _defense;
            speed = _speed;
            attack = _attack;
            range = _range;
            position = _position;

        }












        public void Update(Item newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Text = newData.Text;
            Guid = newData.Guid;

            Description = newData.Description;
            defense = newData.defense;
            speed = newData.speed;
            attack = newData.attack;
            range = newData.range;
            position = newData.position;

        }


    }
}