using System;
using Xamarin.Forms.Xaml;

using Xamarin.Forms;

using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;

namespace DungeonCrawler.Views.Scores
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditScorePage : ContentPage
    {   
    private ScoreDetailViewModel _viewModel;

        public Score Data { get; set; }

        //constructor
        public EditScorePage(ScoreDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();
            
            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        // saves to ScoresViewModel via a message
        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = "Score.png";
            }
            //saves to ScoresViewModel listening for this message
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        //pops to previous page
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
