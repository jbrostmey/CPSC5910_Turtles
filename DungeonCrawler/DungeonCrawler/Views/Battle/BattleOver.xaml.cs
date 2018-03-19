﻿using System;
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
        }

        //New round display
        public BattleOver(string message){
            BindingContext = this;
            summary = message;

            InitializeComponent();
        }

        private async void NextLevel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        private async void OnPickUpItemsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EquipItemPage());
        }







        //Functions below are not functional at this time.
        private async void OnViewStatistics_ButtonClicked(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();

            //  await Navigation.PopToRootAsync();
        }
    }
}