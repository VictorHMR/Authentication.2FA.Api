using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Application.DTOs.Response
{
    public record MessageSuccessDTO
    {
        public string Message { get; private set; }
        public MessageSuccessDTO(string _Message)
        {
            Message = _Message;
        }
    }
}
