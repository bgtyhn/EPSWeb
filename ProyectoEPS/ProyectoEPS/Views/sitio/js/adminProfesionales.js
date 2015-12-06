$(function(){
	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	var accion = ""

	cargarTablaProfesionales();

	$("#profesional-agregar").click(function(event) {
		accion = "agregar"
		$("#profesional-id").prop("readonly", false);
		$("#titulo-modal-profesional").html("Registrar nuevo profesional");
		$("#agregar-profesional-form").trigger("reset");
		$("#modal-agregar-profesional").modal("show");
	});

	$("#registro-cancelar").click(function(event){
		$("#modal-agregar-profesional").modal("hide");
	})

	$("#agregar-profesional-form").on("submit", function(event){
		event.preventDefault();
		
		console.log(accion)

		if(accion == "agregar"){

			var message = { 
				accion : 'adicionarProfesional',
				mensaje : '',
				valores : {
							"idProfesional" : 			$("#profesional-id").val(),
							"passwordProfesional" : 	$("#profesional-password").val(),
							"nombreProfesional" : 		$("#profesional-nombre").val(),
							"apellidosProfesional" : 	$("#profesional-apellidos").val(),
							"cedulaProfesional" : 		$("#profesional-cedula").val(),
							"correoProfesional" : 		$("#profesional-correo").val(),
							"areaProfesional" : 		$("#profesional-area option:selected").val(),
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Profesional/accionProfesionales',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	cargarTablaProfesionales();
		        	$("#modal-agregar-profesional").modal("hide");
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

		} else if (accion == "editar"){

			var message = { 
				accion : 'editarInformacionProfesional',
				mensaje : '',
				valores : {
							"idProfesional" : 			$("#profesional-id").val(),
							"passwordProfesional" : 	$("#profesional-password").val(),
							"nombreProfesional" : 		$("#profesional-nombre").val(),
							"apellidosProfesional" : 	$("#profesional-apellidos").val(),
							"cedulaProfesional" : 		$("#profesional-cedula").val(),
							"correoProfesional" : 		$("#profesional-correo").val(),
							"areaProfesional" : 		$("#profesional-area option:selected").val(),
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Profesional/accionProfesionales',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	cargarTablaProfesionales();
		        	$("#modal-agregar-profesional").modal("hide");
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

		}


	})


	function cargarTablaProfesionales(){

		//Traer de bd

		var message = { 
			accion : 'listarProfesionales',
			mensaje : '',
			valores : {}
		}

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Profesional/accionProfesionales',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data)
                llenarTabla(data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });
	}


	function llenarTabla(profesionales){

		console.log("llenar tabla")

		$(".trProf").remove();

		$.each(profesionales, function(i, profesional){

			var tr = $("<tr></tr>").addClass("trProf");

			tr.append([

					$("<td>"+profesional.id+"</td>"),
					$("<td>"+profesional.password+"</td>"),
					$("<td>"+profesional.nombre+"</td>"),
					$("<td>"+profesional.apellidos+"</td>"),
					$("<td>"+profesional.cedula+"</td>"),
					$("<td>"+profesional.correo+"</td>"),
					$("<td>"+profesional.area+"</td>"),
					$("<td></td>").append([
						$("<button></button>").addClass("btn btn-xs btn-warning").append(
								$("<span><span>").addClass("glyphicon glyphicon-edit")
							).click(function(){
									editarProfesional(profesional);
								}),
						$("<span>&nbsp;&nbsp;</span>"),
						$("<button></button>").addClass("btn btn-xs btn-danger").append(
								$("<span><span>").addClass("glyphicon glyphicon-trash")
							).click(function(event){
								eliminarProfesional(profesional)
							})
						]
					)
				])

			$("#lista-profesionales").append(tr)

		})
	}

	function eliminarProfesional(profesional){
		console.log("eliminar")
		
		var message = { 
			accion : 'eliminarProfesional',
			mensaje : '',
			valores : {
				"idProfesional" : profesional.id
			}
		}

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Profesional/accionProfesionales',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data)
                cargarTablaProfesionales();
            },
            error: function (info) {
                alert("error");
            }
        });

		cargarTablaProfesionales();
	}

	function editarProfesional(profesional){

		$("#titulo-modal-profesional").html("Editar profesional");
		accion = "editar";

		idEditar = profesional.id;

		$("#profesional-id").prop("readonly", true);

		$("#agregar-profesional-form").trigger("reset");

		$("#profesional-id").val(profesional.id);
		$("#profesional-password").val(profesional.password);
		$("#profesional-nombre").val(profesional.nombre);
		$("#profesional-apellidos").val(profesional.apellidos);
		$("#profesional-cedula").val(profesional.cedula);
		$("#profesional-correo").val(profesional.correo);
		$("#profesional-area").val(profesional.area);

		$("#modal-agregar-profesional").modal("show");
	}



})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}