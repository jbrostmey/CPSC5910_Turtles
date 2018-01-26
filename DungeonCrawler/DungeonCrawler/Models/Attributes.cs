using System;
namespace DungeonCrawler.Models
{
    public class Attributes
    {

        //Basic constructor for attributes class
        public Attributes(int health = 10, int xp = 0, int speed = 1, int attack = 1,
                          int defense = 1, int level = 1, bool alive = true, int attackMod = 1, int defenseMod = 1, int speedMod = 1)
        {
            this.health = health;
            currentHealth = health;
            currentExperience = xp;
            this.speed = speed;
            this.attack = attack;
            this.defense = defense;
            this.level = level;
            this.alive = alive;
            this.attackMod = attackMod;
            this.defenseMod = defenseMod;
            this.speedMod = speedMod;
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
        public int attackMod { get; set; }

        // set/get defense modifier (for class specific modifiers)
        public int defenseMod { get; set; }

        // set/get speed modifier (for class specific modifiers)
        public int speedMod { get; set; }

    }
}
