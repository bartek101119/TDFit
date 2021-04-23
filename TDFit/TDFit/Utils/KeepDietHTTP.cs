using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TDFit.Models;
using TDFit.Views;
using Xamarin.Forms;

namespace TDFit.Utils
{
    public class KeepDietHTTP
    {
        public static async Task GetAllNewsAsync(Action<IEnumerable<KeepDiet>> action)
        {

            try
            {
                var email = Application.Current.Properties["MyEmail"].ToString();
                var token = Application.Current.Properties["MyToken"].ToString();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync($"http://{Profile.ipDoMetodHttp}/api/keepdiet/");


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var list = JsonConvert.DeserializeObject<IEnumerable<KeepDiet>>(await response.Content.ReadAsStringAsync());
                    action(list);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
