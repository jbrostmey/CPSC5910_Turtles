using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;

using Xamarin.Forms;

namespace DungeonCrawler.Views.Battle
{
    public partial class BattlePage : ContentPage
    {
        public BattlePage()
        {
            InitializeComponent();
        }

        //Nonfunctional at this time
        private async void ViewCharacters_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemsPage());
        }

        private async void ViewMonsters_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemsPage());
        }



        //Return to Opening Page
        private async void ExitGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameEnd());
        }

        //Open up Game Over Page
        private async void Play_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BattleMessage());
        }
    }
}
