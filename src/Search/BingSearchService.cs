using System;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TimHanewich.Bing.Search
{
    public class BingSearchService
    {
        private string api_key;

        public BingSearchService(string key)
        {
            api_key = key;
        }

        public async Task<BingSearchResult[]> SearchAsync(string query)
        {
            string q = query.ToLower();
            q = q.Replace(" ", "+");
            string url = "https://api.bing.microsoft.com/v7.0/search?q=" + q;

            HttpRequestMessage req = new HttpRequestMessage();
            req.RequestUri = new Uri(url);
            req.Method = HttpMethod.Get;
            req.Headers.Add("Ocp-Apim-Subscription-Key", api_key);

            //Call
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.SendAsync(req);
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Bing search returned '" + resp.StatusCode.ToString() + "' Msg:" + content);
            }

            
            //Load
            JObject response = JObject.Parse(content);
            JArray webPages = (JArray)response.SelectToken("webPages.value");
            List<BingSearchResult> ToReturn = new List<BingSearchResult>();
            foreach (JObject jo in webPages)
            {
                BingSearchResult ThisResult = new BingSearchResult();

                //name
                JProperty prop_name = jo.Property("name");
                if (prop_name != null)
                {
                    ThisResult.Name = prop_name.Value.ToString();
                }

                //url
                JProperty prop_url = jo.Property("url");
                if (prop_url != null)
                {
                    ThisResult.URL = prop_url.Value.ToString();
                }

                //snippet
                JProperty prop_snippet = jo.Property("snippet");
                if (prop_snippet != null)
                {
                    ThisResult.Snippet = prop_snippet.Value.ToString();
                }

                ToReturn.Add(ThisResult);
            }
            
            return ToReturn.ToArray();
        }
    }
}