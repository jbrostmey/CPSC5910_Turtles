 using System;

using Xamarin.Forms;
using DungeonCrawler.Models;
namespace DungeonCrawler
{
    public partial class DeleteCharacterPage : ContentPage
    {
        private CharacterDetailViewModel _viewModel;

        public Character Data { get; set; }

        public DeleteCharacterPage(CharacterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }
        // remove character from the database
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteData", Data);

            // Remove Item Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }
        // cancel, go back
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}




