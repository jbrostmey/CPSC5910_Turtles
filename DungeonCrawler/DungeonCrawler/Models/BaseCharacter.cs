using System;
using System.Collections.Generic;

namespace DungeonCrawler.Models
{
    //Class build to deal with the inventory for the character
    public class BaseCharacter : Actor
    {
        public BaseCharacter()
        {
            inventory = new Dictionary<EquipmentPosition, Item>();
            MiracleMaxLive = true;
        }

        //This will hold the items the character has equiped. It is publically gettable
        //  to allow views to grab the entire inventory of the characters to display on different screens.
        public Dictionary<EquipmentPosition, Item> inventory { get; }

        public bool MiracleMaxLive { get; set; }

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
