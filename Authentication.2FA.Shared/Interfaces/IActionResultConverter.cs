using Authentication._2FA.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentication._2FA.Shared.Interfaces
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false);
    }

}
