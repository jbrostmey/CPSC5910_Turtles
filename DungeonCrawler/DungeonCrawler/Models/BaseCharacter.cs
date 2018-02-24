using System;
using System.Collections.Generic;

namespace DungeonCrawler.Models
{
    public class BaseCharacter : Actor
    {
        public BaseCharacter()
        {
            inventory = new Dictionary<EquipmentPosition, Item>();
        }

        //This will hold the items the character has equiped. It is publically gettable
        //  to allow views to grab the entire inventory of the characters to display on different screens.
        public Dictionary<EquipmentPosition, Item> inventory { get; }

        protected int ItemAttackModifier()
        {
            int attackModifier = 0;
            foreach (Item item in inventory.Values)
                attackModifier += item.attack;

            return attackModifier;
        }

        protected int ItemDamageModifier()
        {
            int damageModifier = 0;
            foreach (Item item in inventory.Values)
                damageModifier += item.damage;
            return damageModifier;
        }
    }
}
