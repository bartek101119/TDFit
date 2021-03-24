using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using TDFit.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KeepWeight : ContentPage
    {
        double sexbmr;
        double height, weight, age;
        public double Calories { get; set; }
        Calorie calorie = new Calorie();
        public KeepWeight()
        {
            InitializeComponent();
            
        }

        public void Keep(double numberOne, int numberTwo)
        {
            sexbmr = (9.99 * weight) + (6.25 * height) + (4.92 * age) - numberTwo;
            calorie.Kcal = sexbmr * numberOne;
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

        private async void OnCalculateClicked(object sender, EventArgs e)
        {
            try
            { 
            height = double.Parse(growthEntry.Text);
            weight = double.Parse(weightEntry.Text);
            age = double.Parse(ageEntry.Text);

            if (((Calorie)activityEntry.SelectedItem).ActivityId == 1 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                    Keep(1.2, 161);
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 2 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                    Keep(1.3, 161);
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 3 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                    Keep(1.5, 161);
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 4 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                    Keep(1.7, 161);
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 5 && (((Calorie)genderEntry.SelectedItem).GenderId == 1))
            {
                    Keep(1.9, 161);
            }
            if (((Calorie)activityEntry.SelectedItem).ActivityId == 1 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                    Keep(1.2, -5);
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 2 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                    Keep(1.3, -5);
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 3 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                    Keep(1.5, -5);
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 4 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                    Keep(1.7, -5);
            }
            else if (((Calorie)activityEntry.SelectedItem).ActivityId == 5 && (((Calorie)genderEntry.SelectedItem).GenderId == 2))
            {
                    Keep(1.9, -5);
            }

            await DisplayAlert("TDFit", "Twoje zapotrzebowanie dzienne to: " + Convert.ToInt32(calorie.Kcal) + " kalorii", "Ok");

                var email = Application.Current.Properties["MyEmail"].ToString();
                var token = Application.Current.Properties["MyToken"].ToString();

                Summary model = new Summary()
                {
                   
                    Weight = weight,
                    KcalKeep = calorie.Kcal,
                    CarbohydrateKeep = calorie.Carbohydrate,
                    ProteinKeep = calorie.Protein,
                    FatKeep = calorie.Fat,
                    Email = email
                };

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var result = await client.PostAsync("http://192.168.43.212:45455/api/summary", content);

                Summary model2 = new Summary()
                {
                   
                    Weight = weight,
                    KcalKeep = calorie.Kcal,
                    CarbohydrateKeep = calorie.Carbohydrate,
                    ProteinKeep = calorie.Protein,
                    FatKeep = calorie.Fat,
                    Email = email
                };
                var json1 = JsonConvert.SerializeObject(model2);
                var content1 = new StringContent(json1, Encoding.UTF8, "application/json");
                HttpClient client1 = new HttpClient();
                client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result2 = client1.PutAsync("http://192.168.43.212:45455/api/summary/" + $"{calorie.Id}", content1);
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
                await DisplayAlert("TDFit", "Twoje zapotrzebowanie dzienne na białko: " + Convert.ToInt32(calorie.Protein) + "g" + ", tłuszcze: " + Convert.ToInt32(calorie.Fat) + "g" + ", węglowodany: " + Convert.ToInt32(calorie.Carbohydrate) + "g", "Ok");
            }
        }
    }
}