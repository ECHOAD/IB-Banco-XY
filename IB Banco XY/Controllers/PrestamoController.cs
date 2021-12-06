using Contratos.BL_Contracts;
using Contratos.Helpers;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using Negocio.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IB_Banco_XY.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly INumberGenerator<Prestamo> _prestamoNumberGenerator;
        private readonly IPrestamoBL _prestamoBL;
        private readonly IEstadoPrestamoBL _estadoPrestamoBL;

        public PrestamoController(
            INumberGenerator<Prestamo> prestamoNumberGenerator,
            IPrestamoBL prestamoBL, IEstadoPrestamoBL estadoPrestamoBL)
        {
            _prestamoNumberGenerator = prestamoNumberGenerator;
            _prestamoBL = prestamoBL;
            _estadoPrestamoBL = estadoPrestamoBL;
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
        [Route("/prestamo/payprestamo")]
        public async Task<IActionResult> PayPrestamosOwms([FromBody] EstadoPrestamo pagoPrestamo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _prestamoBL.PayPrestamo(pagoPrestamo);

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
                return Ok(new { status = -1, ex.Message });
            }

        }


        [HttpGet]
        [Route("/prestamo/estadoPrestamo/{id_prestamo}")]
        public async Task<IActionResult> EstadoDePrestamo(int id_prestamo)
        {
            var prestamo = await _prestamoBL.FindById(id_prestamo);

            return View("View_EstadoPrestamo", prestamo);
        }


        [HttpGet]
        [Route("prestamo/estadoprestamo/partial")]
        public async Task<IActionResult> EstadoDePrestamoDetail(int? id_prestamo, DateTime? desde, DateTime? hasta)
        {
            try
            {

                if (!id_prestamo.HasValue)
                    return NotFound();

                if (!desde.HasValue || !hasta.HasValue)
                {
                    desde = DateTime.Now.AddMonths(-1);
                    hasta = DateTime.Now;
                }

                var EstadosCredito = (await _estadoPrestamoBL.FindByCondition(x => x.Id_prestamo == id_prestamo.Value && x.Fecha > desde.Value
                && x.Fecha.Value < hasta)).AsEnumerable();

                return PartialView("EstadoPrestamo", EstadosCredito);


            }
            catch (Exception e)
            {
                return BadRequest(new { status = -1, Message = "Hubo un fallo con la peticion " + e.Message });

            };
        }


    }

}
