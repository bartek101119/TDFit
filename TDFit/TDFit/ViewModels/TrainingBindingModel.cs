using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TDFit.Models;
using TDFit.Utils;

namespace TDFit.ViewModel
{

    public class TrainingBindingModel : INotifyPropertyChanged
    {
        public IList<TDiary> ttrainingList { get; set; }
        public IList<TDiary> TrainingList { 
            get { 

                return ttrainingList; 
               
                } 
            set {

                ttrainingList = value;
                OnPropertyChanged();


                }
        }

        public TrainingBindingModel()
        {
            try
            {

                TrainingList = new ObservableCollection<TDiary>();
                //TrainingList.Add(new TDiary { Id = 1, Name = "Pompki", Series = "1", Repeat = 1 });
                //TrainingList.Add(new TDiary { Id = 2, Name = "Podciąganie", Series = "2", Repeat = 2 });
                //TrainingList.Add(new TDiary { Id = 3, Name = "Wyciskanie sztangą", Series = "3", Repeat = 3 });
                //TrainingList.Add(new TDiary { Id = 4, Name = "Wiosłowanie hantlą", Series = "4", Repeat = 4 });

                
                TrainingHTTP.GetAllNewsAsync(list =>
                 {
                     
                     foreach (TDiary item in list)
                         TrainingList.Add(item);
                 });



            }
            catch(Exception ex)
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
