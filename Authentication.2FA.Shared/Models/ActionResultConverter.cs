using Authentication._2FA.Shared.Enums;
using Authentication._2FA.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Authentication._2FA.Shared.Models
{

    public class ActionResultConverter : IActionResultConverter
    {
        private readonly string path;

        public ActionResultConverter(IHttpContextAccessor accessor)
        {
            path = accessor.HttpContext.Request.Path.Value;
        }

        public IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false)
        {
            if (response == null)
                return BuildError(new[] { new ErrorMessage("000", "ActionResultConverter Error") }, UseCaseResponseKind.InternalServerError);

            if (response.Success())
            {
                if (noContentIfSuccess)
                {
                    return new NoContentResult();
                }
                else
                {
                    return BuildSuccessResult(response.Result, response.ResultId, response.Status);
                }
            }
            else if (response.Result != null && response.Errors == null)
            {
                return BuildError(response.Result, response.Status);
            }
            else
            {
                var hasErrors = response.Errors == null || !response.Errors.Any();
                var errorResult = hasErrors
                    ? new[] { new ErrorMessage(response.ResultId ?? "000", response.ErrorMessage ?? "Unknown error") }
                    : response.Errors;

                return BuildError(errorResult, response.Status);
            }
        }

        private IActionResult BuildSuccessResult(object data, string id, UseCaseResponseKind status)
        {
            switch (status)
            {
                case UseCaseResponseKind.DataPersisted:
                    return new CreatedResult($"{path}/{id}", data);
                case UseCaseResponseKind.DataAccepted:
                    return new AcceptedResult($"{path}/{id}", data);
                default:
                    return new OkObjectResult(data);
            }
        }

        private ObjectResult BuildError(object data, UseCaseResponseKind status)
        {
            var httpStatus = GetErrorHttpStatusCode(status);

            return new ObjectResult(data)
            {
                StatusCode = (int)httpStatus
            };
        }

        private HttpStatusCode GetErrorHttpStatusCode(UseCaseResponseKind status)
        {
            switch (status)
            {
                case UseCaseResponseKind.RequestValidationError:
                case UseCaseResponseKind.ForeignKeyViolationError:
                case UseCaseResponseKind.RequiredResourceNotFound:
                    return HttpStatusCode.BadRequest;
                case UseCaseResponseKind.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                case UseCaseResponseKind.Forbidden:
                    return HttpStatusCode.Forbidden;
                case UseCaseResponseKind.NotFound:
                    return HttpStatusCode.NotFound;
                case UseCaseResponseKind.UniqueViolationError:
                    return HttpStatusCode.Conflict;
                case UseCaseResponseKind.Unavailable:
                    return HttpStatusCode.ServiceUnavailable;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
