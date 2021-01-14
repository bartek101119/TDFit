﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TDFit.Models
{
    public class KeepDiet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TimeOfEat { get; set; }
        public string Name { get; set; }
        public double Kcal { get; set; }
        public double Protein { get; set; }
        public double Carbohydrate { get; set; }
        public double Fat { get; set; }
        public double MacroSum { get; set; }
        public double KcalSum { get; set; }
        public string Email { get; set; }
        public int TimeOfEatId { get; set; }
    }

  

}
