
﻿using System;

using Xamarin.Forms;
using DungeonCrawler.Services;
using System;
using Xamarin.Forms;
using DungeonCrawler.Services;
using DungeonCrawler.ViewModels;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using DungeonCrawler.Services;
using DungeonCrawler.Controllers;
using DungeonCrawler.Services;
using DungeonCrawler.Controllers;

namespace DungeonCrawler
{
    public partial class AboutPage : ContentPage
    {
        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void DisableRandomNumbersSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            Switch toggleSwitch = sender as Switch;
            if (toggleSwitch.IsToggled)
            {
                App.disabledRandom = true;
            }
        }

        public void ToHit_EditBox(object sender, EventArgs e)
        {
            var text = ((Entry)sender).Text; //cast sender to access the properties of the Entry
            int num = Convert.ToInt32(text);
            Console.WriteLine(num);
            App.inputHitVal = num;
        }


        public AboutPage()
        {
            InitializeComponent();

        }

        private async void InitializeData_Command(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Initialize", "Are you sure you want to re-initialize the data?", "Yes", "No");
            if (answer)
            {
                // Call to the SQL DataStore and have it clear the tables.
                SQLDataStore.Instance.InitializeDatabaseNewTables();
            }
        }


        private void DebugSettingsToggleSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled on, or the SQL if off.
            // SetDataSource(e.Value);
            Switch toggleSwitch = sender as Switch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsToggled == true)
                {
                    // Enable Debug Controlls.

                        UseMockDatabaseSwitch.IsEnabled = true;
                        UseMockDatabaseSwitch.IsVisible = true;

                    UsePostSwitch.IsEnabled = true;
                    UsePostSwitch.IsVisible = true;

                    DisableRandomNumbersSwitch.IsEnabled = true;
                    DisableRandomNumbersSwitch.IsVisible = true;
                   
                }
                if (toggleSwitch.IsToggled == false){
                    UseMockDatabaseSwitch.IsEnabled = false;
                    UseMockDatabaseSwitch.IsVisible = false;

                    UsePostSwitch.IsEnabled = false;
                    UsePostSwitch.IsVisible = false;

                    DisableRandomNumbersSwitch.IsEnabled = false;
                    DisableRandomNumbersSwitch.IsVisible = false;

                }
            }
        }

        private void SetDataSource(bool isMock)
        {
            if (isMock == true)
            {
                ItemsViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                MonsterViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                CharacterViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                ScoresViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
            }
            else
            {
                ItemsViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                MonsterViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                CharacterViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                ScoresViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
            }

            // Have data refresh...
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonsterViewModel.Instance.SetNeedsRefresh(true);
            CharacterViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }
        private void UseMockDatabaseSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            SetDataSource(e.Value);
        }

        private async void UsePostSwitch_OnToggled(object sender, ToggledEventArgs e)
        {

            var answerGet = await DisplayAlert("Get", "Would you like to Get Items from the Server?", "Yes", "No");
            if (answerGet)
            {
                // Call to the Item Service and have it Get the Items
                ItemsController.Instance.GetItemsFromServer();
            }
            // Use Post to get items from the server
 
            var number = 10; // Set the number to 10 in order to get 10 items from the server.
            var level = 20; // Set the level to 20; max value of 6.
            var random = true; // Choose random to be true; random between 1 and level.
            var attribute = Attributes.AttributeEnum.Unknown; // Choose any attribute.
            var location = ItemLocationEnum.Unknown; // Choose any location.
            var updateDatabase = true; // Set the local flag 

            var myDataList = await ItemsController.Instance.GetItemsFromServerPost(number, level, attribute, location, random, updateDatabase);

            var myOutput = string.Empty;
            myOutput = myOutput + "Got from the server:";
            foreach (var item in myDataList)
            {
                myOutput = myOutput + item.Text + "\n" + item.Description;

            }

            var answer = await DisplayAlert("Returned List", myOutput, "Yes", "No");        
        }
      



    }
}

