using Microsoft.AspNetCore.Mvc;
using Services.Models;

namespace Presentation.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ToActionResult<T>(ServiceResult<T> result, Func<T, IActionResult> onSuccess)
        {
            if (result.IsSuccess) return onSuccess(result.Value!);
            return result.Error switch
            {
                ServiceError.NotFound => NotFound(result.ErrorMessage),
                ServiceError.DependencyNotFound => UnprocessableEntity(result.ErrorMessage),
                _ => StatusCode(500)
            };
        }

        protected IActionResult ToActionResult(ServiceResult result)
        {
            if (result.IsSuccess) return NoContent();
            return result.Error switch
            {
                ServiceError.NotFound => NotFound(result.ErrorMessage),
                ServiceError.DependencyNotFound => UnprocessableEntity(result.ErrorMessage),
                _ => StatusCode(500)
            };
        }
    }
}
