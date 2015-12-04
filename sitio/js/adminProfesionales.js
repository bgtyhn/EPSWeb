$(function(){
	
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
		console.log(accion);
		cargarTablaProfesionales();
	})


	function cargarTablaProfesionales(){

		$("#lista-profesionales").not(":first").remove();

		//Traer de bd

		//Stub
		var rows = []

		for(i = 0; i < 50; i++){

			rows.push({
				id : i,
				password : "pass "+i,
				nombre : "nombre "+i,
				apellidos : "apellidos "+i,
				cedula : i*10,
				correo : "mail@"+i+".com",
				area : "area "+i
			})
		}

		$.each(rows, function(i, profesional){

			var tr = $("<tr></tr>");

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
		console.log(profesional)
		cargarTablaProfesionales();
	}

	function editarProfesional(profesional){

		$("#titulo-modal-profesional").html("Editar profesional");
		accion = "editar";

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