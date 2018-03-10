using System;
using System.Collections.Generic;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using DungeonCrawler.Services;
using DungeonCrawler.Controllers;
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

            // store equipment as a json string so we can save it in the SQL Database.


            Console.WriteLine("item selected text:" + itemSelected.Text);
                   

            // function to take the string and translate into a dictionary of item slots to items.
            var dict = new Dictionary<string, string>
            {
                { "Id", itemSelected.Id},
                {"Text", itemSelected.Text},
                {"Description", itemSelected.Description.ToString()},
                {"Defense", itemSelected.defense.ToString()},
                {"Speed", itemSelected.speed.ToString()},
                {"Attack", itemSelected.attack.ToString()},
                {"Range", itemSelected.range.ToString()},
                {"Position", itemSelected.position.ToString()}
            };

            // update the dictionary
            // Convert parameters to a key value pairs to a json object
            JObject finalContentJson = (JObject)JToken.FromObject(dict);

            //todo: call function using json object or item id
        
            // Parse them
            /*
            var myList = ParseJson(DataResult);

            if (myList == null)
            {
                Console.WriteLine("list is null");
            }
            else {
                var myOutput = string.Empty;
                foreach (var item in myList)
                {
                    myOutput = myOutput + item.Id + "\n" + item.Text + "\n" + item.Description; // output list

                }

                 var answer = await DisplayAlert("Returned List", myOutput, "Yes", "No");
                
           */

        }




           

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


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


                // myData.Id = JsonHelper.GetJsonString(json, "Id");

                //   myData.Guid = JsonHelper.GetJsonString(json, "Guid");
                //    myData.Id = myData.Guid; 
                //   // myData.Id = JsonHelper.GetJsonString(json, "Id");

                //  myData.Id = myData.Id;    // Set to be the same as Guid, does not come down from server, but needed for DB
                // todo: may need to change to guid
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
