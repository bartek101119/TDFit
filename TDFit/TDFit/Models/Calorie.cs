using System;
using System.Collections.Generic;
using System.Text;

namespace TDFit.Models
{
    public class Calorie
    {
        public int Id { get; set; }
        public string Gender { get; set; }

        public int GenderId { get; set; }
        public int Weight { get; set; }
        public int Growth { get; set; }
        public int Age { get; set; }
        public bool Sex { get; set; } // płeć

        public string Activity { get; set; }
        public int ActivityId { get; set; } // aktywnosc fizyczna

        public int Carbohydrate { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }

        public double Kcal { get; set; }
    }
}
