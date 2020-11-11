using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GainWeight : ContentPage
    {
        double malebmr, femalebmr;
        double height, weight, age;
        public double Calories { get; set; }
        public GainWeight()
        {
            InitializeComponent();
        }

        public async void CalorieCalculator()
        {
            height = double.Parse(growthEntry.Text);
            weight = double.Parse(weightEntry.Text);
            age = double.Parse(ageEntry.Text);

            if (((Calorie)activityEntry.SelectedItem).ActivityId == 1 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.2 + 300;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 2 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.3 + 300;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 3 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.5 + 300;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 4 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.7 + 300;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 5 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.9 + 300;
            }
            if (((Calorie)activityEntry.SelectedItem).ActivityId == 1 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.2 + 300;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 2 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.3 + 300;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 3 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.5 + 300;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 4 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.7 + 300;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 5 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.9 + 300;
            }

            await DisplayAlert("TDFit", "Twoje zapotrzebowanie dzienne to: " + Calories + " kalorii", "Ok");
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            CalorieCalculator();
        }
    }
}