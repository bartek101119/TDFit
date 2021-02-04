using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BMI : ContentPage
    {
        
        double height, weight, bmiT, bmi;
        public double Calories { get; set; }
        Calorie calorie = new Calorie();
        public BMI()
        {
            InitializeComponent();
        }

        private async void OnCalculateClicked(object sender, EventArgs e)
        {
            try
            {
                height = double.Parse(growthEntry.Text);
                weight = double.Parse(weightEntry.Text);

                bmiT = (weight) / Math.Pow(height, 2);

                bmi = bmiT * 10000;
                bmi = Math.Round(bmi, 2);

                if(bmi < 18.5)
                {
                    await DisplayAlert("TDFit", "BMI wynosi: " + bmi + ". Według wskaźnika masz niedowagę", "Ok");
                }
                else if(bmi >= 18.5 && bmi < 25)
                {
                    await DisplayAlert("TDFit", "BMI wynosi: " + bmi + ". Według wskaźnika Twoja waga jest w normie", "Ok");
                }
                else if (bmi >= 25 && bmi < 30)
                {
                    await DisplayAlert("TDFit", "BMI wynosi: " + bmi + ". Według wskaźnika masz nadwagę", "Ok");
                }
                else if (bmi > 30)
                {
                    await DisplayAlert("TDFit", "BMI wynosi: " + bmi + ". Według wskaźnika waga jest za wysoka (otyłość)", "Ok");
                }
            }
            catch (Exception hm)
            {
                await DisplayAlert("Błąd", "Uzupełnij wszystkie pola", "Ok");
            }
        }
    }
}