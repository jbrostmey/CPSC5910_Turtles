using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DungeonCrawler.Views.Battle
{
    public partial class GameEnd : ContentPage
    {
        public GameEnd()
        {
            InitializeComponent();
        }

        private async void PlayAgain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
