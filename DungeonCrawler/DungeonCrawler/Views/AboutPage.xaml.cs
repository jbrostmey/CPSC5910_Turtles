using System;

using Xamarin.Forms;
using DungeonCrawler.Services;

using DungeonCrawler.ViewModels;
using DungeonCrawler.Services;
using DungeonCrawler.Controllers;
namespace DungeonCrawler
{
    public partial class AboutPage : ContentPage
    {


        public void DisableRandomNumbersSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            Switch toggleSwitch = sender as Switch;
            if(toggleSwitch.IsToggled)
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

        private void UsePostSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            Console.WriteLine("here");
        }



    }
}
