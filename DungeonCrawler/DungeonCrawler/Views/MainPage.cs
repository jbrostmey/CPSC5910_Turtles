using System;

using Xamarin.Forms;
using DungeonCrawler.Views.Scores;
namespace DungeonCrawler
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {




            Page itemsPage, scorePage, characterPage, monsterPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS: //bryan was here
                    characterPage = new CharacterPage()
                    {
                        Title = "Characters"
                    };

                    //monsterPage = new NavigationPage(new MonstersPage())
                    //{
                    //    Title = "Monsters"
                    //};

                    itemsPage = new ItemsPage()
                    {
                        Title = "Items"
                    };
                    scorePage = new  ScoresPage()
                    {
                        Title = "Score"
                    };

                    scorePage = new ScoresPage()
                    {
                        Title = "Scores"
                    };
                    characterPage.Icon = "tab_feed.png";
                    //monsterPage.Icon = "tab_feed.png";
                    itemsPage.Icon = "tab_feed.png";
                    scorePage.Icon = "tab_feed.png";
                    break;
                default:
                    itemsPage = new ItemsPage()
                    {
                        Title = "Browse"
                    };
                    characterPage = new CharacterPage()
                    {
                        Title = "Characters"
                    };

                    //monsterPage = new MonstersPage()
                    //{
                    //    Title = "Monsters"
                    //};

                    itemsPage = new ItemsPage()
                    {
                        Title = "Items"
                    };

                    scorePage = new ScoresPage()
                    {
                        Title = "Scores"
                    };
                    break;
            }

            Children.Add(characterPage);
            //Children.Add(monsterPage);
            Children.Add(itemsPage);
            Children.Add(scorePage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
