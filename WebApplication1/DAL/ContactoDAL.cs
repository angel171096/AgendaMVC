using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class ContactoDAL
    {
        private WebApplication1Context _context;

        public ContactoDAL(WebApplication1Context context)
        {
            this._context = context;
        }

        internal List<Departamento> getDepartamentos()
        {
            return _context.Departamento.Where(c => c.NombreDep == "Masaya").ToList();
        }

        public List<Contacto> GetContactos(int id)
        {
            return _context.Contacto.Where(c => c.Id == id).ToList();
        }

        public List<Departamento> getDepartamento(int id)
        {
            return _context.Departamento.Where(c => c.Id == id).ToList();
        }

        public async Task<List<IdentityError>> guardarContacto(string nombres, string apellidos, DateTime fechaN,
            string direccion, string celular, string email, int departamento)
        {
            List<IdentityError> errorList = new List<IdentityError>();
            Contacto contacto = new Contacto()
            {
                Nombres = nombres,
                Apellidos = apellidos,
                FechaNacimiento = Convert.ToDateTime(fechaN),
                Direccion = direccion,
                Celular = celular,
                Email = email,
                DepartamentoId = departamento
            };

            _context.Add(contacto);
            await _context.SaveChangesAsync();

            errorList.Add(new IdentityError()
            {
                Code = "Save",
                Description = "Save"
            });

            return errorList;
        }

        public List<object[]> filtrarDatos(string valor, int numPagina, string order)
        {
            int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 2;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();

            IEnumerable<Contacto> query;
            List<Contacto> contactos = null;

            switch (order)
            {
                case "Nombres":
                    contactos = _context.Contacto.OrderBy(c => c.Nombres).ToList();
                    break;
                case "Apellidos":
                    contactos = _context.Contacto.OrderBy(c => c.Apellidos).ToList();
                    break;
                case "FechaNacimiento":
                    contactos = _context.Contacto.OrderBy(c => c.FechaNacimiento).ToList();
                    break;
                case "Direccion":
                    contactos = _context.Contacto.OrderBy(c => c.Direccion).ToList();
                    break;
                case "Celular":
                    contactos = _context.Contacto.OrderBy(c => c.Celular).ToList();
                    break;
                case "Email":
                    contactos = _context.Contacto.OrderBy(c => c.Email).ToList();
                    break;
                case "DepartamentoId":
                    contactos = _context.Contacto.OrderBy(c => c.Departamento).ToList();
                    break;

            }

            numRegistros = contactos.Count;

            if ((numRegistros % reg_por_pagina) > 0)
            {
                numRegistros += 1;
            }
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);

            if (valor == "null")
            {
                query = contactos.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = contactos.Where(c => c.Nombres.StartsWith(valor) || c.Apellidos.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                dataFilter += "<tr>" +
                      "<td>" + item.Nombres + "</td>" +
                      "<td>" + item.Apellidos + "</td>" +
                       "<td>" + item.FechaNacimiento + "</td>" +
                      "<td>" + item.Direccion + "</td>" +
                       "<td>" + item.Celular + "</td>" +
                      "<td>" + item.Email + "</td>" +
                        "<td>" + item.DepartamentoId + "</td>" +
                      "<td>" +
                      "<a data-toggle='modal' data-target='#ModalEditontacto'  onclick='getContacto(" + item.Id + ")'class='btn btn-success'>Edit</a>" +
                      "</td>" +
                  "</tr>";
            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos2(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarDatos(" + pagina + ',' + '"' + order + '"' + ")'> < </a>";
                }
                if (1 < can_paginas)
                {
                    paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos2(" + pagina + ',' + '"' + order + '"' + ")'>  > </a>" +
                                 "<a class='btn btn-default' onclick='filtrarDatos2(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }

            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }

        public async Task<List<IdentityError>> editarContacto(int id, string nombres, string apellidos, DateTime fechaNac, string direccion,
            string celular, string email, int idDepartamento)
        {
            List<IdentityError> errorList = new List<IdentityError>();

            string code = string.Empty, des = string.Empty;
            Contacto contacto = new Contacto()
            {
                Id = id,
                Nombres = nombres,
                Apellidos = apellidos,
                FechaNacimiento = fechaNac,
                Direccion = direccion,
                Celular = celular,
                Email = email,
                DepartamentoId = idDepartamento
            };

            _context.Update(contacto);
            await _context.SaveChangesAsync();
            code = "Update";
            des = "Update Success";

            errorList.Add(new IdentityError()
            {
                Code = code,
                Description = des
            });

            return errorList;
        }


    }
}
