using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGraphy.Models
{
    public  class OprationResponseModel
    {
        public bool IsSuccessfull { get; set; }

        public string ResponseMessage { get; set; }=string.Empty;
    }
}
