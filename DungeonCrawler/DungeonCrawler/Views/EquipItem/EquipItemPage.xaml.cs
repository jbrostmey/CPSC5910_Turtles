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

            if (itemSelected == null || characterSelected == null)
            {
                return;
            }

            // add item to inventory
            BattlePage.Instance.AddItem(itemSelected);
            // equip item for character in actor class
            characterSelected.actorItemsCorrespondingToLocation[(int)itemSelected.position - 1] = itemSelected; // todo: might want to add the item instead of the id

            await DisplayAlert("Equip Item", itemSelected.Text + " Equipped!", "Yes", "No");



        }



        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        /*

        // The returned data will be a list of items.  Need to pull that list out
        private List<Item> ParseJson(string myJsonData)
        {
            var myData = new List<Item>();

            try
            {
                JObject json;
                json = JObject.Parse(myJsonData);

                // Data is a List of Items, so need to pull them out one by one...

                var myTempList = json["ItemList"].ToObject<List<JObject>>();

                foreach (var myItem in myTempList)
                {
                    var myTempObject = ConvertFromJson(myItem);
                    if (myTempObject != null)
                    {
                        myData.Add(myTempObject);
                    }
                }

                return myData;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }

        }

        private Item ConvertFromJson(JObject json)
        {
            var myData = new Item();

            try
            {
                myData.Text = JsonHelper.GetJsonString(json, "Text");

                myData.Description = JsonHelper.GetJsonString(json, "Description");


                myData.Guid = JsonHelper.GetJsonString(json, "Guid");
                myData.Id = myData.Guid;

                myData.defense = JsonHelper.GetJsonInteger(json, "Defense");
                myData.speed = JsonHelper.GetJsonInteger(json, "Speed");

                myData.attack = JsonHelper.GetJsonInteger(json, "Attack");

                myData.damage = JsonHelper.GetJsonInteger(json, "Damage");


                myData.range = JsonHelper.GetJsonInteger(json, "Range");

                myData.position = (EquipmentPosition)JsonHelper.GetJsonInteger(json, "Position");
                myData.ImageURI = JsonHelper.GetJsonString(json, "ImageURI");

            }

            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }

            return myData;
        }



        */

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