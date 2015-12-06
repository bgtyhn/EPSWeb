$(function(){
	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	var accion = ""

	cargarTablaAtencion();

	$("#atencion-agregar").click(function(event) {
		accion = "agregar"
		$("#atencion-id").prop("readonly", false);
		$("#titulo-modal-atencion").html("Registrar nuevo usuario");
		$("#agregar-atencion-form").trigger("reset");
		$("#modal-agregar-atencion").modal("show");
	});

	$("#registro-cancelar").click(function(event){
		$("#modal-agregar-atencion").modal("hide");
	})

	$("#agregar-atencion-form").on("submit", function(event){
		event.preventDefault();
		console.log(accion);

		if(accion == "agregar"){

			var message = { 
				accion : 'agregarAtencionCliente',
				mensaje : '',
				valores : {
							"idAC" : 			$("#atencion-id").val(),
							"passwordAC" : 		$("#atencion-password").val(),
							"nombreAC" : 		$("#atencion-nombre").val(),
							"apellidosAC" : 	$("#atencion-apellidos").val(),
							"cedulaAC" : 		$("#atencion-cedula").val(),
							"correoAC" : 		$("#atencion-correo").val(),
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/AtencionCliente/accionAtencionCliente',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	cargarTablaAtencion();
		        	$("#0agregar-atencion-form").trigger("reset");
		        	$("#modal-agregar-atencion").modal("hide");
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

		} else if (accion == "editar"){

			var message = { 
				accion : 'editarAtencionCliente',
				mensaje : '',
				valores : {
							"idAC" : 			$("#atencion-id").val(),
							"passwordAC" : 		$("#atencion-password").val(),
							"nombreAC" : 		$("#atencion-nombre").val(),
							"apellidosAC" : 	$("#atencion-apellidos").val(),
							"cedulaAC" : 		$("#atencion-cedula").val(),
							"correoAC" : 		$("#atencion-correo").val(),
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/AtencionCliente/accionAtencionCliente',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	cargarTablaAtencion();
		        	$("#0agregar-atencion-form").trigger("reset");
		        	$("#modal-agregar-atencion").modal("hide");
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

		}

		cargarTablaAtencion();
	})

	function llenarTabla(rows){

		$(".trAf").remove();

		$.each(rows, function(i, atencion){

			var tr = $("<tr></tr>").addClass("trAf");

			tr.append([

					$("<td>"+atencion.id+"</td>"),
					$("<td>"+atencion.password+"</td>"),
					$("<td>"+atencion.nombre+"</td>"),
					$("<td>"+atencion.apellidos+"</td>"),
					$("<td>"+atencion.cedula+"</td>"),
					$("<td>"+atencion.correo+"</td>"),
					$("<td></td>").append([
						$("<button></button>").addClass("btn btn-xs btn-warning").append(
								$("<span><span>").addClass("glyphicon glyphicon-edit")
							).click(function(){
									editarAtencion(atencion);
								}),
						$("<span>&nbsp;&nbsp;</span>"),
						$("<button></button>").addClass("btn btn-xs btn-danger").append(
								$("<span><span>").addClass("glyphicon glyphicon-trash")
							).click(function(event){
								eliminarAtencion(atencion)
							})
						]
					)
				])

			$("#lista-atencion").append(tr)

		})
	}


	function cargarTablaAtencion(){

		var message = { 
			accion : 'verAtencionCliente',
			mensaje : '',
			valores : {
					}
		}

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/AtencionCliente/accionAtencionCliente',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	llenarTabla(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

	}

	function eliminarAtencion(atencion){
		console.log("eliminar")
		console.log(atencion)

		var message = { 
			accion : 'eliminarAtencionCliente',
			mensaje : '',
			valores : {
				"idAC" : atencion.id
					}
		}

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/AtencionCliente/accionAtencionCliente',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	cargarTablaAtencion();
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

	}

	function editarAtencion(atencion){

		$("#titulo-modal-atencion").html("Editar usuario");
		accion = "editar";

		$("#atencion-id").prop("readonly", true);

		$("#agregar-atencion-form").trigger("reset");

		$("#atencion-id").val(atencion.id);
		$("#atencion-password").val(atencion.password);
		$("#atencion-nombre").val(atencion.nombre);
		$("#atencion-apellidos").val(atencion.apellidos);
		$("#atencion-cedula").val(atencion.cedula);
		$("#atencion-correo").val(atencion.correo);

		$("#modal-agregar-atencion").modal("show");
	}



})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}