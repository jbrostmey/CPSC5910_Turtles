using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DungeonCrawler.Views
{
    public partial class GameEnd : ContentPage
    {
        public GameEnd()
        {
            InitializeComponent();
        }

        private async void PlayAgain_Clicked(object sender, EventArgs e)
        {
            //MUST REINITIALIZE SQL DATASTORE...
            var root = Navigation.NavigationStack[0];
            Navigation.InsertPageBefore(new OpeningPage(), root);
            await Navigation.PopToRootAsync();
        }
    }
}
