using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Shared.Constants
{
    public static class ErrorCodes
    {
        public const string RequestValidation = "00.01";
        public const string ValidationError = "01.01";
        public const string AuthenticateError = "70.70";
        public const string NotFoundError = "10.01";
        public const string WrongUserCredentials = "10.09";
        public const string WrongUserToken = "10.09";
        public const string UserValidationError = "10.11";
    }
}
