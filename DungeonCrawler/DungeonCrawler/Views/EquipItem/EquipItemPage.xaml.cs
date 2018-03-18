using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using DungeonCrawler.Services;
using Newtonsoft.Json.Linq;
using System.Linq;
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

            // Adds the item to inventory, adds the item to the list of items held by the character at the time.
            BattlePage.Instance.AddItem(itemSelected, characterSelected);

            // Ensures the user an item has been equipped.
            await DisplayAlert("Equip Item", itemSelected.Text + " Equipped!", "Yes", "No");


            var response = false;
            if(BattlePage.Instance.CheckIfAllItemsEquipped() == true) {
                 response = await DisplayAlert("All Items Have Been Equipped.", itemSelected.Text + " All Equipped! Return to Battle?", "Yes", "No");
            };
            // Return to battle.
            if(response) {
                await Navigation.PopAsync();
            }

            itemSelected = null;
            characterSelected = null;

            // deselect item and character.

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