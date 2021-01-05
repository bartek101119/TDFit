using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDFit.Views;
using Xamarin.Forms;

namespace TDFit
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

       
        }

        //private async void OnLogOut(object sender, EventArgs e)
        //{


        //    Application.Current.MainPage = new NavigationPage(new LoginPage());
        //    Application.Current.Properties["MyToken"] = "";
        //}
        private async void OnKeepDietClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KeepDietPage());
        }
        private async void OnBMIClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BMI());
        }
        private async void OnTrainingDiaryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrainingDiary());
        }
        private async void OnSummaryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SummaryPage());
        }
        private async void OnLoseWeightClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoseWeight());
        }
        private async void OnGainWeightClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GainWeight());
        }
        private async void OnKeepWeightClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KeepWeight());
        }
    }
}
