using Contratos.BL_Contracts;
using Contratos.Helpers;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IB_Banco_XY.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly INumberGenerator<Prestamo> _prestamoNumberGenerator;
        private readonly IPrestamoBL _prestamoBL;

        public PrestamoController(
            INumberGenerator<Prestamo> prestamoNumberGenerator,
            IPrestamoBL prestamoBL)
        {
            _prestamoNumberGenerator = prestamoNumberGenerator;
            _prestamoBL = prestamoBL;

        }

        [Route("/prestamo/create")]
        public async Task<IActionResult> Create()
        {
            Prestamo prestamo = new();

            prestamo.Codigo_Prestamo = _prestamoNumberGenerator.Generate_a_Code();

            return await Task.Run(() => View(prestamo));
        }

        [HttpPost]
        [Route("/prestamo/create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Prestamo prestamo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _prestamoBL.CreatePrestamo(prestamo);

                    return Ok(new { status = 1, message = $"Prestamo de  No. [{prestamo.Codigo_Prestamo}] creado exitosamente" });
                }
                return BadRequest(new { status = -1, message = $"Prestamo de  No. [{prestamo.Codigo_Prestamo}] no puedo ser creada exitosamente" });
            }
            catch (Exception)
            {

                return BadRequest(new { status = -1, message = $"Tarjeta de credito de  No [{prestamo.Codigo_Prestamo}] no puedo ser creada exitosamente" });


            }
        }


        [Route("/payprestamo/{id}")]
        public async Task<IActionResult> PayPrestamo(int id)
        {
            ViewData["id_prestamo"] = id;

            return await Task.Run(() => View());
        }



        [HttpPost]
        [Route("/prestamo/paycredit")]
        public async Task<IActionResult> PayCreditCardOwms([FromBody] EstadoPrestamo pagoPrestamo)
        {

            try
            {
                if (ModelState.IsValid)
                {


                    await _prestamoBL.PayCreditCard(pagoCredito);

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
