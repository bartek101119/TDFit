using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TDFit.Models;

namespace TDFit.ViewModel
{
    public class Activity
    {
		public IList<Calorie> Activities { get; set; }
        public Activity()
        {

			try
			{
				Activities = new ObservableCollection<Calorie>();
				Activities.Add(new Calorie { ActivityId = 1, Activity = "brak aktywności, praca siedząca" });
				Activities.Add(new Calorie { ActivityId = 2, Activity = "niska aktywność (praca siedząca i 1-2 treningi w tygodniu)" });
				Activities.Add(new Calorie { ActivityId = 3, Activity = "średnia aktywność (praca siedząca i treningi 3-4 razy w tygodniu)" });
				Activities.Add(new Calorie { ActivityId = 4, Activity = "wysoka aktywność (praca fizyczna i 3-4 treningi w tygodniu)" });
				Activities.Add(new Calorie { ActivityId = 5, Activity = "bardzo wysoka aktywność (zawodowi sportowcy, osoby codziennie trenujące)" });


			}
			catch (Exception ex)
			{

				
			}
        }

    }
}
