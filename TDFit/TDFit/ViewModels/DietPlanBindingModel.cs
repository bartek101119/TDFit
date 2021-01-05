using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TDFit.Models;
using TDFit.Utils;
using Xamarin.Forms;

namespace TDFit.ViewModels
{
    public class DietPlanBindingModel : INotifyPropertyChanged
    {
        public IList<KeepDiet> kkeepDietList { get; set; }

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

        public DietPlanBindingModel()
        {
            var email = Application.Current.Properties["MyEmail"].ToString();
            KeepDietList = new ObservableCollection<KeepDiet>();

            try
            {
                KeepDietHTTP.GetAllNewsAsync(list =>
                {
                    foreach (KeepDiet item in list)
                    {

                        if (item.Email == email)
                        {

                            KeepDietList.Add(item);

                        }


                    }
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
