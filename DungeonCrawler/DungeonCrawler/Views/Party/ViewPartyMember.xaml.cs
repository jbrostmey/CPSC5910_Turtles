using System;
using System.Collections.Generic;

using Xamarin.Forms;

using DungeonCrawler.Models;

namespace DungeonCrawler.Views.Party
{
    public partial class ViewPartyMember : ContentPage
    {
        private CharacterDetailViewModel _viewModel;

        public ViewPartyMember(Character character)
        {
            InitializeComponent();
            BindingContext = _viewModel = new CharacterDetailViewModel(character);
            Title = character.name;
        }
    }
}
