using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TDFit.Models;

namespace TDFit.ViewModel
{
   
    public class TrainingBindingModel
    {
        public IList<TDiary> TrainingList { get; set; }

        public TrainingBindingModel()
        {
            try
            {
                
                TrainingList = new ObservableCollection<TDiary>();
                TrainingList.Add(new TDiary { ServiceId = 1, ServiceName = "Podciaganie", Series = "1", Repeat = 1 });
                TrainingList.Add(new TDiary { ServiceId = 2, ServiceName = "Pompki", Series = "2", Repeat = 2 });
                TrainingList.Add(new TDiary { ServiceId = 3, ServiceName = "Wyciskanie sztangą", Series = "3", Repeat = 3 });
                TrainingList.Add(new TDiary { ServiceId = 4, ServiceName = "Wiosłowanie hantlą", Series = "4", Repeat = 4 });
                


            }
            catch(Exception ex)
            {

            }
        }
    }
}
