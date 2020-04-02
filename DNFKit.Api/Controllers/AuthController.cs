using System;
using System.Net;
using System.Threading;
using System.Web.Http;

using DNFKit.Core.Dtos;
using DNFKit.Core.Services;
using DNKit.Api.Security;

namespace DNKit.Api.Controllers
{
    [RoutePrefix("api/identity")]
    public class AuthController : ApiController
    {
        private readonly IUserService _service;
        protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(AuthController));

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                LoginResponse response = _service.Authenticate(login, TokenGenerator.GenerateTokenJwt);
                if (null != response && response.Id > 0)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Authenticate", ex);
            }

            // Unauthorized access 
            return Unauthorized();
        }
    }
}
