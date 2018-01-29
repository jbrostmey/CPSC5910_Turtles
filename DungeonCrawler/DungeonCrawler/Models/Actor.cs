using System;
namespace DungeonCrawler.Models
{
    /*
     * Base class of Character and Monster class
     */

    public class Actor
    {
        public Actor()
        {
        }

        //Name for actor. Actors can be characters or monsters.
        public String name { get; set; }

        //The description for the actor. 
        public String description { get; set; }

        // d10 used for calculations
        protected Random d10;

        //Allows getting of Attributes. No setting: must be done through methods.
        public Attributes attributes { get; }

        //Decreases attributes.currentHealth based on damage parameter.
        public void TakeDamage(int damage) { }

        //Calculates damage, taking into account attack stats, attack modifiers, and item attack values
        public int Attack() { }

        //Calculates defense, taking into account defense stats, defense modifiers, and item defense values
        public int Defense() { }

        //Calculates accuracy, taking into account speed stats, speed modifiers, and item speed values.
        public int Accuracy() { }

        //Returns true if the character is still alive.
        public bool IsAlive()
        {
            return attributes.alive;
        }

        //If a character has died, it will drop all of its equipment and return it to
        // the field as an array of items.
        public Item[] Die()
        {

        }
    }
}