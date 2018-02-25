using System;

using Xamarin.Forms;

namespace DungeonCrawler.Models
{
    /* Team: Turtles 
    *  Julia Brostmeyer 
    *  Bryan Herr 
    *  Denny Tran 
    *  
    * Compiling Implemented code for Battle: 
      Properties implementation 
      Method stubs implementation */

    public class Battle 
    {
        public bool inSession;
        public bool currentTurn; // 0 is character, 1 is for monster
        public Character currentChar { get; set; }
        public Monster currentMon { get; set; }

        public Battle()
        {
            inSession = false;
            currentTurn = false;
        }

        // Turn implementation, keeps track of who's turn and the actions+ouputs associated with a turn
        public void Turn(Monster aMon, Character aChar)
        {
            if (inSession == true)
            {
                if (currentTurn == false) // Character's turn
                {
                    int getCharAtt = aChar.Attack();
                    int getMonDef = aMon.Defense();

                    //Character attacks, monster loses health
                    aMon.TakeDamage(getCharAtt);
                    if (!aMon.IsAlive())
                    {
                        aChar.GainExperience(1); // EXP not yet determined
                        //Item[] drops = aMon.dropPool; // Items dropped from monster's death
                    }
                }
                else // Monster's turn
                {
                    int getMonAtt = aMon.Attack();
                    int getCharDef = aChar.Defense();

                    //Character attacks, monster loses health
                    aChar.TakeDamage(getMonAtt);

                    if (!aChar.IsAlive())
                        aChar.Die(); // Relinquish inventory and drop all items
                }
            }
        }

        // User begins game, switch inSession to true
        public void BeginGame() { inSession = true; }

        //Will take in a character object and retrieve stats of that character to determine what the attack will be. OR if it is a monster, will retrieve attack of monster
        // Check to see what turn, then grab the monster or character stats and perform calculation
        //public int AttackCalculation() { return 5; }

        //Will work with Damage() from Actor class and determine if character or monster dies
        //Check to see what turn, then grab monster or character stats (of opponent) to determine if the damage inflicted kills them 
        public bool DamageDie() { return false; }

        // User ends game, switch inSession to false
        public void EndGame() { inSession = false; }

        //User selects AutoPlay, switch inSession to true, as well as other implementation code to automate gameplay (will call other battle methods)
        public void AutoPlay() { inSession = true; }

        //Calculates damage, taking into account attack stats, attack modifiers, and item attack values
        public int CharacterAttack(Character character)
        {
            return character.ItemDamageModifier() + (int)Math.Ceiling(character.attributes.level * .25);
        }
        //Calculates defense, taking into account defense stats, defense modifiers, and item defense values
        public int CharacterDefense(Character character)
        {
            return character.attributes.defense + character.ItemDefenseModifer() + character.attributes.level;
        }
        //Calculates accuracy, taking into account speed stats, speed modifiers, and item speed values.
        public int Accuracy(Character character)
        {
            return character.attributes.attack + character.ItemAttackModifier() + character.attributes.level + (character.d10.Next() % 20 + 1);
        }

    }
}

