using System;
using Xamarin.Forms;
using DungeonCrawler.Models;
namespace DungeonCrawler
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        // To determine where the item goes
        public EquipmentPosition ItemLocation(Item item) 
        {
            EquipmentPosition equipPos = EquipmentPosition.body;
            return equipPos; 
        }

        // TODO: Might not want to return strings for these.

        // To determine what the item modifies
        public string ItemModifies(Item item) {
            string modif = "health";
            return modif;
        }

        // To determine what the item does
        public string ItemAction(Item item) {
            string action = "kill";
            return action;
        }
    }
}
