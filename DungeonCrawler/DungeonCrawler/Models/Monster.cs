using System;
using System.Collections.Generic;

using SQLite;

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
    public class Monster : Actor
    {
        //Basic constructor.
        // Each monster will initialize their own d10 on creation.
        public Monster()
        {
            d10 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            //attributes.health = 2;
            //attributes.currentHealth = 2;
            //attributes.level = 20;
            //attributes.currentExperience = 3000;
        }

        [PrimaryKey]
        public string Id { get; set; }

        // Droprate is used to determine if the monster drops its item upon death.
        //      This is done by comparing if a d10 dice roll is higher than the drop rate.
        //private int dropRate = 5;

        private string drop { get; set; }
      
        public string ImageURI { get; set; }

        private int ENUMLOCATIONS = 7;


        // Upon death, the monster may drop an item (based on d10 roll and drop rate)
        public override List<Item> Die(Monster monster){

            List<Item> items = new List<Item>();
            for (int i = 0; i < ENUMLOCATIONS; i++)
            {
                items.Add(monster.actorItemsCorrespondingToLocation[i]);
            }
            return items;
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


        public void Update(Monster m)
        {
            name = m.name;
            description = m.description;
            attributes.Update(m.attributes);
        }
    }
}