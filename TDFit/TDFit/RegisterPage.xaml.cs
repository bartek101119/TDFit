using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void EnterLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage()); // przejście do rejestracji poprzez tapnięcie(wciśnięcie) napisu (etykiety) konfiguracja w xaml
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(email.Text) || String.IsNullOrWhiteSpace(password.Text) || String.IsNullOrWhiteSpace(cpassword.Text))
            {
                await DisplayAlert("Błąd", "Wypełnij puste pola", "OK");
                return;
            }
            var uri = new Uri(string.Format("http://192.168.43.212:45459/api/account/register", string.Empty));
            var client = new HttpClient();
            var model = new RegisterModel
            {
                Email = email.Text,
                Password = password.Text,
                ConfirmPassword = cpassword.Text
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Jest OK!");
                await Navigation.PushAsync(new LoginPage());
                await DisplayAlert("Sukces", "Rejestracja zakończona sukcesem", "OK");
            }
            else
            {
                Debug.WriteLine(response);
                await DisplayAlert("Błąd", "Hasło musi składać się z minimum 6 znaków, 1 znaku specjalnego i wielkiej litery!", "OK");
            }
        }
    }
}