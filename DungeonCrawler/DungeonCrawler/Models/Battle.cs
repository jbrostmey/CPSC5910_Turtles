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

    public class Battle : Actor
    {
        public bool inSession = false;
        public bool currentTurn = false; // 0 is character, 1 is for monster
        public Character currentChar { get; set; }
        public Monster currentMon { get; set; }

        public Battle()
        {
        }

        // User begins game, switch inSession to true
        public void BeginGame() { inSession = true; }

        //Will take in a character object and retrieve stats of that character to determine what the attack will be. OR if it is a monster, will retrieve attack of monster
        // Check to see what turn, then grab the monster or character stats and perform calculation
        public int AttackCalculation() { return 5; }

        //Will work with Damage() from Actor class and determine if character or monster dies
        //Check to see what turn, then grab monster or character stats (of opponent) to determine if the damage inflicted kills them 
        public bool DamageDie() { return false; }

        // User ends game, switch inSession to false
        public void EndGame() { inSession = false; }

        //User selects AutoPlay, switch inSession to true, as well as other implementation code to automate gameplay (will call other battle methods)
        public void AutoPlay() { inSession = true; }


    }
}

