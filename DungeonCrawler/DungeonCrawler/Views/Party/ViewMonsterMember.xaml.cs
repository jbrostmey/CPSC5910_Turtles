using System;
using System.Collections.Generic;

using Xamarin.Forms;

using DungeonCrawler.Models;

namespace DungeonCrawler.Views.Party
{
    public partial class ViewMonsterMember : ContentPage
    {
        private MonsterDetailViewModel _viewModel;
        public ViewMonsterMember(Monster monster)
        {
            InitializeComponent();
            BindingContext = _viewModel = new MonsterDetailViewModel(monster);
        }
    }
}
 