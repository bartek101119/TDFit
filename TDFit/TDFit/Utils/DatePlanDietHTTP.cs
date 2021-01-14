using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using Xamarin.Forms;

namespace TDFit.Utils
{
    public class DatePlanDietHTTP
    {
        public static async Task GetAllNewsAsync(Action<IEnumerable<DatePlanDiet>> action)
        {

            try
            {
                var email = Application.Current.Properties["MyEmail"].ToString();
                var token = Application.Current.Properties["MyToken"].ToString();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync("http://192.168.100.4:45455/api/keepdiet/");


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var list = JsonConvert.DeserializeObject<IEnumerable<DatePlanDiet>>(await response.Content.ReadAsStringAsync());
                    action(list);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
