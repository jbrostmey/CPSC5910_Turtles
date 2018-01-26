using System;
namespace DungeonCrawler.Models
{
    public class LevelStats
    {
        private Attributes[] levels;
        private const int MAXLEVEL = 20;
        public LevelStats()
        {
            levels = new Attributes[MAXLEVEL+1];
            levels[0] = null;

            //attributes as follows(   hp,xp,spd,attk,def,level,alive,modifiers)
            levels[1] = new Attributes(15, 0, 1,    1,  1,1, true, 0, 0, 0);

            levels[2] = new Attributes(15, 0, 1,    1,  2, 1, true, 0, 0, 0);

            levels[3] = new Attributes(15, 0, 1,    2,  3, 1, true, 0, 0, 0);

            levels[4] = new Attributes(15, 0, 1,    2,  3, 1, true, 0, 0, 0);

            levels[5] = new Attributes(15, 0, 2,    4,  2, 1, true, 0, 0, 0);

            levels[6] = new Attributes(15, 0, 2,    3,  4, 1, true, 0, 0, 0);

            levels[7] = new Attributes(15, 0, 2,    3,  5, 1, true, 0, 0, 0);

            levels[8] = new Attributes(15, 0, 2,    3,  5, 1, true, 0, 0, 0);

            levels[9] = new Attributes(15, 0, 2,    3,  5, 1, true, 0, 0, 0);

            levels[10] = new Attributes(15, 0, 3,   4,  6, 1, true, 0, 0, 0);

            levels[11] = new Attributes(15, 0, 3,   4,  6, 1, true, 0, 0, 0);

            levels[12] = new Attributes(15, 0, 3,   4,  6, 1, true, 0, 0, 0);

            levels[13] = new Attributes(15, 0, 3,   4,  7, 1, true, 0, 0, 0);

            levels[14] = new Attributes(15, 0, 4,   5,  7, 1, true, 0, 0, 0);

            levels[15] = new Attributes(15, 0, 4,   5,  7, 1, true, 0, 0, 0);

            levels[16] = new Attributes(15, 0, 4,   5,  8, 1, true, 0, 0, 0);

            levels[17] = new Attributes(15, 0, 4,   5,  8, 1, true, 0, 0, 0);

            levels[18] = new Attributes(15, 0, 4,   6,  8, 1, true, 0, 0, 0);

            levels[19] = new Attributes(15, 0, 4,   7,  9, 1, true, 0, 0, 0);

            levels[20] = new Attributes(15, 0, 5,   8,  10, 1, true, 0, 0, 0);
        }
    }
}
