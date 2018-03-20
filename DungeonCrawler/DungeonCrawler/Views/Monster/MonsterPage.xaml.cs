using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;


namespace DungeonCrawler
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterPage : ContentPage
    {
        private MonsterViewModel _viewModel;

        public MonsterPage()
        {
            InitializeComponent();
            //BindingContext = _viewModel = MonstersViewModel.Instance;

            BindingContext = _viewModel = MonsterViewModel.Instance;
        }
        //Select monster from list
        private async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
                return;

            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(data)));

            // Manually deselect item.
            MonsterListView.SelectedItem = null;
        }
        //Add monster and pull up new configuration page for object
        private async void AddMonster_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMonsterPage());
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

    