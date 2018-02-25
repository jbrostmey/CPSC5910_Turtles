using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DungeonCrawler.Models;

using Xamarin.Forms.Xaml;


namespace DungeonCrawler.Views.Scores
{
    /*
    public partial class NewScorePage : ContentPage
    {
        public NewScorePage()
        {
            InitializeComponent();
        }
    }
}
*/

  [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewScorePage : ContentPage
    {
        public Score Data { get; set; }

        public NewScorePage()
        {
            InitializeComponent();

            Data = new Score
            {
                Name = "Score name",
                ScoreTotal = 0,
                Id = Guid.NewGuid().ToString(),
                GameDate = new DateTime(),
                AutoBattle = false,
                TurnNumber = 1,
                MonsterSlainNumber = 0,
                ExperienceGainedTotal = 0,
                CharacterAtDeathList = "Characters dead",
                MonstersKilledList = "Monsters killed",
                ItemsDroppedList = "Items dropped",
                ImageURI = "Score.png"



            };

            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = "Score.png";
            }
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
