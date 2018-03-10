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

        public Battle battleObj = new Battle();
       
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
            //loop through and select currentChar as the first one that is still alive. otherwise increment
            //if at end of (index 5), restart to index 0 

            int currentChar = 0;
            int currentMon = 0;

            for (int i = 0; i < 6; i++){
                if (battleObj.aChar[i].IsAlive())
                    currentChar = i;
            }

            for (int i = 0; i < 6; i++)
            {
                if (battleObj.aMon[i].IsAlive())
                    currentMon = i;
            }

            battleObj.currentChar = currentChar;
            battleObj.currentMon = currentMon;

            if (battleObj.inSession)
                msg = battleObj.Turn(battleObj.aChar[currentChar], battleObj.aMon[currentMon]);
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

            if (_viewModel.Dataset.Count == 0)
            {
                _viewModel.LoadDataCommand.Execute(null);
            }
            else if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDataCommand.Execute(null);
            }

            BindingContext = _viewModel;

           // DEFINE MESSAGE HERE

            BattleMessageName.Text = msg;
        }

    }
}
