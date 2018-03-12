using System;
using System.Collections.Generic;

namespace DungeonCrawler.Models
{
    public class BaseCharacter : Actor
    {
        public BaseCharacter()
        {
        }

        public Dictionary<EquipmentPosition, string> inventory { get; }
    }
}
