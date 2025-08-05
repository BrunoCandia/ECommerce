using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
    }
}
