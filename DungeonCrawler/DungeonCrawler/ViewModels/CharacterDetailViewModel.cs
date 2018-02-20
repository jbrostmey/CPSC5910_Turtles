using System;
using DungeonCrawler.Models;

namespace DungeonCrawler
{
    public class CharacterDetailViewModel : BaseViewModel
    {
        public Character Data { get; set; }
        public CharacterDetailViewModel(Character data = null)
        {
            Title = data?.name;
            Data = data;
        }
    }
}
