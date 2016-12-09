using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientBasics.Models
{

    public class SecretPost
    {
        public string message { get; set; }

        public SecretPost(string secretMessage)
        {
            message = secretMessage;
        }
    }


}
