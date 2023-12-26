namespace Committees.API.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize("ResourceOwnerPolicy")]
    public class BaseController : ControllerBase
    {
       
    }
}
