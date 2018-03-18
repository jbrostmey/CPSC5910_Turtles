using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DungeonCrawler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class BattleMessage : ContentPage
    {
        //END GAME display
        public BattleMessage(string msg)
        {

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
                Text = msg,
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
                await Navigation.PopToRootAsync();
            }
        }
    }
}
