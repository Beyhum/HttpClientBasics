using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientBasics
{
    // create a custom client class to make requests more straightforward
    // we will be using a singleton pattern to reuse the same client through our "App"
    // this is especially useful when moving from page to page in an app while talking to the same server
    // you'll be able to preserve essential info such as the base url, headers and cookies instead of setting them for each request
    class CustomClient : HttpClient
    {
        // our private static "client" field will be our singleton HttpClient which we will reuse
        private static CustomClient client = new CustomClient();

       

        // to use the singleton pattern, we need a private constructor
        // this prevents the creation of new instances of our client outside of our class' scope
        private CustomClient()
        {

            // set the base address for all requests that will be made with this client
            //all of our requests will start with "http://w2backend.azurewebsites.net"
            BaseAddress = new Uri("http://w2backend.azurewebsites.net");

        }

        // our Instance property will be used to access the singleton http client
        // since the client field is static, it is only initialized once when we first reference our class
        // all subsequent calls to CustomClient.Instance will return the same http client
        public static CustomClient Instance{
            get
            {
                return client;
            }
        }

    }
}
