using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientBasics.Models
{

    // our Value class represents the Value object which we are retrieving from our server
    // if you have access to the JSON model you want to deserialize, you can simply copy the json string of that model,
    // then in Visual Studio go to Edit->Paste Special->Paste JSON as Classes and you'll get its equivalent as a C# class
    public class Value
    {
        public int id { get; set; }
        public int data { get; set; }
    }

}
