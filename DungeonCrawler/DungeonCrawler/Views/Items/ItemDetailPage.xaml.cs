using System;

using Xamarin.Forms;
using DungeonCrawler.Models;
using DungeonCrawler.Views.Items;
namespace DungeonCrawler
{
    public partial class ItemDetailPage : ContentPage
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private ItemDetailViewModel _viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var data = new Item
            {
                Id = Guid.NewGuid().ToString(),

                Text = "Item 1",
                Description = "This is an item description", //!!
                defense = 1,
                speed = 2,
                attack = 3,
                range = 4,
                position = EquipmentPosition.body



            };

            _viewModel = new ItemDetailViewModel(data);
            BindingContext = _viewModel;
        }


        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditItemPage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteItemPage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}




