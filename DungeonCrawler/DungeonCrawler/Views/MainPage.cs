using System;

using Xamarin.Forms;
using DungeonCrawler.Views.Scores;
namespace DungeonCrawler
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {



            Page itemsPage, aboutPage, scorePage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS: //bryan was here
                    itemsPage = new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse"
                    };
                    scorePage = new NavigationPage(new ScoresPage())
                    {
                        Title = "Score"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    itemsPage.Icon = "tab_feed.png";
                    scorePage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    itemsPage = new ItemsPage()
                    {
                        Title = "Browse"
                    };

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
