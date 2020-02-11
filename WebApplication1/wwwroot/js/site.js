// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#modalAC').on('shown.bs.modal', function () {
    $('#Nombres').focus();
});



//---------------------------------------------

var idContacto;

//$().ready(() => {
//    document.getElementById("filtrar").focus();
//    filtrarDatos(1);

//    //document.getElementById("filtrar2").focus();
//    //filtrarDatos2(1);

//    getDepartamentos();
  
//});

$().ready(() => {
    document.getElementById("filtrar").focus();
    filtrarDatos(1, "nombre");

    document.getElementById("filtrar2").focus();
    filtrarDatos2(1, "nombres");

});

$('#modaldept').on('shown.bs.modal', function () {
    $('#Departamento').focus();
});
//---------------------------------DEPARTAMENTOS---------------------------------------------

var idDepartamento;
var funcion = 0;

var agregarDepartamento = () => {
    var departamento = document.getElementById("Departamento").value;
    var leyenda = document.getElementById("Leyenda").value;
   
    var action = 'Departamentos/guardarDepartamento';
    var departamento = new Departamentos(departamento, leyenda, action);
    departamento.agregarDepartamento();
}

var getDepartamento = (id) => {
    idDepartamento = id;

    var action = 'Departamentos/getDepartamento';
    var departamento = new Departamentos("", "", action);
    departamento.getDepartamento(idDepartamento);
}

var editarDepartamento = () => {
    var departamento = document.getElementById("DepartamentoEdit").value;
    var leyenda = document.getElementById("LeyendaEdit").value;

    var action = 'Departamentos/editarDepartamento';
    var departamento = new Departamentos(departamento, leyenda, action);  // pasamos el action por el metodo constructor 
    departamento.editarDepartamento(idDepartamento);
}


var filtrarDatos = (numPagina, order) => {  // 1, departamento
    var valor = document.getElementById("filtrar").value;
    var action = 'Departamentos/filtrarDatos';
    var departamento = new Departamentos(valor,"", action);
    departamento.filtrarDatos(numPagina, order);
}


//------------------CONTACTOS-------

//var getDepartamentos = () => {
//    var action = 'Contactos/getDepartamentos';
//    var contactos = new Contactos("", "", "", "", "", "", "", action);
//    contactos.getDepartamentos();
//}
var idContacto;

var agregarContacto = () => {
    var nombres = document.getElementById("Nombres").value;
    var apellidos = document.getElementById("Apellidos").value;
    var fechaN = document.getElementById("FechaN").value;
    var direccion = document.getElementById("Direccion").value;
    var celular = document.getElementById("Celular").value;
    var email = document.getElementById("Email").value;
    var departamento = document.getElementById("idDepartamento").value;

    var action = 'Contactos/guardarContacto'; // controlador / nombre del metodo dentro del controlador
    var contacto = new Contactos(nombres, apellidos, fechaN, direccion, celular, email, departamento, action);
    contacto.agregarContacto();
}

var getContacto = (id) => {
    idContacto = id;

    var action = 'Contactos/GetContactos';
    var contacto = new Contactos("", "","","","","","", action);
    contacto.getContacto(idContacto);
}


var editarContacto = () => {
    var nombres = document.getElementById("NombresEdit").value;
    var apellidos = document.getElementById("ApellidosEdit").value;
    var fechaN = document.getElementById("FechaNEdit").value;
    var direccion = document.getElementById("DireccionEdit").value;
    var celular = document.getElementById("CelularEdit").value;
    var email = document.getElementById("EmailEdit").value;
    var idDepartamento = document.getElementById("idDepartamentoEdit").value;

    var action = 'Contactos/editarContacto';
    var contacto = new Contactos(nombres, apellidos, fechaN, direccion, celular, email, idDepartamento, action);  // pasamos el action por el metodo constructor 
    contacto.editarContacto(idContacto);
}


var filtrarDatos2 = (numPagina, order) => {  // 1, nombreContacto
    var valor = document.getElementById("filtrar2").value; // campo de texto
    var action = 'Contactos/filtrarDatos';
    var contacto = new Contactos(valor, "","","","","","", action);
    contacto.filtrarDatos2(numPagina, order);
}

