
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

        //This displays the ending score at the end of the game.
        public GameEnd(Score obj)
        {
            BindingContext = this;
            InitializeComponent();
            int score = obj.ScoreTotal;
            Display.Text = score.ToString();
        }
        //Pops back to opening page
        private async void PlayAgain_Clicked(object sender, EventArgs e)
        {
            //SQLDataStore.Instance.InitializeDatabaseNewTables();
            await Navigation.PopToRootAsync();
        }
    }
}
