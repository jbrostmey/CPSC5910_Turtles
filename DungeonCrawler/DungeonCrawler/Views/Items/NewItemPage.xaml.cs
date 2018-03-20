using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DungeonCrawler.Models;

namespace DungeonCrawler
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Data { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            // create and initailize new data item
            Data = new Item
            {

                Text = "Item name",
                Description = "This is an item description.",
                defense = 1,
                speed = 2,
                attack = 3,
                range = 4,
                position = EquipmentPosition.body,
      
                Id = Guid.NewGuid().ToString(),
                ImageURI = "Item.png"

            };

            BindingContext = this;
        }

        // add item to the database
        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = "Items.png";
            }
            // add to the database
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }
        // cancel, go back
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
 
 