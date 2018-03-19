
using System;
using System.Collections.Generic;
using DungeonCrawler.Services;
using DungeonCrawler.Models;
using DungeonCrawler.ViewModels;
using Newtonsoft.Json.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Threading.Tasks;

namespace DungeonCrawler.Controllers
{
    public class ItemsController
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static ItemsController _instance;
        private static Random RNG;

        public static ItemsController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemsController();
                    RNG = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
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

            if(myList == null){
                Console.WriteLine("mylist is null");
            }
            // Then update the database

            // Use a foreach on myList
       /*     foreach (var item in myList)
            {
                if(item.Id == null){
                    Console.WriteLine("id is null");
                }
                else if (item != null)
                {
                    await ItemsViewModel.Instance.InsertUpdateAsync_Item(item);
                }
            }
*/
            // When foreach is done, call to the items view model to set needs refresh to true, so it can refetch the list...
            ItemsViewModel.Instance.SetNeedsRefresh(true);
        }

        // Asks the server for items based on paramaters
        // Number is th enumber of items to return
        // Level is the Value max for the items
        // Random is to have the value random between 1 and the Level
        // Attribute is a filter to return only items for that attribute, else unknown is used for any
        // Location is a filter to return only items for that location, else unknown is used for any
        //      public async Task<List<Item>> GetItemsFromServerPost(string text, string description, string id, int defense, int speed, int attack, int damage, int range, int position, string imageURI, bool random, bool updateDataBase)//int number, int level, bool random, bool updateDataBase)
        //    {
        //AttributeEnum attribute, ItemLocationEnum location
        //     public async Task<List<Item>> GetItemsFromServerPost(int number, int level, string description, Attributes.AttributeEnum  attribute, ItemLocationEnum location, bool random, bool updateDataBase)
        //   {
        public async Task<List<Item>> GetItemsFromServerPost(int number, int level, Attributes.AttributeEnum attribute, ItemLocationEnum location, bool random, bool updateDataBase)
        {


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

                //number.ToString();
              //  { "Text", attribute.ToString()}, //.ToString()},
             //   { "Description", description.ToString()},
               // { "id", .ToString()},
              //  { "Defense", defense.ToString()},
                //{ "Speed", speed.ToString()},
                //{ "Attack", attack.ToString()},
                //{ "Damage", damage.ToString()},
                //{ "Range", range.ToString()},
               
                //{ "Position", location.ToString()},
               // { "ImageURI", imageURI.ToString()},
                { "Number", number.ToString()},
                {"Level", level.ToString()},
                {"Random", random.ToString()},
                {"Attribute", attribute.ToString()},
                {"Location", location.ToString()},
                {"UpdateDatabase", updateDataBase.ToString()}
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
                myData.Text = JsonHelper.GetJsonString(json, "Name");
                myData.Guid = JsonHelper.GetJsonString(json, "Guid");
                myData.Id = myData.Guid;    // Set to be the same as Guid, does not come down from server, but needed for DB
                myData.Description = JsonHelper.GetJsonString(json, "Description");
                myData.ImageURI = JsonHelper.GetJsonString(json, "ImageURI");
                myData.damage = JsonHelper.GetJsonInteger(json, "Damage");

                var val = JsonHelper.GetJsonInteger(json, "Value");
                switch (val)
                {
                    case 10:
                        myData.speed = val;
                        break;
                    case 12:
                        myData.defense = val;
                        break;

                    case 14:
                        myData.attack = val;
                        break;

                    default:
                        break;
                }

                // Note:  no attributes in mydata for health

                //     myData.range = JsonHelper.GetJsonInteger(json, "Range");

                EquipmentPosition position;
                switch ((ItemLocationEnum)JsonHelper.GetJsonInteger(json, "Location"))
                {
                    case ItemLocationEnum.Head:
                        position = EquipmentPosition.head;
                        break;
                    case ItemLocationEnum.Necklass:
                        position = EquipmentPosition.body;
                        break;
                    case ItemLocationEnum.PrimaryHand:
                        position = EquipmentPosition.rightHand;
                        break;
                    case ItemLocationEnum.Feet:
                        position = EquipmentPosition.feet;
                        break;
                    case ItemLocationEnum.Finger:
                        if (RNG.Next() % 2 == 0)
                            position = EquipmentPosition.rightFinger;
                        else
                            position = EquipmentPosition.leftFinger;
                        break;
                    case ItemLocationEnum.RightFinger:
                        position = EquipmentPosition.rightFinger;
                        break;
                    case ItemLocationEnum.LeftFinger:
                        position = EquipmentPosition.leftFinger;
                        break;
                    case ItemLocationEnum.OffHand:
                        position = EquipmentPosition.leftHand;
                        break;
                    case ItemLocationEnum.Unknown:
                    default:
                        position = EquipmentPosition.unknown;
                        break;
                }

                myData.position = position;
                //todo:
               // myData.position = (ItemLocationEnum)JsonHelper.GetJsonInteger(json, "Location");

     



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
        /*
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

            }
 
            /*  myData.Text = JsonHelper.GetJsonString(json, "Name");
              myData.Guid = JsonHelper.GetJsonString(json, "Guid");
              myData.Id = myData.Guid;    // Set to be the same as Guid, does not come down from server, but needed for DB

              myData.Description = JsonHelper.GetJsonString(json, "Description");
              myData.ImageURI = JsonHelper.GetJsonString(json, "ImageURI");

              myData.Value = JsonHelper.GetJsonInteger(json, "Value");
              myData.Range = JsonHelper.GetJsonInteger(json, "Range");

              myData.Location = (ItemLocationEnum)JsonHelper.GetJsonInteger(json, "Location");
              myData.Attribute = (AttributeEnum)JsonHelper.GetJsonInteger(json, "Attribute");



            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }

            return myData;
        }
    }*/




