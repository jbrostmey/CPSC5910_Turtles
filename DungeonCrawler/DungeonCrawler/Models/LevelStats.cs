using System;
namespace DungeonCrawler.Models
{
    public class LevelStats
    {
        public Attributes[] levels { get; }
        private const int MAXLEVEL = 20;
        public LevelStats()
        {
            levels = new Attributes[MAXLEVEL];
            levels[0] = null;

            /* Setting up the modifiers by level. Each will be held as an attribute
             *   and upon level up, the level up function in Models.Character.cs will 
             *   use these attributes to update the character's attributes.
             *
             * The index is based on the character's current level, with the modifiers
             *   to apply when moving to the next level.
             */

            levels[1] = new Attributes().SetLevelUpModifier(300, 0, 1, 0);

            levels[2] = new Attributes().SetLevelUpModifier(900, 1, 1, 0);

            levels[3] = new Attributes().SetLevelUpModifier(2700, 0, 0, 0);

            levels[4] = new Attributes().SetLevelUpModifier(6500, 0, 1, 1);

            levels[5] = new Attributes().SetLevelUpModifier(14000, 1, 0, 0);

            levels[6] = new Attributes().SetLevelUpModifier(23000, 0, 1, 0);

            levels[7] = new Attributes().SetLevelUpModifier(34000, 0, 0, 0);

            levels[8] = new Attributes().SetLevelUpModifier(48000, 0, 0, 0);

            levels[9] = new Attributes().SetLevelUpModifier(64000, 1, 1, 1);

            levels[10] = new Attributes().SetLevelUpModifier(85000, 0, 0, 0);

            levels[11] = new Attributes().SetLevelUpModifier(100000, 0, 0, 0);

            levels[12] = new Attributes().SetLevelUpModifier(120000, 0, 1, 0);

            levels[13] = new Attributes().SetLevelUpModifier(140000, 1, 0, 0);

            levels[14] = new Attributes().SetLevelUpModifier(165000, 0, 0, 1);

            levels[15] = new Attributes().SetLevelUpModifier(195000, 0, 1, 0);

            levels[16] = new Attributes().SetLevelUpModifier(225000, 0, 0, 0);

            levels[17] = new Attributes().SetLevelUpModifier(265000, 1, 0, 0);

            levels[18] = new Attributes().SetLevelUpModifier(305000, 1, 1, 0);

            levels[19] = new Attributes().SetLevelUpModifier(355000, 1, 1, 1);

        }

        public int MaxLevel()
        {
            return MAXLEVEL;
        }
    }
}