using System;

using Xamarin.Forms;
using DungeonCrawler.Models;

using DungeonCrawler.ViewModels;
namespace DungeonCrawler.Views.Scores {  
                    
    public partial class ScoreDetailPage : ContentPage
    {
        private ScoreDetailViewModel _viewModel;

        //initialize and set view model
        public ScoreDetailPage(ScoreDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        //if no view model provided, creates a new score to display and creates a new view model
        public ScoreDetailPage()
        {
            InitializeComponent();

            var data = new Score
            {
                Name = "Score name",
                ScoreTotal = 0,
                Id = Guid.NewGuid().ToString(),
                GameDate = DateTime.Now,
                AutoBattle = false,
                TurnNumber = 1,
                MonsterSlainNumber = 0,
                ExperienceGainedTotal = 0,
                CharacterAtDeathList = "Characters dead",
                MonstersKilledList = "Monsters killed",
                ItemsDroppedList = "Items dropped"

            };

            _viewModel = new ScoreDetailViewModel(data);
            BindingContext = _viewModel;
        }

        // push edit screen for this score
        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditScorePage(_viewModel));
        }

        //push delete screen for this score
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteScorePage(_viewModel));
        }

        //return to previous screen (ScoresPage)
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

    
