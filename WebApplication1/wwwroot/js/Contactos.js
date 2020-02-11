
var localStorage = window.localStorage;
class Contactos {
    // datos que vamos a recibir de nuestro modal
    constructor(nombres, apellidos, fechaN, direccion, celuar, email, departamento, action) {
        this.nombres = nombres;
        this.apellidos = apellidos;
        this.fechaN = fechaN;
        this.direccion = direccion;
        this.celuar = celuar;
        this.email = email;
        this.departamento = departamento;
        this.action = action;

    }

    agregarContacto() {
        if (this.nombres == "") {
            document.getElementById("Nombres").focus();
        } else {
            if (this.apellidos == "") {
                document.getElementById("Apellidos").focus();
            } else {
                if (this.direccion == "") {
                    document.getElementById("Direccion").focus();
                } else {
                    if (this.celuar == "") {
                        document.getElementById("Celular").focus();
                    } else {
                        if (this.email == "") {
                            document.getElementById("Email").focus();
                        } else {
                            var nombres = this.nombres;
                            var apellidos = this.apellidos;
                            var fechaN = this.fechaN;
                            var direccion = this.direccion;
                            var celular = this.celuar;
                            var email = this.email;
                            var action = this.action;
                            var departamento = this.departamento;
                            var mensaje = '';
                            $.ajax({
                                type: "POST", // forma en la cual vamos a enviar los datos
                                url: action,  // url donda vamos a enviar nuestra informacion por POST, contiene el controlador 
                                data: {
                                    nombres, apellidos, fechaN, direccion, celular, email, departamento
                                },
                                success: (response) => { // la informacion que nos devuelve el controlador

                                    $.each(response, (index, val) => {
                                        mensaje = val.code;
                                    });

                                    if (mensaje === "Save") {
                                        alert('Guardado con exito');
                                    } else {
                                        document.getElementById("mensaje").innerHTML = "No se puede guardar la categoria";
                                    }
                                }
                            });
                        }
                    }
                }
            }
        }
    }

    filtrarDatos2(numPagina, order) {
        var valor = this.nombres;
        var action = this.action;

        if (valor == "") {
            valor = "null";
        }
        $.ajax({
            type: "POST",
            url: action,
            data: { valor, numPagina, order }, // valor(depat), numoer, departamento
            success: (response) => {
                console.log(response);
                $.each(response, (index, val) => {

                    $("#resultSearch").html(val[0]);
                    $("#paginado").html(val[1]);
                });

            }
        });
    }

    getContacto(id) {
        var action = this.action;

        $.ajax({
            type: "POST",
            url: action, // la direccion del metodo del controlador que se le va a enviar el id
            data: { id }, // lo que se le envia al metodo en el controlador
            success: (response) => { // obtiene como parametro lo que le envia el controlador, un registro de categoria
                console.log(response);
                document.getElementById("NombresEdit").value = response[0].nombres;
                document.getElementById("ApellidosEdit").value = response[0].apellidos;
                document.getElementById("FechaNEdit").value = response[0].fechaNacimiento;
                document.getElementById("DireccionEdit").value = response[0].direccion;
                document.getElementById("CelularEdit").value = response[0].celular;
                document.getElementById("EmailEdit").value = response[0].email;
                document.getElementById("idDepartamentoEdit").value = response[0].departamentoId;

                localStorage.setItem("Departamento", JSON.stringify(response)); // lo almacena el localStorage
            }
        });
    }

    editarContacto(id) {

        var nombres = this.nombres;
        var apellidos = this.apellidos;
        var fechaNac = this.fechaN;
        var direccion = this.direccion;
        var celular = this.celuar;
        var email = this.email;
        var idDepartamento = this.departamento;
        var action = this.action;

        $.ajax({
            type: "POST",
            url: action,
            data: { id, nombres, apellidos, fechaNac, direccion, celular, email, idDepartamento },
            success: (response) => {
                if (response[0].code == "Save") {
                    this.restablecer();
                } else {
                    document.getElementById("titleCurso").innerHTML = response[0].description;
                }
            }
        });
    }

    restablecer() {
        document.getElementById("Nombres").value = "";
        document.getElementById("Apellidos").value = "";
        document.getElementById("FechaN").value = "";
        document.getElementById("Direccion").value = "";
        document.getElementById("Celular").value = "";
        document.getElementById("Email").checked = false;
        document.getElementById('ContactosDepartamento').selectedIndex = 0;
        document.getElementById("mensaje").innerHTML = "";
        filtrarCurso(1, "nombre");
        $('#modalContacto').modal('hide');
        $('#ModalEdit').modal('hide');
    }

}
