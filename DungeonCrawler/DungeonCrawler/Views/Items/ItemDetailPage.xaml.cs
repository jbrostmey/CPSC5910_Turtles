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
                Text = "Item 1",
                Description = "This is an item description.",
             //   position = EquipmentPosition.head




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





        /*ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
*/