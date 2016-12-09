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

        // CustomClient is our custom HttpClient
        private CustomClient client;

        public App()
        {
            // setup our http client when our "App" is created
            // we're using the singleton pattern to always retrieve the same http client
            client = CustomClient.Instance;
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

        public async Task GetValue()
        {
            // since we set the BaseAddress of our Custom http client to http://w2backend.azurewebsites.net , we don't have to mention the 
            // base address for every request we make through the client. Calling GetAsync("api/Values") is equivalent to GetAsync("http://w2backend.azurewebsites.net/api/Values")
            var response = await client.GetAsync("api/Values");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                // print our response string which is formatted as JSON
                Console.WriteLine(responseString);
            }else
            {
                Console.WriteLine(response.StatusCode);
            }
        }
    }
}
