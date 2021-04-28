using System;
using TimHanewich.Bing;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BingImagesService service = new BingImagesService();
            string[] urls = service.GetImageUrlsAsync("kanye").Result;
            foreach (string s in urls)
            {
                Console.WriteLine(s);
            }

        }
    }
}
