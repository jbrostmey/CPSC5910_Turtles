using System;
using DungeonCrawler.Models;

namespace DungeonCrawler
{
    public class MonsterDetailViewModel : BaseViewModel
    {
        public Monster Data { get; set; }
        public MonsterDetailViewModel(Monster data = null)
        {
            Title = data?.name;
            Data = data;
        }
    }
}
