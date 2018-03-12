using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DungeonCrawler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class BattleMessage : ContentPage
    {
        public string message;
        //message passed in from Turn
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
                Text = "Continue Game",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            button.Clicked += ContinueGame_Clicked;

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





            //  InitializeComponent();

            //this.BindingContext = message;






            //message passed correctly. Having troubles with databinding and passing message to XAML 
            /* this.Content = new Label
             {
                 Text = message
             }
         }
 */


            /*      async void ContinueGame_Clicked(object sender, EventArgs e)
                 {
                     await Navigation.PushAsync(new BattleOver());
                 }

         */

            async void ContinueGame_Clicked(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new BattleOver());
            }
        }
    }
}
