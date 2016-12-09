using HttpClientBasics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientBasics
{
    class Authenticator
    {

        private CustomClient client;

        public Authenticator()
        {
            // we're using the singleton pattern to always retrieve the same http client
            // the client which we call in "Authenticator" will be the same one as in "App"
            client = CustomClient.Instance;
        }


        public async Task Register()
        {

            Console.WriteLine("Email: ");

            var email = Console.ReadLine();

            Console.WriteLine("Do NOT use any kind of real password. Connection is not HTTPS secured");
            Console.WriteLine("Pword: ");
            var pword = Console.ReadLine();

            Console.WriteLine("Confirmed Pword: ");
            var confirmedPword = Console.ReadLine();

            // our server requires that we include a JSON object that represents the user to register
            // we create a class (RegisterDto) that we will serialize into JSON
            RegisterDto registrationForm = new RegisterDto(email, pword, confirmedPword);

            var response = await client.PostAsync("Register", CustomClient.JsonContent(registrationForm));

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to register");
            }else
            {
                Console.WriteLine("Registration successful");
            }

        }

        // In order to access protected resources, we need to prove to the server who we are
        // instead of sending the username and password with each request, we login once and the server creates a token which we retrieve.
        // A token is associated with a user, and whenever it is included in a request, it tells the server that the request is done on behalf of a certain user
        // The connect/token API is in charge of generating a token for the client
        public async Task Authorize()
        {

            Console.WriteLine("Email: ");

            var email = Console.ReadLine();

            Console.WriteLine("Pword: ");
            var pword = Console.ReadLine();

            // our server specifies that the connect/token API only accepts a body that is of x-www-form-urlencoded content-type
            // it does not accept JSON as a valid format. So our request body must use the urlencoded format
            var response = await client.PostAsync("connect/token",
                new StringContent($"username={email}&password={pword}&grant_type=password", Encoding.UTF8, "application/x-www-form-urlencoded"));

            if (response.IsSuccessStatusCode)
            {
                string tokenString = await response.Content.ReadAsStringAsync();

                // we retrieve the token as JSON upon success and deserialize it into an object we can manipulate
                var token = JsonConvert.DeserializeObject<Token>(tokenString);

                Console.WriteLine("Authorization successful. The generated token is:\n" + token.access_token);

                // we then add a default header to our client: the Authorization Header - with the retrieved access token
                // now all requests made through this client will include our token in the Authorization header - our server can now identify which user is making the request
                // since our token is of type Bearer, the header will have the following format: "Authorization: Bearer {token}"
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            }
            else
            {
                Console.WriteLine("Failed to authenticate");
            }

        }
    }
}
