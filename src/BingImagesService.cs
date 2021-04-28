using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;

namespace TimHanewich.Bing
{
    public class BingImagesService
    {
        public async Task<string[]> GetImageUrlsAsync(string search_term)
        {
            //"https://www.bing.com/images/search?q=" & SearchTerm & "&form=QBIR" & "&adlt=off" & "&count=100"

            string url = "https://www.bing.com/images/search?q=" + search_term + "&form=QBIR&adlt=off&count=100";
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.GetAsync(url);
            string web = await resp.Content.ReadAsStringAsync();

            System.IO.File.WriteAllText(@"C:\Users\tahan\Downloads\bing.html", web);

            //Get the url's
            List<string> ToReturn = new List<string>();
            List<string> Splitter = new List<string>();
            Splitter.Add("img_cont hoff");
            string[] parts = web.Split(Splitter.ToArray(), StringSplitOptions.None);
            for (int t = 1; t < parts.Length; t++)
            {
                try
                {
                    int loc1 = parts[t].IndexOf("img");
                    loc1 = parts[t].IndexOf("src", loc1 + 1);
                    loc1 = parts[t].IndexOf("\"", loc1 + 1);
                    int loc2 = parts[t].IndexOf("\"", loc1 + 1);
                    string imgurl = parts[t].Substring(loc1 + 1, loc2 - loc1 - 1);
                    ToReturn.Add(imgurl);
                }
                catch
                {

                }
            }

            return ToReturn.ToArray();
        }
    }
}