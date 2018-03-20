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


        // Allows the user to specify which item to equip (pick up)
        private void SelectItem_Clicked(object sender, SelectedItemChangedEventArgs args)
        {
            itemSelected = args.SelectedItem as Item;
            if (itemSelected == null)
            {
                return;
            }
        }


        // Saves the equipped item to the character's inventory.
        private async void Save_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            // Do nothing if equip button is pressed without a character and/or item is selected
            if (itemSelected == null || characterSelected == null)
            {
                await DisplayAlert("Error!", "Please re-select.", "OK");
                return;
            }

            //If player is dead, dont equip
            if(!characterSelected.IsAlive())
            {
                await DisplayAlert("Uh Oh", characterSelected.name + " is dead!", "OK");
                CharacterInfoListView.SelectedItem = null;
                return;
            }
                
            var itemselected = itemSelected as Item;

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
                //If character does not have something there, equip
                if (characterSelected.EquipItem(itemselected))
                {
                    BattlePage.Instance.AddItem(itemSelected); // add item to inventory.
                    await DisplayAlert("Equip Item", itemSelected.Text + " Equipped by " + characterSelected.name, "OK");
                }
                else{ // Else drop item, then equip
                    Item dropped = characterSelected.DropItem(itemselected.position);
                    _viewModel.DatasetItems.Add(dropped);
                    characterSelected.EquipItem(itemselected);
                    await DisplayAlert("Equip Item", itemSelected.Text + " Equipped by " + characterSelected.name + ". Dropped " + dropped.Text, "OK");
                }
                _viewModel.DatasetItems.Remove(itemselected);
            }

            // Deselect item and character.
            CharacterInfoListView.SelectedItem = null;
            ItemsInfoListView.SelectedItem = null;

            OnAppearing();
        }



        // Cancel equipping item and go back to the previous page.
        // Tells each party member to output their items equipped
        // as a string variable so they can be seen by the ViewPartyMember
        // view in order to see what people have equipped.
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            foreach (Character member in _viewModel.Dataset)
                member.ItemSlotsFormatOutput();
            await Navigation.PopAsync();
        }

      
        // Initilaizes and loads data.
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