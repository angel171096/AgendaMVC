using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ContactosController : Controller
    {
        private readonly WebApplication1Context _context;
        private ContactoDAL contactoDAL;

        public ContactosController(WebApplication1Context context)
        {
            _context = context;
            contactoDAL = new ContactoDAL(_context);
        }

        public List<Departamento> getDepartamentos()
        {
            return contactoDAL.getDepartamentos();
        }


        public List<Contacto> GetContactos(int id)
        {
            return contactoDAL.GetContactos(id);
        }

        public List<Departamento> getDepartamento(int id)
        {
            return contactoDAL.getDepartamento(id);
        }

        public async Task<List<IdentityError>> guardarContacto(string nombres, string apellidos, DateTime fechaNac,
           string direccion, string celular, string email, int idDepartamento)
        {
            return await contactoDAL.guardarContacto(nombres,apellidos, fechaNac, direccion,celular,email, idDepartamento);
        }

        public List<object[]> filtrarDatos(string valor, int numPagina, string order)
        {
            return contactoDAL.filtrarDatos(valor, numPagina, order);
        }

        public async Task<List<IdentityError>> editarContacto(int id, string nombres, string apellidos, DateTime fechaN, string direccion,
            string celular, string email, int idDepartamento)
        {
            return await contactoDAL.editarContacto(id, nombres, apellidos, fechaN, direccion,celular,email,idDepartamento);
        }




















        // GET: Contactoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacto.ToListAsync());
        }

        // GET: Contactoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contacto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // GET: Contactoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombres,Apellidos,FechaNacimiento,Direccion,Celular,Email")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacto);
        }

        // GET: Contactoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        // POST: Contactoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,FechaNacimiento,Direccion,Celular,Email")] Contacto contacto)
        {
            if (id != contacto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.Id))
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
            return View(contacto);
        }

        // GET: Contactoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contacto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // POST: Contactoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contacto = await _context.Contacto.FindAsync(id);
            _context.Contacto.Remove(contacto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactoExists(int id)
        {
            return _context.Contacto.Any(e => e.Id == id);
        }
    }
}
