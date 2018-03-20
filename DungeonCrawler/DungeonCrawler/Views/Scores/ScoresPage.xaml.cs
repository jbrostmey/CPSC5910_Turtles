using System;
using System.Collections.Generic;
using DungeonCrawler.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DungeonCrawler.ViewModels;
using DungeonCrawler.Models;

namespace DungeonCrawler.Views.Scores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ScoresPage : ContentPage
    {
    private ScoresViewModel _viewModel;

        //initialize and set view model to global singleton of ScoresViewModel
        public ScoresPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = ScoresViewModel.Instance;
        }

        //On item selected, push Detailed view of that score
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Score;
            //if null, dont do it
            if (data == null)
                return;

            await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(data)));

            //unset selected item
            ScoreListView.SelectedItem = null;
        }

        //pushes an add new score page
        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewScorePage());
        }

        //on appearing, loads any new information from the view model
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            //removes the first toolbar item
            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            //if needed to refresh or dataset is empty, refresh from database
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
