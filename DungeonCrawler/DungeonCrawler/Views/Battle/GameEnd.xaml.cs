using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DungeonCrawler.Views
{
    public partial class GameEnd : ContentPage
    {
        public GameEnd(string msg)
        {
            InitializeComponent();

            //output to screen game summary (end game) 
        }

        private async void PlayAgain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
