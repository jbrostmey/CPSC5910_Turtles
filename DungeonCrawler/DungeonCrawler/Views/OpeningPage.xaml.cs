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

        //Goes to a screen for player to select party members
        async void PlayButtonClick(Object sender, EventArgs e)
        {
         await Navigation.PushAsync(new PartySelect());
        }

        //goes to a the scores page
        async void ScoreButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoresPage());
        }

        //goes to the about page (CRUDi and POST there)
        async void AboutPageButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        //Runs autoplay
        async void AutoPlayButtonClick(Object sender, EventArgs e)
        {
            //Randomly creates party
            BattlePageViewModel.Instance.AutoPlayPartyInitialize();
            //creates battle
            Battle battleObj = new Battle();
            //game messages and summary
            battleObj.BeginGame();
            //passes battle to the battle message page to show all that happened
            await Navigation.PushAsync(new BattleMessage(battleObj));
        }
    }
}
