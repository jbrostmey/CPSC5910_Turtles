using System;

using Xamarin.Forms;
using DungeonCrawler.Services;
namespace DungeonCrawler
{
    public partial class AboutPage : ContentPage
    {
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

    }
}
