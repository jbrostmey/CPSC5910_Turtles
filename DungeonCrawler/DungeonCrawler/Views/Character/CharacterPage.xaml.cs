using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;


namespace DungeonCrawler
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterPage : ContentPage
    {
        private CharacterViewModel _viewModel;

        public CharacterPage()
        {
            InitializeComponent();
            //BindingContext = _viewModel = MonstersViewModel.Instance;

            BindingContext = _viewModel = CharacterViewModel.Instance;
        }

        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
                return;

            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            CharacterListView.SelectedItem = null;
        }

        private async void AddCharacter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewCharacterPage());
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

    