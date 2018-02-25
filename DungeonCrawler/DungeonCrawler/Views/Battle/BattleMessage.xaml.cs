using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DungeonCrawler.Views
{
    public partial class BattleMessage : ContentPage
    {
        public string message;
        //message passed in from Turn
        public BattleMessage(string msg)
        {
            InitializeComponent();
            message = msg;
            this.BindingContext = this;

//message passed correctly. Having troubles with databinding and passing message to XAML 
            this.Content = new Label
            {
                Text = message
            };
        }

        private async void ContinueGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BattleOver());
        }
    }
}
