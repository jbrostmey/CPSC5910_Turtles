using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DungeonCrawler.Models;

namespace DungeonCrawler.Views.Party
{
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
    }

}
