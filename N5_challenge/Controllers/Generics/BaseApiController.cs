using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace N5_challenge.Controllers.Generics
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
