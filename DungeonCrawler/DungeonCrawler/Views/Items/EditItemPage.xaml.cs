using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DungeonCrawler.Views.Items
{
    public partial class EditItemPage : ContentPage
    {
     
        // ReSharper disable once NotAccessedField.Local
        private ItemDetailViewModel _viewModel;

        public Item Data { get; set; }

        public EditItemPage(ItemDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();
            
            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = "Item.png";
            }
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}














        /* public EditItemPage()
        {
             InitializeComponent();
        }
    }
}
*/