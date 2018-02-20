using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DungeonCrawler.Models;

namespace DungeonCrawler
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMonsterPage : ContentPage
    {
        public Monster data { get; set; }

        public NewMonsterPage()
        {
            InitializeComponent();

            data = new Monster
            {
                Id = Guid.NewGuid().ToString(),

                name = "Dan",
                description = "Dan is the man",
            };

            data.attributes.defense = 1;
            data.attributes.alive = true;
            data.attributes.attack = 1;
            data.attributes.attackModifier = 1;
            data.attributes.currentExperience = 0;
            data.attributes.currentHealth = 10;
            data.attributes.currentExperience = 0;
            data.attributes.defenseModifier = 1;
            data.attributes.health = 10;
            data.attributes.level = 1;
            data.attributes.speed = 1;
            data.attributes.speedModifier = 1;

            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddData", data);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

 