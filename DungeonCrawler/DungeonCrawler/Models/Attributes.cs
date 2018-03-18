using System;

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DungeonCrawler.Models
{
    public class Attributes
    {

        //Basic constructor for attributes class
        public Attributes()
        {
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


        public enum AttributeEnum
        {
            // Not specified
            Unknown = 0,

            // The speed of the character, impacts movement, and initiative
            Speed = 10,

            // The defense score, to be used for defending against attacks
            Defense = 12,

            // The Attack score to be used when attacking
            Attack = 14,

            // Current Health which is always at or below MaxHealth
            CurrentHealth = 16,

            // The highest value health can go
            MaxHealth = 18,
        }





        public Attributes SetLevelUpModifier(int experience, int attack, int defense, int speed)
        {
            this.health = 0;
            this.currentExperience = experience;
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

        public void Update(Attributes c)
        {
            alive = c.alive;
            attack = c.attack;
            attackModifier = c.attackModifier;
            currentExperience = c.currentExperience;
            currentHealth = c.currentHealth;
            defense = c.defense;
            defenseModifier = c.defenseModifier;
            health = c.health;
            level = c.level;
            speed = c.speed;
            speedModifier = c.speedModifier;
        }

        public string AttributeString()
        {
            string attributeString = ((JObject)JToken.FromObject(this)).ToString();
            return attributeString;
        }

        public void PopulateFromString(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            var c = JsonConvert.DeserializeObject<Attributes>(data);

            alive = c.alive;
            attack = c.attack;
            attackModifier = c.attackModifier;
            currentExperience = c.currentExperience;
            currentHealth = c.currentHealth;
            defense = c.defense;
            defenseModifier = c.defenseModifier;
            health = c.health;
            level = c.level;
            speed = c.speed;
            speedModifier = c.speedModifier;
        }

        public string StringOutput()
        {
            var myReturn = string.Empty;
            myReturn += "Attack : " + attack;
            myReturn += " , Defense : " + defense;
            myReturn += " , Speed : " + speed;
            myReturn += " , Max Health : " + health;
            myReturn += " , Level : " + level;
            myReturn += " , Experience : " + currentExperience;

            return myReturn;
        }

        // Helper functions for the AttribureList
        public static class AttributeList
        {

            // Returns a list of strings of the enum for Attribute
            // Removes the attributes that are not changable by Items such as Unknown, MaxHealth
            public static List<string> GetListItem
            {
                get
                {
                    var myList = Enum.GetNames(typeof(AttributeEnum)).ToList();
                    var myReturn = myList.Where(a =>
                                                    a.ToString() != AttributeEnum.Unknown.ToString() &&
                                                    a.ToString() != AttributeEnum.MaxHealth.ToString()
                                                ).ToList();
                    return myReturn;
                }
            }

            // Returns a list of strings of the enum for Attribute
            // Removes the unknown
            public static List<string> GetListCharacter
            {
                get
                {
                    var myList = Enum.GetNames(typeof(AttributeEnum)).ToList();
                    var myReturn = myList.Where(a =>
                                                    a.ToString() != AttributeEnum.Unknown.ToString()
                                                ).ToList();
                    return myReturn;
                }
            }

            // Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
            public static AttributeEnum ConvertStringToEnum(string value)
            {
                return (AttributeEnum)Enum.Parse(typeof(AttributeEnum), value);
            }
        }
    }
}