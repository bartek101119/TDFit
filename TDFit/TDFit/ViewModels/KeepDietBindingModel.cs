using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TDFit.Models;

namespace TDFit.ViewModels
{
    public class KeepDietBindingModel
    {
        public IList<KeepDiet> TimesOfEat { get; set; }

        public KeepDietBindingModel()
        {
            try
            {
                TimesOfEat = new ObservableCollection<KeepDiet>();
                TimesOfEat.Add(new KeepDiet { TimeOfEatId = 1, TimeOfEat = "Śniadanie" });
                TimesOfEat.Add(new KeepDiet { TimeOfEatId = 2, TimeOfEat = "II śniadanie" });
                TimesOfEat.Add(new KeepDiet { TimeOfEatId = 3, TimeOfEat = "Obiad" });
                TimesOfEat.Add(new KeepDiet { TimeOfEatId = 4, TimeOfEat = "Podwieczorek" });
                TimesOfEat.Add(new KeepDiet { TimeOfEatId = 5, TimeOfEat = "Kolacja" });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
