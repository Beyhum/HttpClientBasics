using HttpClientBasics.Models;
using Newtonsoft.Json;
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
            Console.WriteLine("Enter the ID of the value you want to retrieve: ");
            string valueID = Console.ReadLine();

            var response = await client.GetAsync("api/Values/" + valueID);
            if (response.IsSuccessStatusCode)
            {
                // we're currently retrieving our data as a JSON string, but what if we want to manipulate this data on our client?
                // we need to deserialize our JSON: create a class (Value) which will represent our data on the client,
                // then map each parameter in our JSON to a field of that class
                string responseString = await response.Content.ReadAsStringAsync();

                // instead of manually mapping each param in our string to a field of our Value class, we will use a popular library: Newtonsoft.Json
                // to add the package, go to Package Manager Console and type: Install-Package Newtonsoft.Json
                // or right click on your project, go to "Manage Nuget Packages", go to "Browse" and look for "Newtonsoft.Json"

                // We use the JsonConvert class to deserialize a string by mentioning the type we want to deserialize to in the generic parameter
                // the DeserializeObject returns a value of the generic type
                Value deserializedValue = JsonConvert.DeserializeObject<Value>(responseString);

                // now we can manipulate our data as an object in C#
                Console.WriteLine("This value's ID is " + deserializedValue.id);
                Console.WriteLine("This value's data is " + deserializedValue.data);
            }
            else
            {
                Console.WriteLine(response.StatusCode);
            }
        }

        public async Task GetValues()
        {
            // since we set the BaseAddress of our Custom http client to http://w2backend.azurewebsites.net , we don't have to mention the 
            // base address for every request we make through the client. Calling GetAsync("api/Values") is equivalent to GetAsync("http://w2backend.azurewebsites.net/api/Values")
            var response = await client.GetAsync("api/Values");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                // api/Values returns a list of values, so we can give List<Value> as a generic type to our DeserializeObject method,
                // and it will return a List of values
                List<Value> deserializedList = JsonConvert.DeserializeObject<List<Value>>(responseString);

                // now we can manipulate our list of values as an object in C#
                var filteredList = deserializedList.Where(v => v.data > 50).ToList();

                foreach (var value in filteredList)
                {
                    // we filtered through our list to retrieve all values with data larger than 50
                    Console.WriteLine($"id: {value.id} | data: {value.data}");
                }
            }
            else
            {
                Console.WriteLine(response.StatusCode);
            }
        }
    }
}
