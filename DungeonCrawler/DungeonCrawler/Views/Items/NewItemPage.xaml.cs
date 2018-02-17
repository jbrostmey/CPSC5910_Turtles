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

            Data = new Item
            {
                Text = "Item name",
                Description = "This is an item description.",
                defense = 1,
                speed = 2,
                attack = 3,
                range = 1,
                position = EquipmentPosition.body,
                Id = Guid.NewGuid().ToString()
            };

            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

    /*
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}
*/