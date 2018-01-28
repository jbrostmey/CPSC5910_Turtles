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

        //Decreases attributes.currentHealth based on damage parameter.
        public void TakeDamage(int damage) { }

        //Calculates damage, taking into account attack stats, attack modifiers, and item attack values
        public int Attack() { }

        //Calculates defense, taking into account defense stats, defense modifiers, and item defense values
        public int Defense() { }

        //Calculates accuracy, taking into account speed stats, speed modifiers, and item speed values.
        public int Accuracy() { }
    }
}

