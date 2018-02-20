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



            Page itemsPage, aboutPage, scorePage, characterPage = null;

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

                    itemsPage = new ItemsPage();
                    itemsPage = new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse"
                    };
                    scorePage = new  ScoresPage()
                    {
                        Title = "Score"
                    };

                    scorePage = new ScoresPage();
                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    characterPage.Icon = "tab_feed.png";
                    //monsterPage.Icon = "tab_feed.png";
                    itemsPage.Icon = "tab_feed.png";
                    scorePage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    itemsPage = new ItemsPage()
                    {
                        Title = "Browse"
                    };

                    //monsterPage = new MonstersPage()
                    //{
                    //    Title = "Monsters"
                    //};
                    scorePage = new ScoresPage()
                    {
                        Title = "Score"
                    };


                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };
                    break;
            }

            Children.Add(characterPage);
            //Children.Add(monsterPage);
            Children.Add(itemsPage);
            Children.Add(scorePage);
            Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
