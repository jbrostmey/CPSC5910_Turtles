
using System;

using Xamarin.Forms;
using DungeonCrawler.Services;

using DungeonCrawler.ViewModels;
using DungeonCrawler.Models;

using DungeonCrawler.Controllers;

namespace DungeonCrawler.Views
{
    public partial class GameEnd : ContentPage
    {
        public GameEnd()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }
        public GameEnd(Score obj)
        {
            BindingContext = this;
            InitializeComponent();
            int score = obj.ScoreTotal;
            Display.Text = score.ToString();
        }
        private async void PlayAgain_Clicked(object sender, EventArgs e)
        {
            SQLDataStore.Instance.InitializeDatabaseNewTables();
            await Navigation.PopToRootAsync();
        }
    }
}
