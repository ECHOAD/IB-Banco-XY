﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Capo_Datos;
using Entidades;
using Contratos.BL_Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Contratos.Helpers;

namespace IB_Banco_XY.Controllers
{
    //[Authorize]
    public class CuentasAhorrosController : Controller
    {
        private readonly ICuentaAhorroBL _cuentaAhorroBl;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly INumberGenerator<CuentasAhorro> _numberGenerator;



        public CuentasAhorrosController(ICuentaAhorroBL cuentaAhorroBL,
            UserManager<IdentityUser> userManager,
            INumberGenerator<CuentasAhorro> numberGenerator)
        {
            this._cuentaAhorroBl = cuentaAhorroBL;
            this.UserManager = userManager;
            this._numberGenerator = numberGenerator;
        }


        // GET: CuentasAhorros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasAhorro = await _cuentaAhorroBl.FindByID(id.Value);
            if (cuentasAhorro == null)
            {
                return NotFound();
            }

            return View(cuentasAhorro);
        }

        // GET: CuentasAhorros/Create
        public async Task<IActionResult> Create()
        {
            CuentasAhorro cuentaAhorro;



            cuentaAhorro = new CuentasAhorro { Id_Usuario = User.Claims.First().Value, Codg_Cuenta = _numberGenerator.Generate_a_Code() };

            return await Task.Run(() => View(cuentaAhorro));

        }

        // POST: CuentasAhorros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_Usuario,Codg_Cuenta,Balance_Actual")] CuentasAhorro cuentasAhorro)
        {
            if (ModelState.IsValid)
            {
                await _cuentaAhorroBl.Save(cuentasAhorro);
                return RedirectToAction(nameof(Index));
            }
            return View(cuentasAhorro);
        }

        // GET: CuentasAhorros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cuentasAhorro = await _cuentaAhorroBl.FindByID(id.Value);

            if (cuentasAhorro == null)
            {
                return NotFound();
            }

            return View(cuentasAhorro);
        }

        // POST: CuentasAhorros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_Usuario,Codg_Cuenta,Balance_Actual")] CuentasAhorro cuentasAhorro)
        {
            if (id != cuentasAhorro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cuentaAhorroBl.Update(cuentasAhorro);

                }
                catch (Exception)
                {
                    if (!await CuentasAhorroExistsAsync(cuentasAhorro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cuentasAhorro);
        }

        // GET: CuentasAhorros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ActualObj = await _cuentaAhorroBl.FindByID(id.Value);

            if (ActualObj == null)
            {
                return NotFound();
            }

            return View(ActualObj);
        }

        // POST: CuentasAhorros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuentasAhorro = await _cuentaAhorroBl.FindByID(id);
            await _cuentaAhorroBl.Delete(cuentasAhorro);


            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CuentasAhorroExistsAsync(int id)
        {
            return (await _cuentaAhorroBl.FindByID(id)) != null;
        }

        [HttpGet("GetAccountByNumber")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<CuentasAhorro>> GetAccountByNumber(string accountNumber)
        {
            return (await _cuentaAhorroBl.FindByCondition(x => x.Codg_Cuenta == accountNumber)).ToList().First();
        }

    }
}
