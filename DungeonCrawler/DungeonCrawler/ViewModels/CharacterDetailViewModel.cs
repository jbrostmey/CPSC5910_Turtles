using System;
using DungeonCrawler.Models;

namespace DungeonCrawler
{
    public class CharacterDetailViewModel : BaseViewModel
    {
        //data to bind to
        public Character Data { get; set; }
        //constructor
        public CharacterDetailViewModel(Character data = null)
        {
            //title for the views binding to use
            Title = data?.name;
            Data = data;
        }
    }
}
