$(function(){
	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	cargarTablaAfiliados();

	$("#edicion-cancelar").click(function(event){
		$("#modal-editar-afiliado").modal("hide");
	})

	$("#editar-afiliado-form").on("submit", function(event){
		console.log("editar");
		event.preventDefault();
		var fecha = $("#afiliado-fecha-af").val().split("-");

		var message = { 
			accion : 'editarAfiliado',
			mensaje : '',
			valores : {
					"idAfiliado" : $("#afiliado-id").val(),
					"passwordAfiliado" : $("#afiliado-password").val(),
					"nombreAfiliado" : $("#afiliado-nombre").val(),
					"apellidosAfiliado" : $("#afiliado-apellidos").val(),
					"edadAfiliado" : $("#afiliado-edad").val(),
					"cedulaAfiliado" : $("#afiliado-cedula").val(),
					"fecha_afiliacionAfiliado" : fecha[2]+"-"+fecha[1]+"-"+fecha[0],
					"estadoAfiliado" : $("#afiliado-estado option:selected").val(),
					"categoriaAfiliado" : $("#afiliado-categoria option:selected").val(),
					"correoAfiliado" : $("#afiliado-correo").val()
					}
		}

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	cargarTablaAfiliados();
	        	$("#editar-afiliado-form").trigger("reset");
				$("#modal-editar-afiliado").modal('hide');
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });
		
	})


	function cargarTablaAfiliados(){

		$("#lista-afiliados").empty();

		var message = { 
			accion : 'listarAfiliados',
			mensaje : '',
			valores : {
					}
		}

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	llenarTabla(data.datos)
	        	$("#solicitud-form").trigger("reset");
				$("#modal-solicitud-registro").modal('hide');
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

		

	}

	function llenarTabla(rows){
		$.each(rows, function(i, afiliado){

			var tr = $("<tr></tr>");

			tr.append([

					$("<td>"+afiliado.id+"</td>"),
					$("<td>"+afiliado.password+"</td>"),
					$("<td>"+afiliado.nombre+"</td>"),
					$("<td>"+afiliado.apellidos+"</td>"),
					$("<td>"+afiliado.cedula+"</td>"),
					$("<td>"+afiliado.edad+"</td>"),
					$("<td>"+afiliado.fecha_afiliacion+"</td>"),
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

		var message = { 
			accion : 'eliminarAfiliado',
			mensaje : '',
			valores : {
				"idAfiliado" : afiliado.id
					}
		}

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	cargarTablaAfiliados();
	        	$("#solicitud-form").trigger("reset");
				$("#modal-solicitud-registro").modal('hide');
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

	}

	function editarAfiliado(afiliado){

		$("#editar-afiliado-form").trigger("reset");

		$("#afiliado-id").val(afiliado.id);
		$("#afiliado-password").val(afiliado.password);
		$("#afiliado-nombre").val(afiliado.nombre);
		$("#afiliado-apellidos").val(afiliado.apellidos);
		$("#afiliado-cedula").val(afiliado.cedula);
		$("#afiliado-edad").val(afiliado.edad);
		$("#afiliado-fecha-af").val(afiliado.fecha_afiliacion);
		$("#afiliado-correo").val(afiliado.correo);
		$("#afiliado-estado").val(afiliado.estado);
		$("#afiliado-categoria").val(afiliado.categoria);

		$("#modal-editar-afiliado").modal("show");
	}



})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}