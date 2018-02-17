using System;
namespace DungeonCrawler.Models
{
    public class Score
    {
        // [PrimaryKey]
        public string Id { get; set; }
        public int ScoreTotal { get; set; }

        //Basic constructor for Score class
        public Score()
        {
            ScoreTotal = 0;
        }


        public void Update(Score newData)
        {
            if (newData == null)
            {
                return;
            }
            // Update all the fields in the Data, except for the Id
            ScoreTotal = ScoreTotal;
        }
    }
}
