using System;
using System.Collections.Generic;

using Xamarin.Forms;

using DungeonCrawler.Models;

namespace DungeonCrawler.Views.Party
{
    public partial class ViewPartyMember : ContentPage
    {
        private Character Data;
        public ViewPartyMember(Character character)
        {
            InitializeComponent();
            Data = character;
        }
    }
}
