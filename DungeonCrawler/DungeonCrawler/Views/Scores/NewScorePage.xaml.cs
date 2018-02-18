using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DungeonCrawler.Views.Scores
{
    public partial class NewScorePage : ContentPage
    {
        public NewScorePage()
        {
            InitializeComponent();
        }
    }
}

/*
 *  [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewScorePage : ContentPage
    {
        public Score Data { get; set; }

        public NewScorePage()
        {
            InitializeComponent();

            Data = new Score
            {
                Name = "Score name",
                ScoreTotal = 0,
                Id = Guid.NewGuid().ToString()
            };

            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
*/