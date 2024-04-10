using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Shared.Models
{
    public class ErrorMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ErrorMessage()
        {
        }

        public ErrorMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
