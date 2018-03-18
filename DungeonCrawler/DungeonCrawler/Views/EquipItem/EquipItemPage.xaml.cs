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


        private async void Save_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            // Do nothing if equip button is pressed without a character and/or item is selected
            if (itemSelected == null || characterSelected == null)
            {
                await DisplayAlert("Error!", "Please re-select.", "OK");
                return;
            }

            var itemselected = itemSelected as Item;

            // Adds the item to inventory, adds the item to the list of items held by the character at the time.
            BattlePage.Instance.AddItem(itemSelected, characterSelected); // todo: might want to delete this...

            bool previouslyEquipped = false;
            foreach (var character in CharacterInfoListView.ItemsSource)
            {
                var data = character as Character;
                if (data.inventory.ContainsValue(itemselected))
                {
                    previouslyEquipped = true;
                    await DisplayAlert("Item Previously Equipped", "Item has already been equipped.", "OK");
                }

            }

            if (!previouslyEquipped)
            {
                characterSelected.inventory.Add(itemSelected.position, itemselected);
                await DisplayAlert("Equip Item", itemSelected.Text + " Equipped by " + characterSelected.name, "OK");
                // todo: might want to add. await Navigation.PushAsync(new EquipItemSuccessPage(itemselected, characterSelected));
            }

            // Deselect item and character.
            CharacterInfoListView.SelectedItem = null;
            ItemsInfoListView.SelectedItem = null;
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