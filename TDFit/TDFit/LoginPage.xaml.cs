using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        
        }

       /* private async void OnMainPageClicked(object sender, EventArgs e)
        {
            var response = LoginAsync(email.Text, password.Text);
            if (response == "1")
                await DisplayAlert("Brak połączenia", "Nie masz połączenia z internetem lub serwer nie jest dostępny", "OK");
            else if (string.IsNullOrEmpty(response))
                await DisplayAlert("Błędne dane", "Spróbuj ponownie", "OK");
            else
                await Navigation.PushAsync(new MainPage());
        }*/

        public async void OnMainPageClicked(object sender, EventArgs e)
        {
            var result = string.Empty;
            try
            {
                var client = new HttpClient();

                // string jsonData = @"{""Email"" : ""test123@wp.pl"", ""Password"" : ""123456""}";

                LoginModel login = new LoginModel
                {
                    Email = email.Text,
                    Password = password.Text
                };

                var json = JsonConvert.SerializeObject(login);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://192.168.43.212:45459/api/account/login", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                result = await response.Content.ReadAsStringAsync();

                Console.WriteLine(result);
                Application.Current.Properties["MyToken"] = $"{result}";
            }
            catch(Exception xd)
            {
                result = "1";
            }

            if (result == "1")
                await DisplayAlert("Brak połączenia", "Nie masz połączenia z internetem lub serwer nie jest dostępny", "OK");
            else if (string.IsNullOrEmpty(result))
                await DisplayAlert("Błędne dane", "Spróbuj ponownie", "OK");
            else
                await Navigation.PushAsync(new MainPage());
        }

        private async void EnterRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage()); // przejście do rejestracji poprzez tapnięcie(wciśnięcie) napisu (etykiety) konfiguracja w xaml
        }
    }
}