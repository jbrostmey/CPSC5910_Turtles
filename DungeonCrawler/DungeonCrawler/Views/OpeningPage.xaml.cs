using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using DungeonCrawler.Views;
using DungeonCrawler.Services;
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
            //  BaseViewModel.DataStoreEnum.Sql; //.Instance.s;
          //  BaseViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
        }

        //Need to add battle page when it is ready
        async void PlayButtonClick(Object sender, EventArgs e)
        {


         await Navigation.PushAsync(new PartySelect());
            //await Navigation.PushAsync(new BattlePage());
        }

        async void CRUDiPageButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        async void AboutPageButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
        async void AutoPlayButtonClick(Object sender, EventArgs e)
        {
            Battle battleObj = new Battle();
            //game messages and summary
            battleObj.BeginGame();
            string output = battleObj.AutoPlay();
            await Navigation.PushAsync(new BattleMessage(output));
        }
    }
}
