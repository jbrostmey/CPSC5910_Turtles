using System;
namespace DungeonCrawler.Models
{
    public class Attributes
    {

        //Basic constructor for attributes class
        public Attributes(){
            health = 10;
            currentHealth = 10;
            currentExperience = 0;
            speed = 1;
            attack = 1;
            defense = 1;
            level = 1;
            alive = true;
            attackModifier = 1;
            defenseModifier = 1;
            speedModifier = 1;
        }

        public Attributes SetLevelUpModifier(int attack, int defense, int speed){
            this.health = 0;
            this.speed = speed;
            this.attack = attack;
            this.defense = defense;
            return this;
        }

        // set/get max health
        public int health { get; set; }

        // set/get current health
        public int currentHealth { get; set; }

        // set/get current experience
        public int currentExperience { get; set; }

        // set/get speed attribute
        public int speed { get; set; }

        // set/get attack attribute
        public int attack { get; set; }

        // set/get defense attribute
        public int defense { get; set; }

        // set/get level attribute
        public int level { get; set; }

        // set/get alive attribute
        public bool alive { get; set; }

        // set/get attack modifier (for class specific modifiers)
        public int attackModifier { get; set; }

        // set/get defense modifier (for class specific modifiers)
        public int defenseModifier { get; set; }

        // set/get speed modifier (for class specific modifiers)
        public int speedModifier { get; set; }

    }
}
