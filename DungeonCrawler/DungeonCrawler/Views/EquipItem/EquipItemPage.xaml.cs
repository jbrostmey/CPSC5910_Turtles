using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using DungeonCrawler.Services;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
namespace DungeonCrawler.Views.EquipItem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipItemPage : ContentPage
    {
        private EquipItemViewModel _viewModel;

        // This view lists all available items and characters and allows the user to equip 
        // an item for a given character. The selected item is saved for the actor, and is
        // added to the battle inventory.

        private Character characterSelected;
        private Item itemSelected;

        public EquipItemPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = EquipItemViewModel.Instance;
        }


        // Allows the user to specify the character equipping the item
        private void SelectCharacter_Clicked(object sender, SelectedItemChangedEventArgs args)
        {
            characterSelected = args.SelectedItem as Character;
            if (characterSelected == null)
            {
                return;
            }
        }


        // Allows the user to specify which item to equip
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
            // Do nothing if equip button is pressed without a character and/or item is selected
            if (itemSelected == null || characterSelected == null)
            {
                return;
            }

            // Adds the item to inventory
            BattlePage.Instance.AddItem(itemSelected);

            // Equip item for character in actor class. Item is stored in list corresponding to location
            characterSelected.actorItemsCorrespondingToLocation[(int)itemSelected.position - 1] = itemSelected; 

            // Ensures the user an item has been equipped.
            await DisplayAlert("Equip Item", itemSelected.Text + " Equipped!", "Yes", "No");
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

        }

    }
}