using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DungeonCrawler.Models;

namespace DungeonCrawler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class BattleMessage : ContentPage
    {
        private Battle battle;
        //END GAME display. This view will display the message at the end of the game. Typically a summary.
        public BattleMessage(Battle battle)
        {

            this.battle = battle;
            Label header = new Label
            {
                Text = "Battle Message: ",
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
                                                 

            };
            Label message = new Label
            {
                Text = battle.summary,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center


            };

            Button button = new Button
            {
                Text = "Exit Game",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            button.Clicked += ExitGame_Clicked;

            var scroll = new ScrollView();
            Content = scroll;
            var stack = new StackLayout
            {
                Children =
                {
                    header,
                    message,
                    button
                }
            };

            scroll.Content = stack;


            async void ExitGame_Clicked(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new GameEnd(this.battle.currentScore));
            }
        }
    }
}
