using System;

using Xamarin.Forms;

using DungeonCrawler.Views;
using SQLite;
using DungeonCrawler.Services;

namespace DungeonCrawler
{


    public partial class App : Application
    {

        public App ()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new OpeningPage());
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }

        static SQLiteAsyncConnection _database;

        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("DungeonCrawler.db3"));
                }
                return _database;
            }
        }

    }
}

