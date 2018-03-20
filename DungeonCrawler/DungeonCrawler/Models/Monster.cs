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
    * Compiling Implemented code for monster: 
        Properties implementation 
        Method stubs implementation */
    public class Monster : BaseMonster
    {
        //Basic constructor.
        // Each monster will initialize their own d10 on creation.
        public Monster()
        {
            d10 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            attributes = new Attributes();
            //attributes.health = 2;
            //attributes.currentHealth = 2;
            //attributes.level = 20;
            //attributes.currentExperience = 3000;
        }

        public Monster(BaseMonster monster)
        {
            d10 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            attributes = new Attributes();
            Id = monster.Id;
            ImageURI = monster.ImageURI;
            AttributeString = monster.AttributeString;
            attributes.PopulateFromString(AttributeString);
            name = monster.name;
            number = monster.number;
            description = monster.description;
        }

        public void SaveAttributes()
        {
            AttributeString = attributes.AttributeString();
        }

        // Droprate is used to determine if the monster drops its item upon death.
        //      This is done by comparing if a d10 dice roll is higher than the drop rate.
        //private int dropRate = 5;

        private string drop { get; set; }

        //Allows getting of Attributes. No setting: must be done through methods.
        public Attributes attributes { get; set; }


        //Returns true if the character is still alive.
        public bool IsAlive()
        {
            return attributes.alive;
        }

        // Upon death, the monster may drop an item (based on d10 roll and drop rate)
        public override List<Item> Die(Monster monster){

            Item newItem = ItemsViewModel.Instance.RandomItem(); 

            List<Item> droppedItems = new List<Item>();
            droppedItems.Add(newItem);
            return droppedItems;
        }

        // Call this when you want to deal damage to the monster.
        //      First calculates how much experience to return, then reduces health of the monster.
        public int TakeDamage(int Damage) {

            // if the monster is going to die, returns all experience and sets health to 0
            if(Damage >= attributes.currentHealth)
            {
                attributes.currentHealth = 0;
                attributes.alive = false;
                return attributes.currentExperience;
            }
                
            // % of damage dealt/health lost = % of experience given
            double experiencePercentage = (double)Damage / attributes.currentHealth;

            // damage reduces health by the damage amount as whole number
            attributes.currentHealth = attributes.currentHealth - Damage;

            // calculates experience and updates remaining experience
            int experience = (int)Math.Ceiling(attributes.currentExperience * experiencePercentage);
            attributes.currentExperience -= experience;

            return experience; 
        }


        //Update monster attributes
        public void Update(Monster m)
        {
            name = m.name;
            description = m.description;
            attributes.Update(m.attributes);
        }

        //Called when Monster is dead
        public string DeadState()
        {
            var myReturn = string.Empty;
            myReturn += name;
            myReturn += " , " + description;
            myReturn += " , " + attributes.StringOutput();

            return myReturn;
        }
    }
}