﻿
﻿using System;

using Xamarin.Forms;
using DungeonCrawler.Services;

using DungeonCrawler.ViewModels;
using DungeonCrawler.Models;

using DungeonCrawler.Controllers;

namespace DungeonCrawler
{
    public partial class AboutPage : ContentPage
    {

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            throw new NotImplementedException();
        }

        //for setting if we want rng in hit rolls (if on, roll is 20)
        public void DisableRandomNumbersSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            Switch toggleSwitch = sender as Switch;
            if (toggleSwitch.IsToggled)
            {
                App.disabledRandom = true;
            }
        }




        public AboutPage()
        {
            InitializeComponent();

            // Set the flag for Mock on or off...
            UseMockDatabaseSwitch.IsToggled = (MasterDataStore.GetDataStoreMockFlag() == DataStoreEnum.Mock);
            SetDataSource(UseMockDatabaseSwitch.IsToggled);
        }

        //confirms request, initializes database if yes
        private async void InitializeData_Command(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Initialize", "Are you sure you want to re-initialize the data?", "Yes", "No");
            if (answer)
            {
                // Call to the SQL DataStore and have it clear the tables and reset.
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

                    PostButton.IsEnabled = true;
                    PostButton.IsVisible = true;

                    DisableRandomNumbersSwitch.IsEnabled = true;
                    DisableRandomNumbersSwitch.IsVisible = true;
                   
                }
                // Disable Debug Controlls.
                if (toggleSwitch.IsToggled == false){
                    UseMockDatabaseSwitch.IsEnabled = false;
                    UseMockDatabaseSwitch.IsVisible = false;

                    PostButton.IsEnabled = false;
                    PostButton.IsVisible = false;

                    DisableRandomNumbersSwitch.IsEnabled = false;
                    DisableRandomNumbersSwitch.IsVisible = false;

                }
            }
        }

        //switches datasource from sql to mock
        private void SetDataSource(bool isMock)
        {
            var set = DataStoreEnum.Sql;

            if (isMock)
            {
                set = DataStoreEnum.Mock;
            }

            //tells MasterDataStore to set to appropriate database
            MasterDataStore.ToggleDataStore(set);

        }

        // Sets the DataStore to be the Mock Data Store if toggled on, or SQL if toggled off.
        private void UseMockDatabaseSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            SetDataSource(e.Value);
        }

        //sends user to the CRUDi pages
        async void CRUDiPageButtonClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        // Button to get the items from the server and insert them into the SQL Database.
        private async void UsePostButtonClick(object sender, EventArgs e)
        {
            // Ask the user if he or she is sure he would like to get the items
            var answerGet = await DisplayAlert("Get Items", "Would you like to Get the Items from the Server?", "Yes", "No");
            if (answerGet)
            {
                // Call to the Item Service and have it Get the Items
                ItemsController.Instance.GetItemsFromServer();
            }

            // Using Post to get items from the server
            var number = 10; // Set the number to 10 in order to get 10 items from the server.
            var level = 20; // Set the level to 20; max value of 6.
            var random = true; // Choose random to be true; random between 1 and level.
            var attribute = Attributes.AttributeEnum.Unknown; // Choose any attribute.
            var location = ItemLocationEnum.Unknown; // Choose any location.
            var updateDatabase = true; // Set the local flag 

            var myDataList = await ItemsController.Instance.GetItemsFromServerPost(number, level, attribute, location, random, updateDatabase);

            var myOutput = string.Empty;
            myOutput = myOutput + "Got the following items from the server:" + "\n";
            foreach (var item in myDataList)
            {
                myOutput = myOutput + item.Text + "\n" + item.Description;

            }

            var answer = await DisplayAlert("Returned List", myOutput, "Yes", "No");        
        }
      
    }
}

