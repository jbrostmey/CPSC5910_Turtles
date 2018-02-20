using System;

using Xamarin.Forms;
using DungeonCrawler.Models;
namespace DungeonCrawler
{
    public partial class MonsterDetailPage : ContentPage
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private MonsterDetailViewModel _viewModel;

        public MonsterDetailPage(MonsterDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public MonsterDetailPage()
        {
            InitializeComponent();

            var data = new Monster
            {
                Id = Guid.NewGuid().ToString(),

                name = "Ooze",
                description = "Gooey and Gross",
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

            _viewModel = new MonsterDetailViewModel(data);
            BindingContext = _viewModel;
        }


        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditMonsterPage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteMonsterPage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}




