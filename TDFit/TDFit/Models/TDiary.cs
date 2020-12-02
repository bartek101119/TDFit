using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TDFit.Models
{
    public class TDiary
    {

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Series { get; set; }
        public int Repeat { get; set; }

    }

    public class Series
    {
        public string TotalSeries { get; set; }
    }

    public class ServiceDetails
    {
        public int ServiceId { get; set; }
        public string ServiceIcon { get; set; }
        public string ServiceName { get; set; }
    }

}
