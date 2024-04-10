using Authentication._2FA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Shared.Interfaces
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<UseCaseResponse<TResponse>> Execute(TRequest request);
    }
}
