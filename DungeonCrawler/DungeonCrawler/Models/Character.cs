using System;
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
    public class Character
    {
        //Basic constructor.
        public Character(Attributes atb, String name) { }

        //Allows getting/setting of name after creation.
        public String name { get; set; }

        //Allows getting of Attributes. No setting: must be done through methods.
        public Attributes atb { get; }

        //Allows for getting/setting of the character class.
        public String characterClass { get; set; }

        //Drops item being held in the specific item type slot
        public Item DropEquip(Enum itemType) { }

        //Equips new item if it can. If it cannot equip the item due to the item slot
        //  being filled, will return false.
        public bool EquipItem(Item item) { }

        //Decreases atb.currentHealth based on damage parameter.
        public void TakeDamage(int damage) { }

        //Level up, called from GainXP when a level up is needed.
        private void LevelUp() { }

        //Increases xp based on xp passed as parameter. Can call LevelUp when necessary.
        public void GainXP(int xp) { }

        //Returns if the opposite of atb.alive.
        public bool IsDead()
        {
            return !atb.alive;
        }
    }
}




