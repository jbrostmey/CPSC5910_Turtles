﻿using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using DungeonCrawler.Services;
using DungeonCrawler.Views.EquipItem;
using DungeonCrawler.Views.Party;
namespace DungeonCrawler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class BattlePage : ContentPage
    {
        private BattlePageViewModel _viewModel;
        private String msg;
        private String score { get; set; }
        private String round { get; set; }
        public static Battle battleObj = new Battle();
        public static Battle Instance { get { return battleObj; } }

        public BattlePage()
        {
            msg = "Click Play to Begin!";

            InitializeComponent();

            BindingContext = _viewModel = BattlePageViewModel.Instance;

            //initialize. OK to have here because we are only creating one instance of BattlePage in OpeningPage
            //Therefor we aren't recreating an instance each time we return to the BattlePage (i.g resets data)
            battleObj.BeginGame();
        }

        private async void Play_Clicked(object sender, EventArgs e)
        {

            // Move user to equip item page when all monsters are killed and player needs to get 6 more.

            /*
            if (Battle.equipItems == true)
              await Navigation.PushAsync(new EquipItemPage());
*/

           // await Navigation.PushAsync(new EquipItemPage());
            this.msg = battleObj.PlayHandler();
            if(msg == null)
                await Navigation.PushAsync(new BattleMessage(battleObj.summary));
            else if(Battle.newRound)
                await Navigation.PushAsync(new BattleOver(msg));
            
            round = "Battle: " + battleObj.currentScore.BattleNumber;
            score = "Current Score: " + battleObj.currentScore.ScoreTotal;
            OnAppearing();
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

        private async void AutoPlay_Clicked(object sender, EventArgs e)
        {
            //game messages and summary
            string output = battleObj.AutoPlay();
            await Navigation.PushAsync(new BattleMessage(output));
        }


        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
            {
                return;
            }

            await Navigation.PushAsync(new ViewPartyMember(data));

            // Manually deselect item.
            CharacterInfoListView.SelectedItem = null;
        }

        private async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
                return;

            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(data)));

            // Manually deselect item.
            MonsterListView.SelectedItem = null;
        }


        protected override void OnAppearing()
        {
            
            base.OnAppearing();

            BindingContext = null;
            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();


            BindingContext = _viewModel;

            // Battle MEssage defined here 
            BattleMessageName.Text = msg;
            BattleScore.Text = score;
            BattleRound.Text = round;
            var inventoryList = string.Empty;
            foreach (var item in battleObj.itemInventory)
            {
                inventoryList = inventoryList + item.Text + "\n";

            }
            if (battleObj.itemInventory.Count <= 0)
            {
                inventoryList = "No items in Inventory.";
            }
            BattleInventory.Text = inventoryList;
        
        }

    }
}