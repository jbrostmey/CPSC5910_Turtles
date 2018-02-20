using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using DungeonCrawler.Views;


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
        async void BattlePageButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BattlePage());
        }

        async void CRUDiPageButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        async void AboutPageButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
    }
}
