using System;
using System.Collections.Generic;

using Xamarin.Forms;

using DungeonCrawler.Views.Party;

namespace DungeonCrawler.Views
{
    public partial class GameEnd : ContentPage
    {
        public GameEnd()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void PlayAgain_Clicked(object sender, EventArgs e)
        {
            //MUST REINITIALIZE SQL DATASTORE...
            await Navigation.PopToRootAsync();
        }
    }
}
