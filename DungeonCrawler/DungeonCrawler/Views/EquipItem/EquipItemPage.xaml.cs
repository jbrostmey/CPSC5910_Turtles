using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using DungeonCrawler.Services;

namespace DungeonCrawler.Views.EquipItem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipItemPage : ContentPage
    {
        private EquipItemViewModel _viewModel;
        // list items
        // list characters
        // equip item
        private Character characterSelected;
        private Item itemSelected;

        public EquipItemPage()
        {


            InitializeComponent();


            BindingContext = _viewModel = EquipItemViewModel.Instance;


        }

 

        private void SelectCharacter_Clicked(object sender, SelectedItemChangedEventArgs args)
        {
            characterSelected = args.SelectedItem as Character;
            if (characterSelected == null)
            {
                return;
            }
           // await Navigation.PushAsync(new CharacterPage()); 
        }



        private void SelectItem_Clicked(object sender, SelectedItemChangedEventArgs args)
        {
            itemSelected = args.SelectedItem as Item;
            if (itemSelected == null)
            {
                return;
            }

        }



        private async void Save_Clicked(object sender, EventArgs e)
        {

            if(itemSelected == null || characterSelected == null)
            {
                return;
            }
            await Navigation.PopAsync(); //implement saving equip item
    
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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

        }

    }
}
