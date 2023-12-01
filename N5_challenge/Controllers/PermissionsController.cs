using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using N5_challenge.Commands;
using N5_challenge.Controllers.Generics;
using N5_challenge.Parameters;
using N5_challenge.Queries;
using static Domain.Helpers.APIHelper;

namespace N5_challenge.Controllers
{
    [ApiVersion(CURRENT_VERSION_API)]
    public class PermissionsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetPermissions([FromQuery] GetAllPermissionsParameters parameters)
        {

            return Ok(await Mediator.Send(new GetAllPermissionsQuery
            {
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                PermissionTypeId = parameters.PermissionTypeId,
                OrderBy = parameters.OrderBy
            }));
        }

        [HttpPost]
        public async Task<IActionResult> RequestPermission([FromBody] CreatePermissionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut(ROUTE_PARAM_ID)]
        public async Task<IActionResult> ModifyPermission([FromBody] UpdatePermissionCommand command, [FromRoute] int id)
        {
            if (command.Id != id)
            {
                throw new ApiException("Id is not valid.");
            }
            return Ok(await Mediator.Send(command));
        }
    }
}