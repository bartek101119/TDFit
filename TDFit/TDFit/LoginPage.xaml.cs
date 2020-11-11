using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        private async void OnMainPageClicked(object sender, EventArgs e)
        {
            var response = await LoginAsync(email.Text, password.Text);
            if (response == "1")
                await DisplayAlert("Brak połączenia", "Nie masz połączenia z internetem lub serwer nie jest dostępny", "OK");
            else if (string.IsNullOrEmpty(response))
                await DisplayAlert("Błędne dane", "Spróbuj ponownie", "OK");
            else
                await Navigation.PushAsync(new MainPage());
        }

        public async Task<string> LoginAsync(string userName, string password)
        {

            string URL = "http://172.17.82.129:44417/api/account/login";
            var accessToken = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    var keyValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Username", userName),
                        new KeyValuePair<string, string>("Password", password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };
                    var request = new HttpRequestMessage(
                        HttpMethod.Post, URL);

                    request.Content = new FormUrlEncodedContent(keyValues);

                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(7);
                    var response = client.SendAsync(request).Result;

                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync();
                        JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(json.Result);
                        var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
                        accessToken = jwtDynamic.Value<string>("access_token");

                        var Username = jwtDynamic.Value<string>("userName");
                        var AccessTokenExpirationDate = accessTokenExpiration;
                        Application.Current.Properties["MyToken"] = $"{accessToken}";
                    }
                }
                catch (Exception ex)
                {
                    accessToken = "1";
                }
            });
            return accessToken;


        }

        private async void EnterRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage()); // przejście do rejestracji poprzez tapnięcie(wciśnięcie) napisu (etykiety) konfiguracja w xaml
        }
    }
}