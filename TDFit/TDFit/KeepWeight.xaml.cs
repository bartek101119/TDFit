using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KeepWeight : ContentPage
    {
        double malebmr, femalebmr;
        double height, weight, age;
        public double Calories { get; set; }
        public KeepWeight()
        {
            InitializeComponent();
            
        }

        public async void CalorieCalculator(int numberOne, int numberTwo, int numberThree, int numberFour, int numberFive)
        {
            height = double.Parse(growthEntry.Text);
            weight = double.Parse(weightEntry.Text);
            age = double.Parse(ageEntry.Text);

            if (((Calorie)activityEntry.SelectedItem).ActivityId == numberOne && (((Calorie)genderEntry.SelectedItem).GenderId == numberOne))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.2;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == numberTwo && (((Calorie)genderEntry.SelectedItem).GenderId == numberOne))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.3;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == numberThree && (((Calorie)genderEntry.SelectedItem).GenderId == numberOne))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.5;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == numberFour && (((Calorie)genderEntry.SelectedItem).GenderId == numberOne))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.7;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == numberFive && (((Calorie)genderEntry.SelectedItem).GenderId == numberOne))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                Calories = femalebmr * 1.9;
            }
            if (((Calorie)activityEntry.SelectedItem).ActivityId == numberOne && (((Calorie)genderEntry.SelectedItem).GenderId == numberTwo))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.2;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == numberTwo && (((Calorie)genderEntry.SelectedItem).GenderId == numberTwo))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.3;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == numberThree && (((Calorie)genderEntry.SelectedItem).GenderId == numberTwo))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.5;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == numberFour && (((Calorie)genderEntry.SelectedItem).GenderId == numberTwo))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.7;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == numberFive && (((Calorie)genderEntry.SelectedItem).GenderId == numberTwo))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                Calories = malebmr * 1.9;
            }

            await DisplayAlert("TDFit", "Twoje zapotrzebowanie dzienne to: " + Calories + " kalorii", "Ok");
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            CalorieCalculator(1,2,3,4,5);
        }
    }
}