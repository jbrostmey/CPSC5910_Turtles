using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using DungeonCrawler.Services;
namespace DungeonCrawler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class BattlePage : ContentPage
    {
        private BattlePageViewModel _viewModel;
        private String msg;

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
            this.msg = battleObj.PlayHandler();
            if(msg == null)
                await Navigation.PushAsync(new BattleOver(battleObj.summary));

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

            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

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

            var inventory = string.Empty;
            foreach (var item in battleObj.itemInventory)
            {
                inventory = inventory + item.Text + "\n";

            }
            if (battleObj.itemInventory.Count <= 0)
            {
                inventory = "No items in Inventory.";
            }

            BattleInventory.Text = inventory;
        }

    }
}