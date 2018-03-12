using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DungeonCrawler.Views
{
    public partial class BattleMessage : ContentPage
    {
        public BattleMessage()
        {
            InitializeComponent();
        }

        private async void ContinueGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BattleOver());
        }
    }
}
