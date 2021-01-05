using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using TDFit.Utils;
using TDFit.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainingDiary : ContentPage
    {
        public IList<Series> SeriesList { get; set; }
        public IList<ServiceDetails> ExercisesList { get; set; }

        TrainingBindingModel evm;
        public TrainingDiary()
        {
            InitializeComponent();
            evm = new TrainingBindingModel();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ExercisesList = new ObservableCollection<ServiceDetails>();
            ExercisesList = GetAllNameExercises();
            ServiceEntry.ItemsSource = ExercisesList.ToList();
            ServiceEntry.ItemDisplayBinding = new Binding("Name");

            SeriesList = new ObservableCollection<Series>();
            SeriesList = GetSeries();
            SeriesEntry.ItemsSource = SeriesList.ToList();
            SeriesEntry.ItemDisplayBinding = new Binding("TotalSeries");
        }

        private void btnTrainingPopupClicked(object sender, EventArgs e)
        {
            ServiceEntry.IsEnabled = true;
            popupAddTrainingView.IsVisible = true;
            btnAddTraining.IsVisible = true;
            btnUpdateTraining.IsVisible = false;
        }
        void OnImageNameTappedClosePopup(object sender, EventArgs args)
        {
            try
            {
                popupAddTrainingView.IsVisible = false;
            }
            catch (Exception ex)
            {

            }
        }
        private async void btnAddTrainingClicked(object sender, EventArgs e)
        {
            try
            {
                var email = Application.Current.Properties["MyEmail"].ToString();
                var token = Application.Current.Properties["MyToken"].ToString();
                popupAddTrainingView.IsVisible = false;
                string ServiceName = ((ServiceDetails)ServiceEntry.SelectedItem).Name;
                string TotalSeries = ((Series)SeriesEntry.SelectedItem).TotalSeries;
                TDiary trainingBindingModel = new TDiary() {
                    Name = ServiceName, 
                    Series = TotalSeries,
                    Repeat = Convert.ToInt32(RepeatEntry.Text),
                    Email = email
                };

                evm.TrainingList.Add(trainingBindingModel);
               
                BindingContext = evm;

                lstTraining.IsRefreshing = false;

                var json = JsonConvert.SerializeObject(trainingBindingModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.PostAsync("http://192.168.100.4:45455/api/training", content);

                lstTraining.ItemsSource = evm.TrainingList;

                if (result.IsSuccessStatusCode)
                {
                   
                    Navigation.InsertPageBefore(new TrainingDiary(), this);
                    await Navigation.PopAsync();


                }
                else
                {
                    await DisplayAlert("Błąd", "Dodanie ćwiczenia niepowiodło się. Uzupełnij wszystkie pola.", "Ok");
                }

            }
            catch(Exception ex)
            {
                await DisplayAlert("Błąd", "Dodanie ćwiczenia niepowiodło się. Uzupełnij wszystkie pola.", "Ok");
            }

        }
     
        private async void RemoveTrainingTapped(object sender, EventArgs e)
        {
            try
            {


                if (await DisplayAlert("Potwierdzenie", "Czy chcesz usunąć to ćwiczenie?", "Tak", "Anuluj"))
                {
               
                    var button = sender as Image;
                    var training = button.BindingContext as TDiary;
                    var token = Application.Current.Properties["MyToken"].ToString();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var result = await client.DeleteAsync("http://192.168.100.4:45455/api/training/" + $"{training.Id}");
                    lstTraining.ItemsSource = evm.TrainingList;

                    if (result.IsSuccessStatusCode)
                    {
                        
                        Navigation.InsertPageBefore(new TrainingDiary(), this);
                        await Navigation.PopAsync();
                       

                    }
                    else
                    {
                        await DisplayAlert("Błąd", "Spróbuj ponownie później", "OK");

                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void EditTrainingTapped(object sender, EventArgs e)
        {
            var button = sender as Image;
            var training = button.BindingContext as TDiary;
            ServiceEntry.IsEnabled = false;
          

            // Bind current selected ServiceName
            List<ServiceDetails> AllServices = GetAllNameExercises().ToList();
            var ServiceID = training.Id;
            Application.Current.Properties["ID"] = ServiceID;
            ServiceDetails serviceDetails = AllServices.ToList().FirstOrDefault(a => a.Id == ServiceID);
            int index = AllServices.ToList().FindIndex(a => a.Id == ServiceID);
            ServiceEntry.ItemsSource = AllServices;
            ServiceEntry.ItemDisplayBinding = new Binding("Name");
            ServiceEntry.SelectedIndex = index;

            // Bind current selected Series
            int SeriesIndex = SeriesList.ToList().FindIndex(a => a.TotalSeries == (training.Series));

            SeriesEntry.SelectedIndex = SeriesIndex;

            // Bind current selected Repeat
            RepeatEntry.Text = Convert.ToString(training.Repeat);

            // Show popup

            btnUpdateTraining.IsVisible = true;
            btnAddTraining.IsVisible = false;
            popupAddTrainingView.IsVisible = true;

        }

        private async void btnUpdateTrainingClicked(object sender, EventArgs e)
        {
            popupAddTrainingView.IsVisible = false;
            var token = Application.Current.Properties["MyToken"].ToString();
            var id = Convert.ToInt32(Application.Current.Properties["ID"].ToString());
            
            string totalSeries = ((Series)SeriesEntry.SelectedItem).TotalSeries;

           
            TDiary diary = new TDiary()
            {
                Series = totalSeries,
                Repeat = Convert.ToInt32(RepeatEntry.Text)
                
            };

            var json = JsonConvert.SerializeObject(diary);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await client.PutAsync("http://192.168.100.4:45455/api/training/" + $"{id}", content);

            if (result.IsSuccessStatusCode)
            {
                
                Navigation.InsertPageBefore(new TrainingDiary(), this);
                await Navigation.PopAsync();


            }
            else
            {
                await DisplayAlert("Błąd", "Spróbuj ponownie później", "OK");

            }

            lstTraining.ItemsSource = evm.TrainingList;
        }

        public List<Series> GetSeries()
        {
            List<Series> seriesList = new List<Series>();
            seriesList.Add(new Series { TotalSeries = "1" });
            seriesList.Add(new Series { TotalSeries = "2" });
            seriesList.Add(new Series { TotalSeries = "3" });
            seriesList.Add(new Series { TotalSeries = "4" });
            seriesList.Add(new Series { TotalSeries = "5" });

            return seriesList;
        }

        public List<ServiceDetails> GetAllNameExercises()
        {
            List<ServiceDetails> ServiceList = new List<ServiceDetails>();
            ServiceList.Add(new ServiceDetails { Id = 1, Name = "Pompki" });
            ServiceList.Add(new ServiceDetails { Id = 2, Name = "Podciąganie" });
            ServiceList.Add(new ServiceDetails { Id = 3, Name = "Wyciskanie sztangą" });
            ServiceList.Add(new ServiceDetails { Id = 4, Name = "Wiosłowanie hantlą" });
            return ServiceList;
        }
    }
}
