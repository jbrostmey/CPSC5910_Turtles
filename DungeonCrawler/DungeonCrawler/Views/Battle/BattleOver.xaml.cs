using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DungeonCrawler.Views.Battle
{
    public partial class BattleOver : ContentPage
    {
        public BattleOver()
        {
            InitializeComponent();
        }


        //Functions below are not functional at this time.

        private async void NextLevel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void OnPickUpItemsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void OnViewStatistics_ButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
