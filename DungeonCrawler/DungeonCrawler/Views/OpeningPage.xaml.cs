using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.Views.Scores;

using DungeonCrawler.Views.Party;

using Xamarin.Forms;

namespace DungeonCrawler.Views
{
    public partial class OpeningPage : ContentPage
    {
        public OpeningPage()
        {
            InitializeComponent();
            Title = "Main Menu";

        }

        //Need to add battle page when it is ready
        async void PlayButtonClick(Object sender, EventArgs e)
        {
         await Navigation.PushAsync(new PartySelect());
        }

        async void ScoreButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoresPage());
        }

        async void AboutPageButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
        async void AutoPlayButtonClick(Object sender, EventArgs e)
        {
            BattlePageViewModel.Instance.AutoPlayPartyInitialize();
            Battle battleObj = new Battle();
            //game messages and summary
            battleObj.BeginGame();
            string output = battleObj.AutoPlay();
            await Navigation.PushAsync(new BattleMessage(output));
        }
    }
}
