using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

using Xamarin.Forms;
using DungeonCrawler.ViewModels;
using DungeonCrawler.Models;
namespace DungeonCrawler.Views.Scores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DeleteScorePage : ContentPage
    {
        
        private ScoreDetailViewModel _viewModel;

        public Score Data { get; set; }

        //constructor
        public DeleteScorePage (ScoreDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        //delete score function, pulls the trigger on a deletion
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            //sends message for ScoresViewModel to pick up and delete the data
            MessagingCenter.Send(this, "DeleteData", Data);

            // Remove Item Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            //popping to root because we got crashes by just popping async...
            await Navigation.PopToRootAsync();
        }

        //pops to previous page
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}


