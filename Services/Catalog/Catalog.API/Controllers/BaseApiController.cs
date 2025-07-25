using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    ////[Route("api/v{apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
    }
}
