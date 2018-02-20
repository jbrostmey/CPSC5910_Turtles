﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DungeonCrawler.Views;
using Xamarin.Forms.Xaml;
using DungeonCrawler.Models;

using DungeonCrawler.ViewModels;
namespace DungeonCrawler.Views.Scores {  
                     

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreDetailPage : ContentPage
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private ScoreDetailViewModel _viewModel;

        public ScoreDetailPage(ScoreDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public ScoreDetailPage()
        {
            InitializeComponent();

            var data = new Score
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
                ItemsDroppedList = "Items dropped"

            };

            _viewModel = new ScoreDetailViewModel(data);
            BindingContext = _viewModel;
        }


        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditScorePage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteScorePage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

    
