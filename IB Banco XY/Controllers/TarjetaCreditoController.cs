using Contratos.BL_Contracts;
using Contratos.Helpers;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using Negocio.Exceptions;
using System;
using System.Threading.Tasks;

namespace IB_Banco_XY.Controllers
{
    public class TarjetaCreditoController : Controller
    {
        private readonly INumberGenerator<TarjetaCredito> _creditCardNumberGenerator;
        private readonly ITarjetaCreditoBL _tarjetaCreditoBL;

        public TarjetaCreditoController(ITarjetaCreditoBL tarjetaCreditoBL
            , INumberGenerator<TarjetaCredito> creditCardNumberGenerator)
        {
            _creditCardNumberGenerator = creditCardNumberGenerator;
            _tarjetaCreditoBL = tarjetaCreditoBL;
        }
        [Route("/tarjetaCredito/create")]
        public async Task<IActionResult> Create()
        {
            TarjetaCredito tarjetaCredito = new();

            tarjetaCredito.Numero_tarjetaCredito = _creditCardNumberGenerator.Generate_a_Code();

            return await Task.Run(() => View(tarjetaCredito));
        }

        [Route("/paycreditcard/{id}")]
        public async Task<IActionResult> PayCreditCard(int id)
        {
            ViewData["id_creditCard"] = id;

            return await Task.Run(() => View());
        }

        [HttpPost]
        [Route("/tarjetaCredito/create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] TarjetaCredito tarjetaCredito)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _tarjetaCreditoBL.CreateCreditCard(tarjetaCredito);

                    return Ok(new { status = 1, message = $"Tarjeta de credito de  No [{tarjetaCredito.Numero_tarjetaCredito}] creada exitosamente" });
                }
                return BadRequest(new { status = -1, message = $"Tarjeta de credito de  No [{tarjetaCredito.Numero_tarjetaCredito}] no puedo ser creada exitosamente" });
            }
            catch (Exception)
            {
                return BadRequest(new { status = -1, message = $"Tarjeta de credito de  No [{tarjetaCredito.Numero_tarjetaCredito}] no puedo ser creada exitosamente" });
            }
        }

        [HttpPost]
        [Route("/tarjetaCredito/paycredit")]
        public async Task<IActionResult> PayCreditCardOwms([FromBody] EstadoCredito pagoCredito)
        {

            try
            {
                if (ModelState.IsValid)
                {


                    await _tarjetaCreditoBL.PayCreditCard(pagoCredito);

                    return Ok(new { Status = 1, Message = "Pago realizado correctamente" });
                }
                return BadRequest(new { Status = -1, Message = $"El pago no pudo ser realizado correctamente" });


            }
            catch (NoSufficientAmountException ex)
            {
                return Ok(new { Status = -1, ex.Message });
            }
            catch (UnnexpectedValueException ex)
            {
                return Ok(new { Status = -1, ex.Message });
            }
            catch (Exception ex)
            {

                return BadRequest(new { status = -1, ex.Message });

            }


        }

    }
}
