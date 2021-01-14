using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using TDFit.Models;
using TDFit.Utils;
using Xamarin.Forms;

namespace TDFit.ViewModels
{
    public class DietPlanBindingModel : INotifyPropertyChanged
    {
        public double totalValue;
        public double totalValuee;
        public double totalValueee;
        public double totalValueeee;
        public double Sum
        {
            get
            {
                return totalValue;
            }
            set
            {
                totalValue = value;
                OnPropertyChanged();
            }
        }
        public double SumProtein
        {
            get
            {
                return totalValuee;
            }
            set
            {
                totalValuee = value;
                OnPropertyChanged();
            }
        }
        public double SumCarbo
        {
            get
            {
                return totalValueee;
            }
            set
            {
                totalValueee = value;
                OnPropertyChanged();
            }
        }
        public double SumFat
        {
            get
            {
                return totalValueeee;
            }
            set
            {
                totalValueeee = value;
                OnPropertyChanged();
            }
        }
        public IList<KeepDiet> kkeepDietList { get; set; }

        public IList<KeepDiet> kkeepDietListDate { get; set; }
        public IList<KeepDiet> kkkeepDietListDate { get; set; }
        public IList<KeepDiet> kkkkeepDietListDate { get; set; }
        public IList<KeepDiet> kkkkkeepDietListDate { get; set; }

        public IList<KeepDiet> summarydiet { get; set; }


        public IList<KeepDiet> KeepDietList
        {
            get
            {
                return kkeepDietList;
            }
            set
            {
                kkeepDietList = value;
                OnPropertyChanged();
            }
        }

        public IList<KeepDiet> KeepDietListDate
        {
            get
            {
                return kkeepDietListDate;
            }
            set
            {
                kkeepDietListDate = value;
                OnPropertyChanged();
            }
        }
        public IList<KeepDiet> KeepDietListtt
        {
            get
            {
                return kkkeepDietListDate;
            }
            set
            {
                kkkeepDietListDate = value;
                OnPropertyChanged();
            }
        }
        public IList<KeepDiet> KeepDietListttt
        {
            get
            {
                return kkkkeepDietListDate;
            }
            set
            {
                kkkkeepDietListDate = value;
                OnPropertyChanged();
            }
        }
        public IList<KeepDiet> KeepDietListtttt
        {
            get
            {
                return kkkkkeepDietListDate;
            }
            set
            {
                kkkkkeepDietListDate = value;
                OnPropertyChanged();
            }
        }

        public IList<KeepDiet> Summary
        {
            get
            {
                return summarydiet;
            }
            set
            {
                summarydiet = value;
                OnPropertyChanged();
            }
        }



        public DietPlanBindingModel()
        {
            var email = Application.Current.Properties["MyEmail"].ToString();
            var token = Application.Current.Properties["MyToken"].ToString();
            KeepDietList = new ObservableCollection<KeepDiet>();
            KeepDietListDate = new ObservableCollection<KeepDiet>();
            KeepDietListtt = new ObservableCollection<KeepDiet>();
            KeepDietListttt = new ObservableCollection<KeepDiet>();
            KeepDietListtttt = new ObservableCollection<KeepDiet>();
            Sum = new double();
            SumProtein = new double();
            SumCarbo = new double();
            SumFat = new double();

            Summary = new ObservableCollection<KeepDiet>();

            
            try
            {
                KeepDietHTTP.GetAllNewsAsync(list =>
                {


                    double sum = 0;
                    double sumProtein = 0;
                    double sumCarbo = 0;
                    double sumFat = 0;

                    foreach (KeepDiet item in list)
                    {
                     
                        if (item.Email == email && item.TimeOfEat == "Śniadanie")
                        {

                            KeepDietList.Add(item);

                        }
                        else if(item.Email == email && item.TimeOfEat == "II śniadanie")
                        {
                            KeepDietListDate.Add(item);
                        }
                        else if (item.Email == email && item.TimeOfEat == "Obiad")
                        {
                            KeepDietListtt.Add(item);
                        }
                        else if (item.Email == email && item.TimeOfEat == "Podwieczorek")
                        {
                            KeepDietListttt.Add(item);
                        }
                        else if (item.Email == email && item.TimeOfEat == "Kolacja")
                        {
                            KeepDietListtttt.Add(item);
                        }

                        if (item.Email == email && item.Kcal >= 0)
                        {
                            sum = sum + item.Kcal;
                        }
                        if(item.Email == email && item.Protein >= 0)
                        {
                            sumProtein = sumProtein + item.Protein;
                        }
                        if (item.Email == email && item.Carbohydrate >= 0)
                        {
                            sumCarbo = sumCarbo + item.Carbohydrate;
                        }
                        if (item.Email == email && item.Fat >= 0)
                        {
                            sumFat = sumFat + item.Fat;
                        }

                    }
                    Sum = sum;
                    SumProtein = sumProtein;
                    SumCarbo = sumCarbo;
                    SumFat = sumFat;
                   
                });
                
            }
            catch (Exception ex)
            {

            }
    }

    public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
