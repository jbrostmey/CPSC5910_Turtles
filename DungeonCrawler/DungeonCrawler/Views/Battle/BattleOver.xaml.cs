using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using Xamarin.Forms;
using DungeonCrawler.Views.EquipItem;

namespace DungeonCrawler.Views
{
    public partial class BattleOver : ContentPage
    {
        public string summary { get; set; } 
        public BattleOver()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        //New round display. This view will display a message whenever a new round begins.
        public BattleOver(string message){
            BindingContext = this;
            summary = message;

            InitializeComponent();
        }

        //Pop back to previous page to continue playing
        private async void NextLevel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //Open up equip page to equip items
        private async void OnPickUpItemsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EquipItemPage());
        }

    }
}