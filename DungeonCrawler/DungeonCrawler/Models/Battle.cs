﻿using System;
using System.Collections.Generic;
using System.Linq;

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

    //BATTLE LIVES INSIDE BATTLEPAGE.XAML
    public class Battle
    {
        const int SIZE = 6;


        public bool inSession;
        public bool currentTurn; // 0 is character, 1 is for monster
        public int currentChar;
        public int currentMon;
        public int round;

        public Character[] aChar;
        public Monster[] aMon;

        public Battle()
        {
            inSession = true;
            currentTurn = false;
        }

        // Turn implementation, keeps track of who's turn and the actions+ouputs associated with a turn
        //return string for BattleMessage.xaml
        public string Turn(Character aChar, Monster aMon)
        {
            string msg = "";

            if (inSession == true)
            {
                if (currentTurn == false) // Character's turn (by default, character goes first)
                {
                    int getCharAtt = CharacterAttack(aChar);
                    int getMonDef = MonsterDefense(aMon);

                    //Character attacks, monster loses health
                    int experience = aMon.TakeDamage(getCharAtt);


                    msg = "Character " + currentChar + " attacked Monster " + currentMon + " with a damage of " + getCharAtt;

                    aChar.GainExperience(experience); // EXP not yet determined
                    if (!aMon.IsAlive())
                    {
                        //Item[] drops = aMon.dropPool; // Items dropped from monster's death
                        currentMon++;
                    }
                    currentTurn = true;
                }
                else // Monster's turn
                {
                    int getMonAtt = MonsterAttack(aMon);
                    int getCharDef = CharacterDefense(aChar);

                    //Character attacks, monster loses health
                    aChar.TakeDamage(getMonAtt);

                    msg = "Monster " + currentMon + " attacked Character " + currentChar + " with a damage of " + getMonAtt;

                    if (!aChar.IsAlive())
                    {
                        aChar.Die(); // Relinquish inventory and drop all items
                        currentChar++;
                    }
                    currentTurn = false;
                }
            }

            /*If character party dies, END GAME (inSession = false). If monster party is dead, send message
             * to begin a new round to a round function. 
             */
            if (CheckParty("character"))
                inSession = false;
            else if (CheckParty("monster"))
            {
                round++;
                msg += "\n Next round! Round " + round;
            }

            return msg;
        }

        // User begins game, switch inSession to true
        //This function will be the Green light to begin Battle Engine. It will initialize characters and monsters, as well as handle turns
        //This function is to be called in BattlePage.xaml.cs
        public void BeginGame()
        {

            aChar = new Character[SIZE];
            aMon = new Monster[SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                aChar[i] = new Character();
                aMon[i] = new Monster();
            }

            //For now, selection of Character and Monster are automated (characer 1 fights monster 1 until one dies)
            //Will go in order from index 0 to index 6

            currentMon = 0;
            currentChar = 0;
            round = 1;
        }

        //Will take in a character object and retrieve stats of that character to determine what the attack will be. OR if it is a monster, will retrieve attack of monster
        // Check to see what turn, then grab the monster or character stats and perform calculation
        //public int AttackCalculation() { return 5; }

        //Will work with Damage() from Actor class and determine if character or monster dies
        //Check to see what turn, then grab monster or character stats (of opponent) to determine if the damage inflicted kills them 
        public bool DamageDie() { return false; }

        // User ends game, switch inSession to false
        //Connect function to UI Button "Quit Game"
        public void EndGame() { inSession = false; }

        //User selects AutoPlay, run game indefinitely until inSession is switched to false
        public void AutoPlay()
        {



        }

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
        public int CharacterAccuracy(Character character)
        {
            return character.attributes.attack + character.ItemAttackModifier() + character.attributes.level + (character.d10.Next() % 20 + 1);
        }

        //Calculates damage, taking into account attack stats, attack modifiers, and item attack values
        public int MonsterAttack(Monster monster)
        {
            return monster.attributes.attack + (int)Math.Ceiling(monster.attributes.level * .25);
        }
        //Calculates defense, taking into account defense stats, defense modifiers, and item defense values
        public int MonsterDefense(Monster monster)
        {
            return monster.attributes.defense + monster.attributes.level;
        }
        //Calculates accuracy, taking into account speed stats, speed modifiers, and item speed values.
        public int MonsterAccuracy(Monster monster)
        {
            return monster.attributes.attack + monster.attributes.level + (monster.d10.Next() % 20 + 1);
        }


        //Helper function used in Turn to help implement rounds. Return true if all have died in party. 
        private bool CheckParty(string type)
        {
            bool status = false;
            int count = 0;
            if (type == "monster")
            {
                for (int i = 0; i < SIZE; i++)
                {
                    if (!aMon[i].IsAlive())
                        count++;
                }
            }
            else
            {
                for (int i = 0; i < SIZE; i++)
                {
                    if (!aMon[i].IsAlive())
                        count++;
                }

            }

            if (count == SIZE)
                status = true;

            return status;
        }


        private void InitNewRound()
        {
            for (int i = 0; i < SIZE; i++)
            {

            }
        }

        //To be called in Turn function after every Turn. Sorts character and monster for next turn
        //Once a character or monster dies, switch their speed to -99 so they are sorted to the bottom)
        //Keep a member that holds the count of monster/chars alive

        private void EntityOrder(bool type)
        { // true for character, false for monster
            if (type)
            {
                List<Character> sortChar = new List<Character>();
                //obj is a temp var that holds the entire list and looks at individual objects
                sortChar = sortChar.OrderBy(obj => obj.attributes.speed)
                     .ThenBy(obj => obj.attributes.level)
                     .ThenBy(obj => obj.attributes.currentExperience)
                     .ThenBy(obj => obj.name)
                     .ToList();

                //overwrite base objects with sorted list
                for (int i = 0; i < SIZE; i++)
                    aChar[i] = sortChar[i];
            }
            else
            {
                //Convert aMon array to aMon list so we can use Linq to sort the list, then pushback/overwrite into original aMon array
                List<Monster> sortMon = new List<Monster>();
                //obj is a temp var that holds the entire list and looks at individual objects
                sortMon = sortMon.OrderBy(obj => obj.attributes.speed)
                     .ThenBy(obj => obj.attributes.level)
                     .ThenBy(obj => obj.attributes.currentExperience)
                     .ThenBy(obj => obj.name)
                     .ToList();

                //overwrite base objects with sorted list
                for (int i = 0; i < SIZE; i++)
                    aMon[i] = sortMon[i];
            }
        }
    }
}



// todo: replace view characters and view monsters with battle message
// refresh entire page, characters will have stats refreshed/ shown * (when action happens, refresh page)

// figure out score *

// looting items: put together page that handles the items (create items page, choose piece of equipment, new page choose character)
// could always randomize item pickup

// rounds: start round function that should probably take character levels into account
// less on priority: move battle.cs to viewmodel (Data and functions)
