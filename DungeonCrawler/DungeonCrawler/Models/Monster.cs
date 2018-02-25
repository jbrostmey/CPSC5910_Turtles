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
        }

        [PrimaryKey]
        public string Id { get; set; }

        // Droprate is used to determine if the monster drops its item upon death.
        //      This is done by comparing if a d10 dice roll is higher than the drop rate.
        private int dropRate = 5;

        private string drop { get; set; }
      
        public string ImageURI { get; set; }

        // Upon death, the monster may drop an item (based on d10 roll and drop rate)
        public override List<Item> Die(){
            
            return null;
        }

        // Call this when you want to deal damage to the monster.
        //      First calculates how much experience to return, then reduces health of the monster.
        public int TakeDamage(int Damage) { return 5; }


        public void Update(Monster m)
        {
            name = m.name;
            description = m.description;
            attributes.Update(m.attributes);
        }


    }
}