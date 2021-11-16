using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Capo_Datos;
using Entidades;

namespace IB_Banco_XY.Controllers
{
    public class CuentasAhorrosController : Controller
    {
        private readonly InternetBanking _context;

        public CuentasAhorrosController(InternetBanking context)
        {
            _context = context;
        }

        // GET: CuentasAhorros
        public async Task<IActionResult> Index()
        {
            var internetBanking = _context.CuentasAhorro.Include(c => c.Usuario);
            return View(await internetBanking.ToListAsync());
        }

        // GET: CuentasAhorros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasAhorro = await _context.CuentasAhorro
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuentasAhorro == null)
            {
                return NotFound();
            }

            return View(cuentasAhorro);
        }

        // GET: CuentasAhorros/Create
        public IActionResult Create()
        {
            ViewData["Id_Usuario"] = new SelectList(_context.Set<Usuarios>(), "Id", "Id");
            return View();
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
                _context.Add(cuentasAhorro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Usuario"] = new SelectList(_context.Set<Usuarios>(), "Id", "Id", cuentasAhorro.Id_Usuario);
            return View(cuentasAhorro);
        }

        // GET: CuentasAhorros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasAhorro = await _context.CuentasAhorro.FindAsync(id);
            if (cuentasAhorro == null)
            {
                return NotFound();
            }
            ViewData["Id_Usuario"] = new SelectList(_context.Set<Usuarios>(), "Id", "Id", cuentasAhorro.Id_Usuario);
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
                    _context.Update(cuentasAhorro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentasAhorroExists(cuentasAhorro.Id))
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
            ViewData["Id_Usuario"] = new SelectList(_context.Set<Usuarios>(), "Id", "Id", cuentasAhorro.Id_Usuario);
            return View(cuentasAhorro);
        }

        // GET: CuentasAhorros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasAhorro = await _context.CuentasAhorro
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuentasAhorro == null)
            {
                return NotFound();
            }

            return View(cuentasAhorro);
        }

        // POST: CuentasAhorros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuentasAhorro = await _context.CuentasAhorro.FindAsync(id);
            _context.CuentasAhorro.Remove(cuentasAhorro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentasAhorroExists(int id)
        {
            return _context.CuentasAhorro.Any(e => e.Id == id);
        }
    }
}
