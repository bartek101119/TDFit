using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using TDFit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DietPlan : ContentPage
    {
        DietPlanBindingModel dietplan;
        public DietPlan()
        {
            InitializeComponent();

            dietplan = new DietPlanBindingModel();
            BindingContext = dietplan;

        }

     
        private async void RemoveTrainingTappedBreakfast(object sender, EventArgs e)
        {
            try
            {


                if (await DisplayAlert("Potwierdzenie", "Czy chcesz usunąć to ćwiczenie?", "Tak", "Anuluj"))
                {

                    var button = sender as Image;
                    var diet = button.BindingContext as KeepDiet;
                    var token = Application.Current.Properties["MyToken"].ToString();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var result = await client.DeleteAsync("http://192.168.100.4:45455/api/keepdiet/" + $"{diet.Id}");
                    listview.ItemsSource = dietplan.KeepDietList;

                    if (result.IsSuccessStatusCode)
                    {

                        Navigation.InsertPageBefore(new DietPlan(), this);
                        await Navigation.PopAsync();


                    }
                    else
                    {
                        await DisplayAlert("Błąd", "Spróbuj ponownie później", "OK");

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void RemoveTrainingTappedSecondBreakfast(object sender, EventArgs e)
        {
            try
            {


                if (await DisplayAlert("Potwierdzenie", "Czy chcesz usunąć to ćwiczenie?", "Tak", "Anuluj"))
                {

                    var button = sender as Image;
                    var diet = button.BindingContext as KeepDiet;
                    var token = Application.Current.Properties["MyToken"].ToString();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var result = await client.DeleteAsync("http://192.168.100.4:45455/api/keepdiet/" + $"{diet.Id}");
                    listview.ItemsSource = dietplan.KeepDietList;

                    if (result.IsSuccessStatusCode)
                    {

                        Navigation.InsertPageBefore(new DietPlan(), this);
                        await Navigation.PopAsync();


                    }
                    else
                    {
                        await DisplayAlert("Błąd", "Spróbuj ponownie później", "OK");

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void RemoveTrainingTappedDinner(object sender, EventArgs e)
        {
            try
            {


                if (await DisplayAlert("Potwierdzenie", "Czy chcesz usunąć produkt?", "Tak", "Anuluj"))
                {

                    var button = sender as Image;
                    var diet = button.BindingContext as KeepDiet;
                    var token = Application.Current.Properties["MyToken"].ToString();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var result = await client.DeleteAsync("http://192.168.100.4:45455/api/keepdiet/" + $"{diet.Id}");
                    listview.ItemsSource = dietplan.KeepDietList;

                    if (result.IsSuccessStatusCode)
                    {

                        Navigation.InsertPageBefore(new DietPlan(), this);
                        await Navigation.PopAsync();


                    }
                    else
                    {
                        await DisplayAlert("Błąd", "Spróbuj ponownie później", "OK");

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void RemoveTrainingTappedTea(object sender, EventArgs e)
        {
            try
            {


                if (await DisplayAlert("Potwierdzenie", "Czy chcesz usunąć to ćwiczenie?", "Tak", "Anuluj"))
                {

                    var button = sender as Image;
                    var diet = button.BindingContext as KeepDiet;
                    var token = Application.Current.Properties["MyToken"].ToString();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var result = await client.DeleteAsync("http://192.168.100.4:45455/api/keepdiet/" + $"{diet.Id}");
                    listview.ItemsSource = dietplan.KeepDietList;

                    if (result.IsSuccessStatusCode)
                    {

                        Navigation.InsertPageBefore(new DietPlan(), this);
                        await Navigation.PopAsync();


                    }
                    else
                    {
                        await DisplayAlert("Błąd", "Spróbuj ponownie później", "OK");

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void RemoveTrainingTappedSupper(object sender, EventArgs e)
        {
            try
            {


                if (await DisplayAlert("Potwierdzenie", "Czy chcesz usunąć to ćwiczenie?", "Tak", "Anuluj"))
                {

                    var button = sender as Image;
                    var diet = button.BindingContext as KeepDiet;
                    var token = Application.Current.Properties["MyToken"].ToString();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var result = await client.DeleteAsync("http://192.168.100.4:45455/api/keepdiet/" + $"{diet.Id}");
                    listview.ItemsSource = dietplan.KeepDietList;

                    if (result.IsSuccessStatusCode)
                    {

                        Navigation.InsertPageBefore(new DietPlan(), this);
                        await Navigation.PopAsync();


                    }
                    else
                    {
                        await DisplayAlert("Błąd", "Spróbuj ponownie później", "OK");

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}