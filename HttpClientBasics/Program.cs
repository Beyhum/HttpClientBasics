using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            App appInstance = new App();
            RunApp(appInstance).Wait();

        }


        static async Task RunApp(App appInstance)
        {
            // create a list of methods which return Tasks, and add all the methods that we'd like to call from our App instance
            var commandList = new List<Func<Task>> { appInstance.GetValue, appInstance.GetValues, appInstance.PostValue };

            while (true)
            {
                // go through all methods in our list and print the index associated with each one
                for (int i = 0; i < commandList.Count; i++)
                {
                    Console.WriteLine($"{i} - {commandList[i].Method.Name}");
                }
                Console.WriteLine("Press any other key to exit");


                try
                {
                    // parse user input into an int which corresponds to the index of the method we want to call
                    int commandIndex = Int32.Parse(Console.ReadLine());

                    if(commandIndex >= commandList.Count || commandIndex < 0)
                    {
                        break;
                    }
                    // call the method at the selected index
                    await commandList[commandIndex]();
                }
                catch (FormatException)
                {
                    break;
                }
            }
        }

    }
    
}
