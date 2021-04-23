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
using TDFit.Views;
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

                var result = await client.PostAsync($"http://{Profile.ipDoMetodHttp}/api/training", content);

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
                    var result = await client.DeleteAsync($"http://{Profile.ipDoMetodHttp}/api/training/" + $"{training.Id}");
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

            var result = await client.PutAsync($"http://{Profile.ipDoMetodHttp}/api/training/" + $"{id}", content);

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
            ServiceList.Add(new ServiceDetails { Id = 1, Name = "Wyciskanie sztangi sprzed głowy" });
            ServiceList.Add(new ServiceDetails { Id = 2, Name = "Wyciskanie sztangi zza głowy" });
            ServiceList.Add(new ServiceDetails { Id = 3, Name = "Wyciskanie sztangielek" });
            ServiceList.Add(new ServiceDetails { Id = 4, Name = "Unoszenie sztangielek bokiem w górę" });
            ServiceList.Add(new ServiceDetails { Id = 5, Name = "Arnoldki" });
            ServiceList.Add(new ServiceDetails { Id = 6, Name = "Unoszenie sztangielek w opadzie tułowia" });
            ServiceList.Add(new ServiceDetails { Id = 7, Name = "Podciąganie sztangi wzdłuż tułowia" });
            ServiceList.Add(new ServiceDetails { Id = 8, Name = "Podciąganie sztangielek wzdłuż tułowia" });
            ServiceList.Add(new ServiceDetails { Id = 9, Name = "Unoszenie ramion w przód ze sztangą" });
            ServiceList.Add(new ServiceDetails { Id = 10, Name = "Unoszenie ramion w przód ze sztangielkami" });
            ServiceList.Add(new ServiceDetails { Id = 11, Name = "Unoszenie ramion ze sztangielkami w leżeniu" });
            ServiceList.Add(new ServiceDetails { Id = 12, Name = "Unoszenie ramion w przód z linkami wyciągu" });
            ServiceList.Add(new ServiceDetails { Id = 13, Name = "Unoszenie ramion bokiem w górę z linkami wyciągu" });
            ServiceList.Add(new ServiceDetails { Id = 14, Name = "Unoszenie ramion bokiem w górę w opadzie tułowia z linkami wyciągu" });
            ServiceList.Add(new ServiceDetails { Id = 15, Name = "Odwrotne rozpiętki" });
            ServiceList.Add(new ServiceDetails { Id = 16, Name = "Wyciskanie sztangi w leżeniu na ławce poziomej" });
            ServiceList.Add(new ServiceDetails { Id = 17, Name = "Wyciskanie sztangielek w leżeniu na ławce poziomej" });
            ServiceList.Add(new ServiceDetails { Id = 18, Name = "Wyciskanie sztangi w leżeniu na ławce skośnej-głową w górę" });
            ServiceList.Add(new ServiceDetails { Id = 19, Name = "Wyciskanie sztangielek w leżeniu na ławce skośnej-głową w górę" });
            ServiceList.Add(new ServiceDetails { Id = 20, Name = "Wyciskanie sztangi w leżeniu na ławce skośnej-głową w dół" });
            ServiceList.Add(new ServiceDetails { Id = 21, Name = "Wyciskanie sztangielek w leżeniu na ławce skośnej-głową w dół" });
            ServiceList.Add(new ServiceDetails { Id = 22, Name = "Rozpiętki ze sztangielkami w leżeniu na ławce poziomej" });
            ServiceList.Add(new ServiceDetails { Id = 23, Name = "Rozpiętki ze sztangielkami w leżeniu na ławce skośnej -głową do góry" });
            ServiceList.Add(new ServiceDetails { Id = 24, Name = "Wyciskanie sztangi w leżeniu na ławce poziomej wąskim uchwytem" });
            ServiceList.Add(new ServiceDetails { Id = 25, Name = "Przenoszenie sztangielki w leżeniu w poprzek ławki poziomej" });
            ServiceList.Add(new ServiceDetails { Id = 26, Name = "Pompki na poręczach" });
            ServiceList.Add(new ServiceDetails { Id = 27, Name = "Rozpiętki w siadzie na maszynie" });
            ServiceList.Add(new ServiceDetails { Id = 28, Name = "Krzyżowanie linek wyciągu w staniu" });
            ServiceList.Add(new ServiceDetails { Id = 29, Name = "Wyciskania poziome w siadzie na maszynie" });
            ServiceList.Add(new ServiceDetails { Id = 30, Name = "Podciąganie na drążku szerokim uchwytem (nachwyt)" });
            ServiceList.Add(new ServiceDetails { Id = 31, Name = "Podciąganie na drążku w uchwycie neutralnym" });
            ServiceList.Add(new ServiceDetails { Id = 32, Name = "Podciąganie na drążku podchwytem" });
            ServiceList.Add(new ServiceDetails { Id = 33, Name = "Podciąganie sztangi w opadzie (wiosłowanie)" });
            ServiceList.Add(new ServiceDetails { Id = 34, Name = "Podciąganie sztangielki w opadzie (wiosłowanie)" });
            ServiceList.Add(new ServiceDetails { Id = 35, Name = "Podciąganie końca sztangi w opadzie" });
            ServiceList.Add(new ServiceDetails { Id = 36, Name = "Przyciąganie linki wyciągu dolnego w siadzie płaskim" });
            ServiceList.Add(new ServiceDetails { Id = 37, Name = "Przyciąganie linki wyciągu górnego w siadzie" });
            ServiceList.Add(new ServiceDetails { Id = 38, Name = "Ściąganie drążka/rączki wyciągu górnego w siadzie szerokim uchwytem (nachwyt)" });
            ServiceList.Add(new ServiceDetails { Id = 39, Name = "Ściąganie drążka/rączki wyciągu górnego w siadzie podchwytem" });
            ServiceList.Add(new ServiceDetails { Id = 40, Name = "Ściąganie drążka/rączki wyciągu górnego w siadzie uchwyt neutralny" });
            ServiceList.Add(new ServiceDetails { Id = 41, Name = "Przenoszenie sztangi w leżeniu na ławce poziomej" });
            ServiceList.Add(new ServiceDetails { Id = 42, Name = "Podciąganie (wiosłowanie) w leżeniu na ławeczce poziomej" });
            ServiceList.Add(new ServiceDetails { Id = 43, Name = "Skłony ze sztangą trzymaną na karku (tzw. dzień dobry)" });
            ServiceList.Add(new ServiceDetails { Id = 44, Name = "Unoszenie tułowia z opadu" });
            ServiceList.Add(new ServiceDetails { Id = 45, Name = "Martwy ciąg" });
            ServiceList.Add(new ServiceDetails { Id = 46, Name = "Martwy ciąg na prostych nogach" });
            ServiceList.Add(new ServiceDetails { Id = 47, Name = "Wznosy barków Sztrugsy" });
            ServiceList.Add(new ServiceDetails { Id = 48, Name = "Przysiady ze sztangą na barkach" });
            ServiceList.Add(new ServiceDetails { Id = 49, Name = "Przysiady ze sztangą trzymaną z przodu" });
            ServiceList.Add(new ServiceDetails { Id = 50, Name = "Hack-przysiady" });
            ServiceList.Add(new ServiceDetails { Id = 51, Name = "Przysiady na suwnicy skośnej (hack-maszynie)" });
            ServiceList.Add(new ServiceDetails { Id = 52, Name = "Syzyfki" });
            ServiceList.Add(new ServiceDetails { Id = 53, Name = "Prostowanie nóg w siadzie" });
            ServiceList.Add(new ServiceDetails { Id = 54, Name = "Wypychanie ciężaru na suwnicy (maszynie)" });
            ServiceList.Add(new ServiceDetails { Id = 55, Name = "Uginanie nóg w leżeniu" });
            ServiceList.Add(new ServiceDetails { Id = 56, Name = "Przysiady wykroczne" });
            ServiceList.Add(new ServiceDetails { Id = 57, Name = "Nożyce" });
            ServiceList.Add(new ServiceDetails { Id = 58, Name = "Wysoki step ze sztangą/sztangielkami" });
            ServiceList.Add(new ServiceDetails { Id = 59, Name = "Odwodzenie nogi w tył" });
            ServiceList.Add(new ServiceDetails { Id = 60, Name = "Ściąganie kolan w siadzie" });
            ServiceList.Add(new ServiceDetails { Id = 61, Name = "Przywodzenie nóg do wewnątrz" });
            ServiceList.Add(new ServiceDetails { Id = 62, Name = "Odwodzenie nóg na zewnątrz" });
            ServiceList.Add(new ServiceDetails { Id = 63, Name = "Martwy ciąg na prostych nogach" });
            ServiceList.Add(new ServiceDetails { Id = 64, Name = "Wspięcia na palce w staniu" });
            ServiceList.Add(new ServiceDetails { Id = 65, Name = "Wspięcia na palce w siadzie" });
            ServiceList.Add(new ServiceDetails { Id = 66, Name = "Ośle wspięcia" });
            ServiceList.Add(new ServiceDetails { Id = 67, Name = "Wspięcia na palce na hack - maszynie" });
            ServiceList.Add(new ServiceDetails { Id = 68, Name = "Wypychanie ciężaru na maszynie/suwnicy palcami nóg" });
            ServiceList.Add(new ServiceDetails { Id = 69, Name = "Odwrotne wspięcia w staniu" });
            ServiceList.Add(new ServiceDetails { Id = 70, Name = "Skłony w leżeniu płasko" });
            ServiceList.Add(new ServiceDetails { Id = 71, Name = "Skłony w leżeniu głową w dół" });
            ServiceList.Add(new ServiceDetails { Id = 72, Name = "Unoszenie nóg w leżeniu na skośnej ławce" });
            ServiceList.Add(new ServiceDetails { Id = 73, Name = "Unoszenie nóg w zwisie na drążku" });
            ServiceList.Add(new ServiceDetails { Id = 74, Name = "Unoszenie nóg w podporze" });
            ServiceList.Add(new ServiceDetails { Id = 75, Name = "Unoszenie kolan w leżeniu płasko" });
            ServiceList.Add(new ServiceDetails { Id = 76, Name = "Skłony tułowia z linką wyciągu siedząc" });
            ServiceList.Add(new ServiceDetails { Id = 77, Name = "Skręty tułowia" });
            ServiceList.Add(new ServiceDetails { Id = 78, Name = "Skłony tułowia z linką wyciągu klęcząc" });
            ServiceList.Add(new ServiceDetails { Id = 79, Name = "Skłony boczne" });
            ServiceList.Add(new ServiceDetails { Id = 80, Name = "Skłony boczne na ławce" });
            ServiceList.Add(new ServiceDetails { Id = 81, Name = "Skręty tułowia w leżeniu" });
            ServiceList.Add(new ServiceDetails { Id = 82, Name = "Uginanie ramion ze sztangą stojąc podchwytem" });
            ServiceList.Add(new ServiceDetails { Id = 83, Name = "Uginanie ramion ze sztangielkami stojąc podchwytem (z supinacją nadgarstka)" });
            ServiceList.Add(new ServiceDetails { Id = 84, Name = "Uginanie ramion ze sztangielkami stojąc (uchwyt młotkowy)" });
            ServiceList.Add(new ServiceDetails { Id = 85, Name = "Uginanie ramion ze sztangą na modlitewniku" });
            ServiceList.Add(new ServiceDetails { Id = 86, Name = "Uginanie ramienia ze sztangielką na modlitewniku" });
            ServiceList.Add(new ServiceDetails { Id = 87, Name = "Uginanie ramion ze sztangielkami w siadzie na ławce skośnej (z supinacją nadgarstka)" });
            ServiceList.Add(new ServiceDetails { Id = 88, Name = "Uginanie ramienia ze sztangielką w siadzie - w podporze o kolano" });
            ServiceList.Add(new ServiceDetails { Id = 89, Name = "Uginanie ramion podchwytem stojąc-z rączką wyciągu" });
            ServiceList.Add(new ServiceDetails { Id = 90, Name = "Uginanie ramion ze sztangą nachwytem stojąc" });
            ServiceList.Add(new ServiceDetails { Id = 91, Name = "Uginanie ramion ze sztangą nachwytem na modlitewniku" });
            ServiceList.Add(new ServiceDetails { Id = 92, Name = "Uginanie nadgarstków podchwytem w siadzie" });
            ServiceList.Add(new ServiceDetails { Id = 93, Name = "Uginanie nadgarstków nachwytem w siadzie" });
            ServiceList.Add(new ServiceDetails { Id = 94, Name = "Prostowanie ramion na wyciągu stojąc" });
            ServiceList.Add(new ServiceDetails { Id = 95, Name = "Wyciskanie francuskie sztangi w siadzie" });
            ServiceList.Add(new ServiceDetails { Id = 96, Name = "Wyciskanie francuskie jednorącz sztangielki w siadzie" });
            ServiceList.Add(new ServiceDetails { Id = 97, Name = "Wyciskanie francuskie sztangi w leżeniu" });
            ServiceList.Add(new ServiceDetails { Id = 98, Name = "Wyciskanie francuskie sztangielki w leżeniu" });
            ServiceList.Add(new ServiceDetails { Id = 99, Name = "Prostowanie ramienia ze sztangielką w opadzie tułowia" });
            ServiceList.Add(new ServiceDetails { Id = 100, Name = "Prostowanie ramion na wyciągu w płaszczyźnie poziomej stojąc" });
            ServiceList.Add(new ServiceDetails { Id = 101, Name = "Prostowanie ramion na wyciągu w płaszczyźnie poziomej w podporze" });
            ServiceList.Add(new ServiceDetails { Id = 102, Name = "Pompki na poręczach" });
            ServiceList.Add(new ServiceDetails { Id = 103, Name = "Pompki w podporze tyłem" });
            ServiceList.Add(new ServiceDetails { Id = 104, Name = "Prostowanie ramienia podchwytem na wyciągu stojąc" });
            ServiceList.Add(new ServiceDetails { Id = 105, Name = "Wyciskanie w leżeniu na ławce poziomej wąskim uchwytem" });
            ServiceList.Add(new ServiceDetails { Id = 106, Name = "Pompki klasyczne" });
            ServiceList.Add(new ServiceDetails { Id = 107, Name = "Pompki na krzesłach (poręczach)" });
            ServiceList.Add(new ServiceDetails { Id = 108, Name = "Pompki boczne" });
            ServiceList.Add(new ServiceDetails { Id = 109, Name = "Pompki w staniu na rękach (głową w dół)" });
            ServiceList.Add(new ServiceDetails { Id = 110, Name = "Pompki przy ścianie stojąc" });
            ServiceList.Add(new ServiceDetails { Id = 111, Name = "Pompki na taborecie" });
            ServiceList.Add(new ServiceDetails { Id = 112, Name = "Pompki w podporze tyłem" });
            ServiceList.Add(new ServiceDetails { Id = 113, Name = "Rozciąganie ekspandera za plecami" });
            ServiceList.Add(new ServiceDetails { Id = 114, Name = "Rozciąganie zamocowanego na końcu ekspandera jednorącz stojąc" });
            ServiceList.Add(new ServiceDetails { Id = 115, Name = "Rozciąganie zamocowanego na końcu ekspandera do brzucha w siadzie" });
            ServiceList.Add(new ServiceDetails { Id = 116, Name = "UUginanie ramienia z ekspanderem stojąc" });
            ServiceList.Add(new ServiceDetails { Id = 117, Name = "Francuskie wyciskanie jednorącz z ekspanderem" });
            ServiceList.Add(new ServiceDetails { Id = 118, Name = "Rozciąganie ekspandera przed sobą" });
            ServiceList.Add(new ServiceDetails { Id = 119, Name = "Rozciąganie ekspandera nad głową" });
            ServiceList.Add(new ServiceDetails { Id = 120, Name = "Unoszenie ramion w górę w leżeniu z ekspanderem" });
            ServiceList.Add(new ServiceDetails { Id = 121, Name = "Unoszenie ramion w górę przodem z ekspanderem" });
            ServiceList.Add(new ServiceDetails { Id = 122, Name = "Unoszenie ramion w górę bokiem z ekspanderem" });
            ServiceList.Add(new ServiceDetails { Id = 123, Name = "Przysiady" });
            ServiceList.Add(new ServiceDetails { Id = 124, Name = "Wspięcia na palce" });
            ServiceList.Add(new ServiceDetails { Id = 125, Name = "Skłony tułowia w leżeniu tyłem" });
            ServiceList.Add(new ServiceDetails { Id = 126, Name = "Unoszenie nóg w leżeniu tyłem" });
            ServiceList.Add(new ServiceDetails { Id = 127, Name = "Nożyce w leżeniu tyłem" });
            ServiceList.Add(new ServiceDetails { Id = 128, Name = "Spinanie brzucha w leżeniu tyłem" });
            ServiceList.Add(new ServiceDetails { Id = 129, Name = "Unoszenie tułowia z leżenia przodem" });
            ServiceList.Sort((x, y) => string.Compare(x.Name, y.Name));

            return ServiceList;
        }
    }
}
