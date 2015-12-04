$(function(){
	
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
		cargarTablaAtencion();
	})


	function cargarTablaAtencion(){

		$("#lista-atencion").not(":first").remove();

		//Traer de bd

		//Stub
		var rows = []

		for(i = 0; i < 50; i++){

			rows.push({
				id : i,
				password : "pass "+i,
				nombre : "nombre "+i,
				apellidos : "apellidos "+i,
				cedula : i*10
			})
		}

		$.each(rows, function(i, atencion){

			var tr = $("<tr></tr>");

			tr.append([

					$("<td>"+atencion.id+"</td>"),
					$("<td>"+atencion.password+"</td>"),
					$("<td>"+atencion.nombre+"</td>"),
					$("<td>"+atencion.apellidos+"</td>"),
					$("<td>"+atencion.cedula+"</td>"),
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

	function eliminarAtencion(atencion){
		console.log("eliminar")
		console.log(atencion)
		cargarTablaAtencion();
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

		$("#modal-agregar-atencion").modal("show");
	}



})