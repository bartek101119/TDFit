using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KeepDietPage : ContentPage
    {
        public KeepDietPage()
        {
            InitializeComponent();
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DietPlan());
        }
        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            try
            {
                var email = Application.Current.Properties["MyEmail"].ToString();
                var token = Application.Current.Properties["MyToken"].ToString();

                //var datePickerDate = dtPck.Date;
                string TimeOfEatName = ((KeepDiet)TimesOfEatEntry.SelectedItem).TimeOfEat;

                KeepDiet keepDietBindingModel = new KeepDiet()
                {
                    //Date = datePickerDate,
                    TimeOfEat = TimeOfEatName,
                    Name = nameEntry.Text,
                    Kcal = Convert.ToDouble(kcalEntry.Text),
                    Protein = Convert.ToDouble(proteinEntry.Text),
                    Carbohydrate = Convert.ToDouble(carboEntry.Text),
                    Fat = Convert.ToDouble(fatEntry.Text),
                    Email = email
                };

                var json = JsonConvert.SerializeObject(keepDietBindingModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.PostAsync("http://192.168.43.212:45455/api/keepdiet", content);

                if (result.IsSuccessStatusCode)
                {
                    await DisplayAlert("TDFit", "Dodanie produktu do listy powiodło się.", "Ok");
                }
                else
                {
                    await DisplayAlert("Błąd", "Dodanie produktu do listy nie powiodło się. Uzupełnij wszystkie pola.", "Ok");
                }

            }
            catch(Exception ex)
            {
                await DisplayAlert("Błąd", "Dodanie produktu do listy nie powiodło się. Uzupełnij wszystkie pola.", "Ok");
            }
        }
    }
}