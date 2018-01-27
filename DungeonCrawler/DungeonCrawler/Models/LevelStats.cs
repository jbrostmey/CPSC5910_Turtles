﻿using System;
namespace DungeonCrawler.Models
{
    public class LevelStats
    {
        private Attributes[] levels;
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

            levels[1] = new Attributes().setLevelUpModifier(0,1,0);

            levels[2] = new Attributes().setLevelUpModifier(1,1,0);

            levels[3] = new Attributes().setLevelUpModifier(0,0,0);

            levels[4] = new Attributes().setLevelUpModifier(0,1,1);

            levels[5] = new Attributes().setLevelUpModifier(1,0,0);

            levels[6] = new Attributes().setLevelUpModifier(0,1,0);

            levels[7] = new Attributes().setLevelUpModifier(0,0,0);

            levels[8] = new Attributes().setLevelUpModifier(0,0,0);

            levels[9] = new Attributes().setLevelUpModifier(1,1,1);

            levels[10] = new Attributes().setLevelUpModifier(0,0,0);

            levels[11] = new Attributes().setLevelUpModifier(0,0,0);

            levels[12] = new Attributes().setLevelUpModifier(0,1,0);

            levels[13] = new Attributes().setLevelUpModifier(1,0,0);

            levels[14] = new Attributes().setLevelUpModifier(0,0,1);

            levels[15] = new Attributes().setLevelUpModifier(0,1,0);

            levels[16] = new Attributes().setLevelUpModifier(0,0,0);

            levels[17] = new Attributes().setLevelUpModifier(1,0,0);

            levels[18] = new Attributes().setLevelUpModifier(1,1,0);

            levels[19] = new Attributes().setLevelUpModifier(1,1,1);

        }
    }
}