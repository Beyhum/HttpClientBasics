using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientBasics
{
    class App
    {

        private HttpClient client;

        public App()
        {
            // setup our HttpClient when our "App" is created
            client = new HttpClient();
        }

        public async Task GetPage()
        {
            // GetAsync makes a GET request to the mentioned url, and returns a response (HttpResponseMessage)
            // The response contains information relevant to our request (its status code, the returned data, ...)
            var response = await client.GetAsync("http://stackoverflow.com");

            // Check if our response was successful
            // Equivalent to retrieving response.StatusCode and checking if it is between 200 and 299 (success range)
            if (response.IsSuccessStatusCode)
            {
                // retrieve the content of the response as a string to output
                string responseBody = await response.Content.ReadAsStringAsync();

                // print our string, which in this case will be an html page
                Console.WriteLine(responseBody);
            }


        }
    }
}
