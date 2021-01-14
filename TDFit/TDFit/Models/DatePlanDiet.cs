using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TDFit.Models
{
    public class DatePlanDiet : ObservableCollection<KeepDiet>
    {
        public DateTime Date { get; set; }
        public ObservableCollection<KeepDiet> keepDiet => this;
    }
}
