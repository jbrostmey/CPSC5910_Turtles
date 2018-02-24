using System;
using System.Collections.Generic;
namespace DungeonCrawler.Models
{
    public class BaseMonster : Actor
    {
        public BaseMonster()
        {
            DropPool = new List<Item>();
        }

        // Upon creation, a monster is assigned items to be in its drop pool.
        //  These items can be dropped upon death.
        public List<Item> DropPool { get; set; }
    }
}
