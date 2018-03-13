
using System;
using System.Collections.Generic;
using DungeonCrawler.Services;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler.Controllers
{
    public class ItemsController
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static ItemsController _instance;

        public static ItemsController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemsController();
                }
                return _instance;
            }
        }

        // Return the Default Image URI for the Local Image for an Item.
        public static string DefaultImageURI = "Item.png";

        public async void GetItemsFromServer(int parameter = 1)
        {
            // parameter is the item group to request.  1, 2, 3, 100

            // Needs to get items from the server
            // Parse them
            // Then update the database
            // Only update fields on existing items
            // Insert new items
            // Then notify the viewmodel of the change

            // Needs to get items from the server

            // URL for the Server
            var URLComponent = "GetItemListPost/";//"BogusURL";

            var DataResult = await HttpClientService.Instance.GetJsonGetAsync(WebGlobals.WebSiteAPIURL + URLComponent + parameter);

            // Parse them
            var myList = ParseJson(DataResult);

            // Then update the database

            // Use a foreach on myList
            foreach (var item in myList)
            {
                await SQLDataStore.Instance.InsertUpdateAsync_Item(item);
            }

            // When foreach is done, call to the items view model to set needs refresh to true, so it can refetch the list...
            ItemsViewModel.Instance.SetNeedsRefresh(true);
        }

        // Asks the server for items based on paramaters
        // Number is th enumber of items to return
        // Level is the Value max for the items
        // Random is to have the value random between 1 and the Level
        // Attribute is a filter to return only items for that attribute, else unknown is used for any
        // Location is a filter to return only items for that location, else unknown is used for any
        public async Task<List<Item>> GetItemsFromServerPost(int number, int level, bool random, bool updateDataBase)
        {
            //AttributeEnum attribute, ItemLocationEnum location


            // Needs to get items from the server
            // Parse them
            // Then update the database
            // Only update fields on existing items
            // Insert new items
            // Then notify the viewmodel of the change

            // Needs to get items from the server

            var URLComponent = "GetItemListPost/";


            var dict = new Dictionary<string, string>
            {
                //TODO: add other variables to dictionary 

              //  { "Text", number.ToString()},
                { "Number", number.ToString()},
                {"Level", level.ToString()},
                {"Random", random.ToString()},
               // {"Attribute", attribute.ToString()},
                //{"Location", location.ToString()},
                {"UpdateDatabase", updateDataBase.ToString()}


            //    {"Location", location.ToString()}

                // dictionary of key value pairs
                // What other parameters to send to the server?

            };

            // Convert parameters to a key value pairs to a json object
            JObject finalContentJson = (JObject)JToken.FromObject(dict);

            // Make a call to the helper.  URL and Parameters
            var DataResult = await HttpClientService.Instance.GetJsonPostAsync(WebGlobals.WebSiteAPIURL + URLComponent, finalContentJson);

            // Parse them
            var myList = ParseJson(DataResult);

            // Then update the database

            // Use a foreach on myList
            if (updateDataBase)
            {
                foreach (var item in myList)
                {
                    await SQLDataStore.Instance.InsertUpdateAsync_Item(item);
                }

                // When foreach is done, call to the items view model to set needs refresh to true, so it can refetch the list...
                ItemsViewModel.Instance.SetNeedsRefresh(true);
            }

            return myList;
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
                myData.Text = JsonHelper.GetJsonString(json, "Text");                  myData.Description = JsonHelper.GetJsonString(json, "Description");                   myData.Guid = JsonHelper.GetJsonString(json, "Guid");                 myData.Id = myData.Guid;                  myData.defense = JsonHelper.GetJsonInteger(json, "Defense");                 myData.speed = JsonHelper.GetJsonInteger(json, "Speed");                  myData.attack = JsonHelper.GetJsonInteger(json, "Attack");                  myData.damage = JsonHelper.GetJsonInteger(json, "Damage");                   myData.range = JsonHelper.GetJsonInteger(json, "Range");                  myData.position = (EquipmentPosition)JsonHelper.GetJsonInteger(json, "Position");                 myData.ImageURI = JsonHelper.GetJsonString(json, "ImageURI"); 
              /*  myData.Text = JsonHelper.GetJsonString(json, "Name");
                myData.Guid = JsonHelper.GetJsonString(json, "Guid");
                myData.Id = myData.Guid;    // Set to be the same as Guid, does not come down from server, but needed for DB

                myData.Description = JsonHelper.GetJsonString(json, "Description");
                myData.ImageURI = JsonHelper.GetJsonString(json, "ImageURI");

                myData.Value = JsonHelper.GetJsonInteger(json, "Value");
                myData.Range = JsonHelper.GetJsonInteger(json, "Range");

                myData.Location = (ItemLocationEnum)JsonHelper.GetJsonInteger(json, "Location");
                myData.Attribute = (AttributeEnum)JsonHelper.GetJsonInteger(json, "Attribute");
*/
            }

            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }

            return myData;
        }


    }
}
