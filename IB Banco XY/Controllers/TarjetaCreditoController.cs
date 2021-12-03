using Contratos.BL_Contracts;
using Contratos.Helpers;
using Entidades;
using Microsoft.AspNetCore.Mvc;
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

            tarjetaCredito.numero_tarjetaCredito = _creditCardNumberGenerator.Generate_a_Code();



            return await Task.Run(() => View(tarjetaCredito));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_Usuario,Codg_Cuenta,Balance_Actual")] TarjetaCredito tarjetaCredito)
        {
            if (ModelState.IsValid)
            {
                await _tarjetaCreditoBL.Save(tarjetaCredito);

                return Ok();
            }
            return View(tarjetaCredito);
        }
    }
}
