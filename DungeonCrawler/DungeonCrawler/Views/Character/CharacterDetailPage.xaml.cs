using System;

using Xamarin.Forms;
using DungeonCrawler.Models;
namespace DungeonCrawler
{
    public partial class CharacterDetailPage : ContentPage
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private CharacterDetailViewModel _viewModel;

        public CharacterDetailPage(CharacterDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public CharacterDetailPage()
        {
            InitializeComponent();

            var data = new Character
            {
                Id = Guid.NewGuid().ToString(),

                name = "Dan",
                description = "Dan is the man",
                characterClass = "paladin",
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

            _viewModel = new CharacterDetailViewModel(data);
            BindingContext = _viewModel;
        }


        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCharacterPage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteCharacterPage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}




