using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DungeonCrawler.Models;

namespace DungeonCrawler.Views.Party
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartyMemberSelect : ContentPage
    {
        private CharacterViewModel _viewModel;
        private Character partyMember;
        public PartyMemberSelect(Character character)
        {
            InitializeComponent();
            partyMember = character;
            BindingContext = _viewModel = CharacterViewModel.Instance;
            Title = "Select Member";
        }

        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs e) 
        {
            var selected = e.SelectedItem as Character;
            var response = await DisplayAlert(selected.name, "Add " + selected.name + " to the party?", "Aye", "Nay");
            if(response)
            {
                partyMember.update(selected);
                await Navigation.PopAsync();
            }
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


        }
    }

}
