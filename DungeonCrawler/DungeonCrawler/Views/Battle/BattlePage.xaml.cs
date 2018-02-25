using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;

using Xamarin.Forms;

namespace DungeonCrawler.Views
{
    public partial class BattlePage : ContentPage
    {

        public Battle battleObj = new Battle();

        public BattlePage()
        {
            InitializeComponent();
            //initialize. OK to have here because we are only creating one instance of BattlePage in OpeningPage
            //Therefor we aren't recreating an instance each time we return to the BattlePage (i.g resets data)
            battleObj.BeginGame();
        }

        private async void Play_Clicked(object sender, EventArgs e)
        {
            int currMon_index = battleObj.currentMon;
            int currChar_index = battleObj.currentChar;
            string msg = "";

            if (battleObj.inSession)
            {
                msg = battleObj.Turn(battleObj.aChar[currChar_index], battleObj.aMon[currMon_index]);
            }

            await Navigation.PushAsync(new BattleMessage(msg));
        }

        //Nonfunctional at this time
        private async void ViewCharacters_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterPage());
        }

        private async void ViewMonsters_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonsterPage());
        }



        //Return to Opening Page
        private async void ExitGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameEnd());
        }


    }
}
