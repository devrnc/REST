//using Tapioca.HATEOAS;
using Microsoft.AspNetCore.Mvc;
using TestePratico.Business;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using TestePratico.Model;

namespace TestePratico.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PaymentsController : Controller
    {
        private IPaymentsBusiness paymentBusiness;

        public PaymentsController(IPaymentsBusiness paymentBusiness)
        {
            this.paymentBusiness = paymentBusiness;
        }

        // Configura o Swagger para a operação
        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<Payments>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Get()
        {
            return Ok(this.paymentBusiness.FindAll());
        }

        // Configura o Swagger para a operação
        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(Payments))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Get(long id)
        {
            Payments payment = this.paymentBusiness.FindById(id);
            if (payment == null)
                return NotFound();

            return new OkObjectResult(payment);
        }

        // Configura o Swagger para a operação
        [HttpGet("find-by-name")]
        [SwaggerResponse((200), Type = typeof(List<Payments>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult GetByName([FromQuery] string name)
        {
            return new OkObjectResult(this.paymentBusiness.FindByName(name));
        }

        // Configura o Swagger para a operação
        [HttpPost]
        [SwaggerResponse((201), Type = typeof(Payments))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Post([FromBody]Payments payment)
        {
            if (payment == null)
                return BadRequest();

            return new OkObjectResult(this.paymentBusiness.Create(payment));
        }

        // Configura o Swagger para a operação
        [HttpPut]
        [SwaggerResponse((202), Type = typeof(Payments))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Put([FromBody]Payments payment)
        {
            if (payment == null)
                return BadRequest();

            Payments updatedPayment = this.paymentBusiness.Update(payment);
            if (updatedPayment == null)
                return BadRequest();

            return new OkObjectResult(updatedPayment);
        }

        // Configura o Swagger para a operação
        [HttpDelete("{id}")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Delete(int id)
        {
            this.paymentBusiness.Delete(id);
            return NoContent();
        }
    }
}
