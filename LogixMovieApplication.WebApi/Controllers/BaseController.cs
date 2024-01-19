using LogixMovieApplication.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LogixMovieApplication.WebApi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }
        protected Guid LoggedInUserId
        {
            get
            {
                try
                {
                    if (HttpContext.User != null)
                    {
                        if (HttpContext.User.Identity?.IsAuthenticated ?? false)
                        {
                            string strUserId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value.ToString() ?? Guid.Empty.ToString();
                            if (Guid.TryParse(strUserId, out Guid userId))
                            {
                                return userId;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Guid.Empty;
                }
                return Guid.Empty;
            }
        }

    }
}
