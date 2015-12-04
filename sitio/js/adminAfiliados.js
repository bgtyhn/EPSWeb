$(function(){
	
	cargarTablaAfiliados();

	$("#afiliado-editar").click(function(event) {
		accion = "agregar"
		$("#editar-afiliado-form").trigger("reset");
		$("#modal-editar-afiliado").modal("show");
	});

	$("#edicion-cancelar").click(function(event){
		$("#modal-editar-afiliado").modal("hide");
	})

	$("#editar-afiliado-form").on("submit", function(event){
		event.preventDefault();
		console.log("editar");
		cargarTablaAfiliados();
	})


	function cargarTablaAfiliados(){

		$("#lista-afiliados").empty();

		//Traer de bd

		//Stub
		var rows = []

		var categorias = ["A", "B", "C", "D", "E"]

		for(i = 0; i < 50; i++){

			rows.push({
				id : i,
				password : "pass "+i,
				nombre : "nombre "+i,
				apellidos : "apellidos "+i,
				cedula : i*10,
				edad : i*(20%i),
				fechaAfiliacion : "11/11/2011",
				correo : "mail@"+i+".com",
				estado: "activo",
				categoria : categorias[i%categorias.length]
			})
		}

		$.each(rows, function(i, afiliado){

			var tr = $("<tr></tr>");

			tr.append([

					$("<td>"+afiliado.id+"</td>"),
					$("<td>"+afiliado.password+"</td>"),
					$("<td>"+afiliado.nombre+"</td>"),
					$("<td>"+afiliado.apellidos+"</td>"),
					$("<td>"+afiliado.cedula+"</td>"),
					$("<td>"+afiliado.edad+"</td>"),
					$("<td>"+afiliado.fechaAfiliacion+"</td>"),
					$("<td>"+afiliado.correo+"</td>"),
					$("<td>"+afiliado.estado+"</td>"),
					$("<td>"+afiliado.categoria+"</td>"),
					$("<td></td>").append([
						$("<button></button>").addClass("btn btn-xs btn-warning").append(
								$("<span><span>").addClass("glyphicon glyphicon-edit")
							).click(function(){
									editarAfiliado(afiliado);
								}),
						$("<span>&nbsp;&nbsp;</span>"),
						$("<button></button>").addClass("btn btn-xs btn-danger").append(
								$("<span><span>").addClass("glyphicon glyphicon-trash")
							).click(function(event){
								eliminarAfiliado(afiliado)
							})
						]
					)
				])

			$("#lista-afiliados").append(tr)

		})

	}

	function eliminarAfiliado(afiliado){
		console.log("eliminar")
		console.log(afiliado)
		cargarTablaAfiliados();
	}

	function editarAfiliado(afiliado){

		$("#editar-afiliado-form").trigger("reset");

		$("#afiliado-id").val(afiliado.id);
		$("#afiliado-password").val(afiliado.password);
		$("#afiliado-nombre").val(afiliado.nombre);
		$("#afiliado-apellidos").val(afiliado.apellidos);
		$("#afiliado-cedula").val(afiliado.cedula);
		$("#afiliado-edad").val(afiliado.edad);
		$("#afiliado-fecha-af").val(afiliado.fechaAfiliacion);
		$("#afiliado-correo").val(afiliado.correo);
		$("#afiliado-estado").val(afiliado.estado);
		$("#afiliado-categoria").val(afiliado.categoria);

		$("#modal-editar-afiliado").modal("show");
	}



})