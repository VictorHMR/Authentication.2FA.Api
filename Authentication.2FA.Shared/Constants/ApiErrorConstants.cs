using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Shared.Constants
{
    public class ApiErrorConstants
    {
        public const string VALIDATION_EXCEPTION = "Validation exception";
        public const string SERVICE_UNAVAILABLE = "Service Unavailable";
        public const string FK_VIOLATION_EXCEPTION = "FK violation exception";
        public const string UNIQUE_VIOLATION_EXCEPTION = "Unique violation exception";
        public const string REQUIRED_RESOURCE_NOT_FOUND = "Required resource not found";
        public const string DATA_NOT_FOUND = "Data not found";
        public const string NOT_AUTHORIZED = "Não Autorizado";
        public const string VALIDATION_ERRORS = "There was validation errors";
    }
}
