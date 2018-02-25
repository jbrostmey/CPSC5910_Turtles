using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DungeonCrawler.Models;

namespace DungeonCrawler
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCharacterPage : ContentPage
    {
        public Character data { get; set; }

        public NewCharacterPage()
        {
            InitializeComponent();

            data = new Character
            {
                Id = Guid.NewGuid().ToString(),

                name = "Dan",
                description = "Dan is the man",
                characterClass = "paladin",
                ImageURI = "Character.png"

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

            if (string.IsNullOrEmpty(data.ImageURI))
            {
                data.ImageURI = "Character.png";
            }
            MessagingCenter.Send(this, "AddData", data);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

 