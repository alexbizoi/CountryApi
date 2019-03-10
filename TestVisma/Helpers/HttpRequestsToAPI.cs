using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestVisma
{
    public class HttpRequestsToAPI
    {
        private HttpClient _httpClient = new HttpClient();

        public HttpRequestsToAPI()
        {
            _httpClient.BaseAddress = new Uri("https://api.openaq.org/v1/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        //public async Task<CountryList>GetResource()
        //{
        //    var response = await _httpClient.GetAsync("countries");
        //    response.EnsureSuccessStatusCode();
        //    var content = await response.Content.ReadAsStringAsync();
        //    CountryList countries = null;
        //    if (response.Content.Headers.ContentType.MediaType == "application/json")
        //    {
        //       countries = JsonConvert.DeserializeObject<CountryList>(content);
        //    }
        //    return countries;
        //}
        public async Task<List<Country>> GetResources()
        {
            try
            {
                var response = await _httpClient.GetAsync("countries");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                List<Country> countries = new List<Country>();
                if (response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    JObject json = JObject.Parse(content);
                    json.Remove("meta");

                    var jsonList = json["results"].ToList();
                    foreach (var jsonObj in jsonList)
                    {
                        Country country = jsonObj.ToObject<Country>();
                        countries.Add(country);
                    }
                }
                return countries;
            }
            catch (Exception)
            {
                throw new FetchDataFromAPIException("Error while trying to fetch data from API");
            }
        }


    }
}
