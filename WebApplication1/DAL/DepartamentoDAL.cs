using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class DepartamentoDAL
    {
        private WebApplication1Context _context;

        public DepartamentoDAL(WebApplication1Context context)
        {
            this._context = context;
        }

        public async Task<List<IdentityError>> guardarDepartamento(string departamento, string leyenda)
        {
            List<IdentityError> errorList = new List<IdentityError>();
            Departamento departament = new Departamento()
            {
                NombreDep = departamento,
                Leyenda = leyenda
            };

            _context.Add(departament);
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

            IEnumerable<Departamento> query;
            List<Departamento> departamentos = null;

            switch (order)
            {
                case "Departamento":
                    departamentos = _context.Departamento.OrderBy(c => c.NombreDep).ToList();
                    break;
                case "Leyenda":
                    departamentos = _context.Departamento.OrderBy(c => c.Leyenda).ToList();
                    break;
            }

            numRegistros = departamentos.Count;

            if ((numRegistros % reg_por_pagina) > 0)
            {
                numRegistros += 1;
            }
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);

            if (valor == "null")
            {
                query = departamentos.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = departamentos.Where(c => c.NombreDep.StartsWith(valor) || c.Leyenda.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                dataFilter += "<tr>" +
                      "<td>" + item.NombreDep + "</td>" +
                      "<td>" + item.Leyenda + "</td>" +
                      "<td>" +
                      "<a data-toggle='modal' data-target='#ModalEditar'  onclick='getDepartamento(" + item.Id + ")'class='btn btn-success'>Edit</a>" +
                      "</td>" +
                  "</tr>";
            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarDatos(" + pagina + ',' + '"' + order + '"' + ")'> < </a>";
                }
                if (1 < can_paginas)
                {
                    paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos(" + pagina + ',' + '"' + order + '"' + ")'>  > </a>" +
                                 "<a class='btn btn-default' onclick='filtrarDatos(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }

            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }

        public List<Departamento> getDepartamento(int id)
        {
            return _context.Departamento.Where(c => c.Id == id).ToList();
        }

        public async Task<List<IdentityError>> editarDepartamento(int id, string depart, string leyenda)
        {
            List<IdentityError> errorList = new List<IdentityError>();

            string code = string.Empty, des = string.Empty;
            Departamento departamento = new Departamento()
            {
                Id = id,
                NombreDep = depart,
                Leyenda = leyenda
            };

            _context.Update(departamento);
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
