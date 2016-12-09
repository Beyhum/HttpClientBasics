using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientBasics.Models
{

    public class ValuePost
    {
        public int data { get; set; }

        public ValuePost(int dataToPost)
        {
            data = dataToPost;
        }
    }

}
