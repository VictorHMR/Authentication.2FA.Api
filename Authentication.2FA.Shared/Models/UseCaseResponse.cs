using Authentication._2FA.Shared.Enums;
using Authentication._2FA.Shared.Interfaces;
using FluentValidation;
using Authentication._2FA.Shared.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Shared.Models
{
    public class UseCaseResponse<T> : IResponse
    {
        public UseCaseResponseKind Status { get; private set; }
        public string ErrorMessage { get; private set; }
        public IEnumerable<ErrorMessage> Errors { get; private set; }

        public T Result { get; private set; }
        public string ResultId { get; private set; }

        public UseCaseResponse<T> SetSuccess(T result)
        {
            Result = result;

            return SetStatus(UseCaseResponseKind.Success);
        }

        public UseCaseResponse<T> SetPersisted(T result, string resultId)
        {
            Result = result;
            ResultId = resultId;

            return SetStatus(UseCaseResponseKind.DataPersisted);
        }

        public UseCaseResponse<T> SetProcessed(T result, string resultId)
        {
            Result = result;
            ResultId = resultId;

            return SetStatus(UseCaseResponseKind.DataAccepted);
        }

        public UseCaseResponse<T> SetInternalServerError(string errorMessage, string resultId = null, IEnumerable<ErrorMessage> errors = null)
        {
            return SetStatus(UseCaseResponseKind.InternalServerError, errorMessage, resultId, errors);
        }

        public UseCaseResponse<T> SetUnavailable(T result)
        {
            Result = result;
            Status = UseCaseResponseKind.Unavailable;
            ErrorMessage = ApiErrorConstants.SERVICE_UNAVAILABLE;
            return this;
        }

        public UseCaseResponse<T> SetRequestValidationError(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetStatus(UseCaseResponseKind.RequestValidationError, errorMessage, ErrorCodes.RequestValidation, errors);
        }

        public UseCaseResponse<T> SetRequestValidationError(ValidationException ex)
        {
            return SetRequestValidationError(ApiErrorConstants.VALIDATION_EXCEPTION, ex.Errors.Select(error => new ErrorMessage(error.ErrorCode, error.ErrorMessage)));
        }

        public UseCaseResponse<T> SetNotFound(ErrorMessage error)
        {
            return SetStatus(UseCaseResponseKind.NotFound, ApiErrorConstants.DATA_NOT_FOUND, ErrorCodes.NotFoundError, new ErrorMessage[] { error });
        }

        public UseCaseResponse<T> SetStatus(UseCaseResponseKind status, string errorMessage = null, string resultId = null, IEnumerable<ErrorMessage> errors = null)
        {
            Status = status;
            ErrorMessage = errorMessage;
            Errors = errors;
            ResultId = resultId;
            return this;
        }

        public bool Success()
        {
            return ErrorMessage is null;
        }
    }
}
