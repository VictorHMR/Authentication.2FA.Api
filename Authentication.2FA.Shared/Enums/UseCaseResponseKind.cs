using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Shared.Enums
{
    public enum UseCaseResponseKind
    {
        Success,
        DataPersisted,
        DataAccepted,
        InternalServerError,
        RequestValidationError,
        ForeignKeyViolationError,
        UniqueViolationError,
        RequiredResourceNotFound,
        NotFound,
        Unauthorized,
        Forbidden,
        Unavailable

    }
}
