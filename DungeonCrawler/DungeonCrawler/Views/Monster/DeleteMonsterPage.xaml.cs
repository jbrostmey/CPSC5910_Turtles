using System;

using Xamarin.Forms;
using DungeonCrawler.Models;
namespace DungeonCrawler
{
    public partial class DeleteMonsterPage : ContentPage
    {
        private MonsterDetailViewModel _viewModel;

        public Monster Data { get; set; }

        public DeleteMonsterPage(MonsterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        //Remove character and pop back to selection
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteData", Data);

            // Remove Item Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }

        //Cancel action, pop back to prev page
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}




