using System;
namespace DungeonCrawler.Models
{
    public class Score
    {
        //Basic constructor for Score class
        public Score()
        {
            currentScore = 0;
        }

        // Set/get current score
        public int currentScore { get; set; }
    }
}
