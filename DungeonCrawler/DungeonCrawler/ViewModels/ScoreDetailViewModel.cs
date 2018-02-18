using System;
using DungeonCrawler.Models;
namespace DungeonCrawler.ViewModels
{
    public class ScoreDetailViewModel : BaseViewModel
    {
        public Score Data { get; set; }
        public ScoreDetailViewModel(Score data = null)
        {
            Title = data?.Id;
            Data = data;
        }
    }
}

