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
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        private async void OnChangePasswordClicked(object sender, EventArgs e)
        {
            var token = Application.Current.Properties["MyToken"].ToString();
            if (String.IsNullOrWhiteSpace(email.Text) || String.IsNullOrWhiteSpace(newPassword.Text) || String.IsNullOrWhiteSpace(newPassword.Text))
            {
                await DisplayAlert("Błąd", "Wypełnij puste pola", "OK");
                return;
            }
                  
            var model = new ChangePassword
            {
                Email = email.Text,
                Password = oldPassword.Text,
                newPassword = newPassword.Text,
            };
            var json = JsonConvert.SerializeObject(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await client.PutAsync("http://192.168.100.4:45455/api/account/ChangePassword", content);
            if (result.IsSuccessStatusCode)
            {
                
                await DisplayAlert("Sukces", "Zmiana hasła zakończona sukcesem", "OK");
            }
            else
            {
                
                await DisplayAlert("Błąd", "Spróbuj ponownie", "OK");
            }
        }
    }
}