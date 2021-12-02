using Contratos.BL_Contracts;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IB_Banco_XY.Controllers
{
    public class TransferenciasContoller : Controller
    {

        private readonly ITransferenciaBL _transferenciaBL;

        public TransferenciasContoller(ITransferenciaBL transferenciaBL)
        {
            _transferenciaBL = transferenciaBL;
        }

        [Authorize]
        [Route("/transact/{account}")]
        public IActionResult Transferencias(string account)
        {
            ViewBag.Account = account;

            return View();
        }


        [HttpPost]
        [Route("/transact/create")]
        public async Task<ActionResult<int>> CreateTransaction([FromBody] TransferenciaCuenta transeferencia)
        {
            try
            {
                await _transferenciaBL.RealizeTransaction(transeferencia);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
