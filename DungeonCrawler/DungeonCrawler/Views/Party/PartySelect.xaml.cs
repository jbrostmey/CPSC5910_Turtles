using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DungeonCrawler.Models;

namespace DungeonCrawler.Views.Party
{
    public enum PartySlots
    {
        first = 0,
        second = 1,
        third = 2,
        fourth = 3,
        fifth = 4,
        sixth = 5,
        unknown = 6
    }
    public partial class PartySelect : ContentPage
    {
        private BattlePageViewModel _viewModel;

        public PartySelect()
        {
            Title = "Prepare Party";
            InitializeComponent();

            BindingContext = _viewModel = BattlePageViewModel.Instance;

            _viewModel.NewParty();
        }

        async void SelectPlayer(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
                return;

            await Navigation.PushAsync(new PartyMemberSelect(data));

            // Manually deselect item.
            CharacterListView.SelectedItem = null;
        }

        async void PlayPressed(Object sender, EventArgs e)
        {
            if(_viewModel.ValidParty())
                await Navigation.PushAsync(new BattlePage());
            else
                await DisplayAlert("Wait!", "You haven't selected all of your characters!", "Try Again!");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            InitializeComponent();

            BindingContext = _viewModel;
        }
    }
}
