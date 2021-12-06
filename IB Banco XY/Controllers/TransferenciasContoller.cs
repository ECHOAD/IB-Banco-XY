using Contratos.BL_Contracts;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocio.Exceptions;
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

            return View("TransferenciasCuenta");
        }

        [Authorize]
        [Route("/credit/transact/{id_credito}")]
        public IActionResult TransferenciasCredito(int id_credito)
        {
            ViewData["id_creditCard"] = id_credito;

            return View("TransferenciasCredito");
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


        [HttpPost]
        [Route("credit/transact/create")]
        public async Task<ActionResult<int>> CreateCreditTransaction([FromBody] TransferenciaCredito transeferencia)
        {
            try
            {
                await _transferenciaBL.RealizeCreditTransaction(transeferencia);
                return Ok(new { Status = 1, Message = "Transaccion realizada correctamente" });
            }
            catch (UnnexpectedValueException ex)
            {
                return Ok(new { Status = -1, Message = ex.Message });
            }
            catch (NoSufficientAmountException ex)
            {
                return Ok(new { Status = -1, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = -1, Message = ex.Message });
            }
        }
    }
}
