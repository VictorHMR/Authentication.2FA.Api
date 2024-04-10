using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.DTOs.Response;
using Authentication._2FA.Shared.Interfaces;

namespace Authentication._2FA.Application.Interfaces.UseCases
{
    public interface ICreateUserUseCase: IUseCase<CreateUserRequestDTO, MessageSuccessDTO>
    {
    }
}
