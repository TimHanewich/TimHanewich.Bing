using System;
using TimHanewich.Bing;
using TimHanewich.Bing.Search;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BingSearchService bss = new BingSearchService("");
            BingSearchResult[] results = bss.SearchAsync("oscar sherer").Result;
            Console.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));

        }
    }
}
