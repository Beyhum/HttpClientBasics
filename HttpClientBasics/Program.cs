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
            appInstance.GetValue().Wait();
            Console.ReadKey();

        }

    }
    
}
