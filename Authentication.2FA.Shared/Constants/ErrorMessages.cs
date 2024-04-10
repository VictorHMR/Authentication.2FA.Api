using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Shared.Constants
{
    public static class ErrorMessages
    {
        public static string UserValidationError = "Usuário não validado. Favor validar a conta.";
        public static string WrongUserCredentials= "Email ou Senha incorretos.";
        public static string WrongUserToken = "Token incorreto informado.";



    }
}
