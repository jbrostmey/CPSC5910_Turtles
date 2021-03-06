﻿using System;
using SQLite;
namespace DungeonCrawler.Models
{
    public class Score
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        //Total score of entire game
        public int ScoreTotal { get; set; }

        // The Date the game played, and when the score was saved
        public DateTime GameDate { get; set; }

        // Tracks if auto battle is true, or if user battle = false
        public bool AutoBattle { get; set; }

        // The number of turns the battle took to finish
        // Missign property, to add as part of the SQL lecture...
        public int TurnNumber { get; set; }

        //Number of rounds/battles in game
        public int BattleNumber { get; set; }

        //Number of total turns in game
        public int NumTurns { get; set; }

        // The count of monsters slain during battle
        public int MonsterSlainNumber { get; set; }

        // The total experience points all the characters received during the battle
        public long ExperienceGainedTotal { get; set; }

        // A list of all the characters at the time of death and their stats.  Needs to be in json format, so saving a string
        public string CharacterAtDeathList { get; set; }

        // All of the monsters killed and their stats. Needs to be in json format, so saving as a string
        public string MonstersKilledList { get; set; }

        // All of the items dropped and their stats. Needs to be in json format, so saving as a string
        public string ItemsDroppedList { get; set; }
       
        public string ImageURI { get; set; }

        //Basic constructor for Score class, sets default values
        public Score()
        {
           // ScoreTotal = 0;
            GameDate = DateTime.Now;    // Set to be now by default.
            AutoBattle = false;         //assume user battle
            CharacterAtDeathList = "";
            MonstersKilledList = "";
            ItemsDroppedList = "";
            BattleNumber = 0;
            NumTurns = 0;
            Id = Guid.NewGuid().ToString();
        }

        //This function will update all the appropriate fields associated with Score object
        public void Update(Score newData)
        {
            if (newData == null)
            {
                return;
            }
            // Update all the fields in the Data, except for the Id
            Name = newData.Name;
            ScoreTotal = newData.ScoreTotal;
            GameDate = newData.GameDate;
            AutoBattle = newData.AutoBattle;
            TurnNumber = newData.TurnNumber;
            BattleNumber = newData.BattleNumber;
            MonsterSlainNumber = newData.MonsterSlainNumber;
            ExperienceGainedTotal = newData.ExperienceGainedTotal;
            CharacterAtDeathList = newData.CharacterAtDeathList;
            MonstersKilledList = newData.MonstersKilledList;
            ItemsDroppedList = newData.ItemsDroppedList;
            Id = newData.Id;
        }
    }
}
