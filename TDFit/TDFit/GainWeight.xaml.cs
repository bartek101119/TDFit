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
        Calorie calorie = new Calorie();
        public GainWeight()
        {
            InitializeComponent();
        }

        private async void OnCalculateClicked(object sender, EventArgs e)
        {
            try { 
            height = double.Parse(growthEntry.Text);
            weight = double.Parse(weightEntry.Text);
            age = double.Parse(ageEntry.Text);

            if (((Calorie)activityEntry.SelectedItem).ActivityId == 1 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                calorie.Kcal = femalebmr * 1.2 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 2 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                calorie.Kcal = femalebmr * 1.3 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 3 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                calorie.Kcal = femalebmr * 1.5 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 4 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                calorie.Kcal = femalebmr * 1.7 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 5 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                femalebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - 161;
                calorie.Kcal = femalebmr * 1.9 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }
            if (((Calorie)activityEntry.SelectedItem).ActivityId == 1 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                calorie.Kcal = malebmr * 1.2 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 2 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                calorie.Kcal = malebmr * 1.3 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 3 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                calorie.Kcal = malebmr * 1.5 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 4 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                calorie.Kcal = malebmr * 1.7 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 5 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                malebmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) + 5;
                calorie.Kcal = malebmr * 1.9 + 300;
                calorie.Fat = calorie.Kcal * 0.25;
                var proteinG = weight * 2;
                calorie.Protein = proteinG * 4;
                calorie.Carbohydrate = calorie.Kcal - calorie.Fat - calorie.Protein;
                var fatG = calorie.Fat / 9;
                var carboG = calorie.Carbohydrate / 4;
                calorie.Protein = proteinG;
                calorie.Fat = fatG;
                calorie.Carbohydrate = carboG;
            }

            await DisplayAlert("TDFit", "Twoje zapotrzebowanie dzienne to: " + Convert.ToInt32(calorie.Kcal) + " kalorii", "Ok");
            }
            catch (Exception hm)
            {
                await DisplayAlert("Błąd", "Uzupełnij wszystkie pola", "Ok");
            }
        }
        private async void OnMacroClicked(object sender, EventArgs e)
        {
            if (calorie.Protein == 0 && calorie.Fat == 0 && calorie.Carbohydrate == 0)
            {
                await DisplayAlert("Uwaga!", "Musisz najpierw obliczyć swoje zapotrzebowanie kaloryczne", "Ok");
            }
            else
            {
                await DisplayAlert("TDFit", "Twoje zapotrzebowanie dzienne na białko: " + Convert.ToInt32(calorie.Protein) + " g" + ", tłuszcze: " + Convert.ToInt32(calorie.Fat) + " g" + ", węglowodany: " + Convert.ToInt32(calorie.Carbohydrate) + " g", "Ok");
            }
        }


    }
}