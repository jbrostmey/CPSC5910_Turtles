using System;

using Xamarin.Forms;

using DungeonCrawler.Views;
using SQLite;
using DungeonCrawler.Services;

namespace DungeonCrawler
{


    public partial class App : Application
    {

        // What number should return for random numbers (1 is good choice...)
        public static int globalForcedRandomValue = 2;
        // What number to use for ToHit values (1,2, 19, 20)
        public static int globalForceToHitValue = 20;
        public static int inputHitVal = 0;
        public static bool disabledRandom = false; 


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

