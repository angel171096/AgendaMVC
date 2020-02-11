var localStorage = window.localStorage;
class Departamentos {
    constructor(departamento, leyenda, action) {
        this.departamento = departamento;
        this.leyenda = leyenda;
        this.action = action;
    }

    agregarDepartamento(id) {

        var idDepartamento = id;

        if (this.departamento == "") {
            document.getElementById("Departamento").focus();
        } else {
            if (this.leyenda == "") {
                document.getElementById("Leyenda").focus();
            } else {
                var departamento = this.departamento;
                var leyenda = this.leyenda;
                var action = this.action;
                var mensaje = "";

                $.ajax({
                    type: "POST", // forma en la cual vamos a enviar los datos
                    url: action,  // url donda vamos a enviar nuestra informacion por POST, contiene el controlador 
                    data: {
                        idDepartamento, departamento, leyenda
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

    filtrarDatos(numPagina, order) {
        var valor = this.departamento;
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

    getDepartamento(id) {
        var action = this.action;

        $.ajax({
            type: "POST",
            url: action, // la direccion del metodo del controlador que se le va a enviar el id
            data: { id }, // lo que se le envia al metodo en el controlador
            success: (response) => { // obtiene como parametro lo que le envia el controlador, un registro de categoria
                console.log(response);
                document.getElementById("DepartamentoEdit").value = response[0].nombreDep;
                document.getElementById("LeyendaEdit").value = response[0].leyenda;

                localStorage.setItem("Departamento", JSON.stringify(response)); // lo almacena el localStorage
            }
        });
    }

    editarDepartamento(id) {

        var depart = this.departamento;
        var leyenda = this.leyenda;
        var action = this.action;
     
        $.ajax({
            type: "POST",
            url: action,
            data: { id, depart, leyenda },
            success: (response) => { // espero la respuesta del errorList del controlador el code y description
               
                if (response[0].code == "Update") {
                    alert('Se actualizo correctamente');
                }
                this.restablecer();
            }
        });
    }

    restablecer() {
        // Limpiar los campos de texto
        document.getElementById("Departamento").value = "";
        document.getElementById("Leyenda").value = "";
        document.getElementById("mensaje").innerHTML = "";
        $('#modaldept').modal('hide'); // ocultar nuestro modal modalAC
        $('#ModalEditar').modal('hide');
    }
}