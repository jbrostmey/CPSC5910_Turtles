using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DungeonCrawler.Models;

using Xamarin.Forms.Xaml;


namespace DungeonCrawler.Views.Scores
{

  [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewScorePage : ContentPage
    {
        public Score Data { get; set; }

        //creates a new score to later be saved if the user chooses to
        public NewScorePage()
        {
            InitializeComponent();

            Data = new Score
            {
                Name = "Score name",
                ScoreTotal = 0,
                Id = Guid.NewGuid().ToString(),
                GameDate =  DateTime.Now,
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

        //saves the object and sends a message for ScoresViewModel to pick up
        private async void Save_Clicked(object sender, EventArgs e)
        {
            //if image is null, set default
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = "Score.png";
            }
            //sends message for ScoresViewModel to pick up
            MessagingCenter.Send(this, "AddData", Data);
            //return back to list
            await Navigation.PopAsync();
        }

        //go back to previous page
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
