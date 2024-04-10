using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Application.DTOs.Response
{
    public class GenerateConfirmationQRResponseDTO
    {
        public string QRCodeBase64 { get; set; }
    }
}
