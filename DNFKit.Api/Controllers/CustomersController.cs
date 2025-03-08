using System;
using System.Web.Http;

using DNFKit.Core.Dtos;
using DNFKit.Core.Services;

namespace DNKit.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _service;
        protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(AuthController));

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetCustomerByRequest([FromUri] CustomerRequest request)
        {
            try
            {
                CustomerResponse response = _service.FetchCustomer(request.IdType, request.IdNumber);
                if (null != response && response.Id > 0)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetCustomerByRequest", ex);
            }

            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult SetCustomerByRequest([FromBody] CustomerRequest request)
        {
            try
            {
                CustomerResponse response = _service.UpdateCustomer(request.IdType, request.IdNumber);
                if (null != response && response.Id > 0)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SetCustomerByRequest", ex);
            }

            return NotFound();
        }
    }
}
