﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDFit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DietPlan : ContentPage
    {
        DietPlanBindingModel dietplan;
        public DietPlan()
        {
            InitializeComponent();

            dietplan = new DietPlanBindingModel();
            BindingContext = dietplan;
        }
    }
}