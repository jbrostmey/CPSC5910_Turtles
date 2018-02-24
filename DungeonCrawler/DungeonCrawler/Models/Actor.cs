using System;
using System.Collections.Generic;
using SQLite;

namespace DungeonCrawler.Models
{
    /*
     * Base class of Character and Monster class
     */

    public class Actor
    {
        public Actor()
        {
            attributes = new Attributes();
            imageSource = "DapperDino.png";
        }

        public EquipmentPosition equipmentPositions { get; set; }

        //Name for actor. Actors can be characters or monsters.
        public string name { get; set; }

        public string imageSource { get; set; }

        //The description for the actor. 
        public string description { get; set; }

        // d10 used for calculations
        protected Random d10;

        //Allows getting of Attributes. No setting: must be done through methods.
        public Attributes attributes { get; }

        //Decreases attributes.currentHealth based on damage parameter.
        public void TakeDamage(int damage) { }

        //Calculates damage, taking into account Level damage, and item attack values
        public virtual int Attack() { return 5; }

        //Calculates defense, taking into account defense stats, defense modifiers, and item defense values
        public virtual int Defense() { return 5; }

        //Calculates accuracy, taking into account attack stats, attack modifiers, and item attack values.
        public virtual int Accuracy() { return 5; }

        //Calculates speed, taking into account speed stats, speed modifiers and item speed values.
        public virtual int Speed() { return 5; }

        //Returns true if the character is still alive.
        public bool IsAlive()
        {
            return attributes.alive;
        }

        //If a character has died, it will drop all of its equipment and return it to
        // the field as an array of items.
        public virtual List<Item> Die()
        {
            return null;
        }
    }
}