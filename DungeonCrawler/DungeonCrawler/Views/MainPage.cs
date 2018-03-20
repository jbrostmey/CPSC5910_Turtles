using System;

using Xamarin.Forms;
using DungeonCrawler.Views.Scores;
using DungeonCrawler.Views;
namespace DungeonCrawler
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {


            //used for our CRUDi
            Page itemsPage, monsterPage, scorePage, characterPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS: 
                    characterPage = new CharacterPage()
                    {
                        Title = "Characters"
                    };

                    monsterPage = new MonsterPage()
                    {
                        Title = "Monsters"
                    };

                    itemsPage = new ItemsPage()
                    {
                        Title = "Items"
                    };
                    scorePage = new  ScoresPage()
                    {
                        Title = "Score"
                    };
                    characterPage.Icon = "tab_feed.png";
                    monsterPage.Icon = "tab_feed.png";
                    itemsPage.Icon = "tab_feed.png";
                    scorePage.Icon = "tab_feed.png";
                    break;
                default:
                    characterPage = new CharacterPage()
                    {
                        Title = "Characters"
                    };
                    itemsPage = new ItemsPage()
                    {
                        Title = "Items"
                    };

                    monsterPage = new MonsterPage()
                    {
                        Title = "Monsters"
                    };
                    scorePage = new ScoresPage()
                    {
                        Title = "Score"
                    };
                    break;



            }

            //adds them in the order we want the tabs
            Children.Add(characterPage);
            Children.Add(monsterPage);
            Children.Add(itemsPage);
            Children.Add(scorePage);

            Title = Children[0].Title;
        }

        //updates title per tab
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
