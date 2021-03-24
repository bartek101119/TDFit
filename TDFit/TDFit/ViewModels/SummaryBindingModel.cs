using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using TDFit.Utils;
using Xamarin.Forms;

namespace TDFit.ViewModels
{
    public class SummaryBindingModel : INotifyPropertyChanged
    {

        public IList<Summary> ssummaryList { get; set; }
        public IList<Summary> sssummaryList { get; set; }
        public IList<Summary> ssssummaryList { get; set; }



        public IList<Summary> SummaryList
        {
            get
            {

                return ssummaryList;

            }
            set
            {

                ssummaryList = value;
                OnPropertyChanged();


            }
        }
        public IList<Summary> SsummaryList
        {
            get
            {

                return sssummaryList;

            }
            set
            {

                sssummaryList = value;
                OnPropertyChanged();


            }
        }
        public IList<Summary> SssummaryList
        {
            get
            {

                return ssssummaryList;

            }
            set
            {

                ssssummaryList = value;
                OnPropertyChanged();


            }
        }

        public SummaryBindingModel()
        {
            var email = Application.Current.Properties["MyEmail"].ToString();
            SummaryList = new ObservableCollection<Summary>();
            SsummaryList = new ObservableCollection<Summary>();
            SssummaryList = new ObservableCollection<Summary>();


            try
            {
                SummaryHTTP.GetAllNewsAsync(list =>
                {
                    Summary item1 = list.LastOrDefault(k => k.Email == email && k.CarbohydrateGain == 0 && k.CarbohydrateLose == 0);
                    Summary item2 = list.LastOrDefault(k => k.Email == email && k.CarbohydrateGain == 0 && k.CarbohydrateKeep == 0);
                    Summary item3 = list.LastOrDefault(k => k.Email == email && k.CarbohydrateLose == 0 && k.CarbohydrateKeep == 0);

                    SummaryList.Add(item1);
                    SsummaryList.Add(item2);
                    SssummaryList.Add(item3);

                    //foreach (Summary item in list)
                    //{

                        


                    //    if (item.Email == email && item.CarbohydrateKeep != 0 )
                    //    {

                    //        SummaryList.Add(item);                          
                    //    }
                    //    else if (item.Email == email && item.CarbohydrateLose != 0 )
                    //    {
                    //        SsummaryList.Add(item);
                            
                    //    }
                    //    else if (item.Email == email && item.CarbohydrateGain != 0 )
                    //    {
                    //        SssummaryList.Add(item);
                    //    }



                    //}
                });
            }
            catch(Exception ex)
            {
                
            }
        }

        //&& (Convert.ToBoolean(item.CarbohydrateGain /= numberZero) && Convert.ToBoolean(item.ProteinGain /= numberZero) && Convert.ToBoolean(item.FatGain /= numberZero) && Convert.ToBoolean(item.KcalGain /= numberZero)
        //            && Convert.ToBoolean(item.CarbohydrateKeep == numberZero) && Convert.ToBoolean(item.ProteinKeep == numberZero) && Convert.ToBoolean(item.FatKeep == numberZero) && Convert.ToBoolean(item.KcalKeep == numberZero)
        //            && Convert.ToBoolean(item.CarbohydrateLose == numberZero) && Convert.ToBoolean(item.ProteinLose == numberZero) && Convert.ToBoolean(item.FatLose == numberZero) && Convert.ToBoolean(item.KcalLose == numberZero))

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
