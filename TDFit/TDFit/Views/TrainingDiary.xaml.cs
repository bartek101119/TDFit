﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
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
        private void btnAddTrainingClicked(object sender, EventArgs e)
        {
            popupAddTrainingView.IsVisible = false;
            string ServiceName = ((ServiceDetails)ServiceEntry.SelectedItem).Name;
            string TotalSeries = ((Series)SeriesEntry.SelectedItem).TotalSeries;
            TDiary trainingBindingModel = new TDiary() { Name = ServiceName, Series = TotalSeries, Repeat = Convert.ToInt32(RepeatEntry.Text) };
            evm.TrainingList.Add(trainingBindingModel);

            BindingContext = evm;

            lstTraining.IsRefreshing = false;

        }

        private void RemoveTrainingTapped(object sender, EventArgs e)
        {
            var button = sender as Image;
            var training = button.BindingContext as TDiary; 
            evm.TrainingList.Remove(training);
            BindingContext = evm;
        }

        private void EditTrainingTapped(object sender, EventArgs e)
        {
            var button = sender as Image;
            var training = button.BindingContext as TDiary;
            ServiceEntry.IsEnabled = false;

            // Bind current selected ServiceName
            List<ServiceDetails> AllServices = GetAllNameExercises().ToList();
            var ServiceID = training.Id;
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

        private void btnUpdateTrainingClicked(object sender, EventArgs e)
        {
            popupAddTrainingView.IsVisible = false;

            int ServiceId = ((ServiceDetails)ServiceEntry.SelectedItem).Id;
            string totalSeries = ((Series)SeriesEntry.SelectedItem).TotalSeries;

            foreach (var item in evm.TrainingList.Where(w => w.Id == ServiceId))
            {
                item.Series = totalSeries;
                item.Repeat = Convert.ToInt32(RepeatEntry.Text);
            }
            BindingContext = evm;
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