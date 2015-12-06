$(function(){
	if(sessionStorage.getItem('rol')!='atencioncliente'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	var id = $.getUrlVar("id");

	$("#atenciones-pendientes-lista").empty();
	$("#multas-pendientes-lista").empty();


	cargarDetalles();


	function cargarDetalles(){
		//Traerse el afiliado con este id de base de datos
		
		var message = { 
				accion : 'verDatosAfiliado',
				mensaje : '',
				valores : {
							"idAfiliado" : id
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);

		        	llenarInforme(data.datos)
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

	}

	function llenarInforme(afiliado){

		$("#afiliado-nombre").html(afiliado.nombre);
		$("#afiliado-nombre-usuario").html(afiliado.id);
		$("#afiliado-cedula").html(afiliado.cedula);
		$("#afiliado-categoria").html(afiliado.categoria);
		$("#afiliado-fecha-afiliacion").html(afiliado.fecha_afiliacion)

		cargarAtencionesPagar();

		cargarMultasPagar();
	}


	function cargarMultasPagar(){
		$("#multas-pendientes-lista").empty();

		//traer de bd

		var message = { 
				accion : 'multasPendientes',
				mensaje : '',
				valores : {
							"idAfiliado" : id
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);

		        	llenarMultasPendientes(data.datos);
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

	}
		
	function llenarMultasPendientes(multasPagar){

		$.each(multasPagar, function(i, multa){

			var panel = $("<div></div>").addClass("panel panel-default panel-pendiente");

			var pbody = $("<div></div>").addClass("panel-body").append([
					$("<h4><strong>Valor: </strong>"+multa.valor+"</h4>")

				])

			var pfooter = $("<div></div>").addClass("panel-footer").append([
					$("<button>Registrar pago!</button>").addClass("btn btn-primary pull-right").click(function(){
						registrarPagoMulta(multa)
					}),
					$("<div></div>").addClass('clearfix')
				])

			panel.append([pbody, pfooter]);

			$("#multas-pendientes-lista").append(panel);

		})
	}

	function registrarPagoMulta(multa){
		var message = { 
				accion : 'pagarMulta',
				mensaje : '',
				valores : {
							"idMulta" : multa.id
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Citas/accionCitas',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	if(data.exito == 1){
		        		toastr.success("Pago registrado con éxito")
		        		cargarMultasPagar();
		        	}
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });
	}

	function cargarAtencionesPagar(){
		$("#atenciones-pendientes-lista").empty();

		//traer de bd

		var message = { 
				accion : 'citasNoPagas',
				mensaje : '',
				valores : {
							"idUsuario" : id
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Citas/accionCitas',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);

		        	llenarAtencionesPendientes(data.datos);
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

	}

	function llenarAtencionesPendientes(atencionesPagar){

		$.each(atencionesPagar, function(i, atencion){

			var panel = $("<div></div>").addClass("panel panel-default panel-pendiente");

			var pbody = $("<div></div>").addClass("panel-body").append([
					$("<h5><strong>Fecha: </strong>"+atencion.fecha+"</h5>"),
					$("<h5><strong>Tipo: </strong>"+atencion.tipo_atencion+"</h5>"),
					$("<h5><strong>Doctor: </strong>"+atencion.profesional+"</h5>"),
					$("<h4><strong>Valor: </strong>"+atencion.valor+"</h4>")

				])

			var pfooter = $("<div></div>").addClass("panel-footer").append([
					$("<button>Registrar pago!</button>").addClass("btn btn-success pull-right").click(function(){
						registrarPagoAtencion(atencion)
					}),
					$("<div></div>").addClass('clearfix')
				])

			panel.append([pbody, pfooter]);

			$("#atenciones-pendientes-lista").append(panel);

		})


	}

	function registrarPagoAtencion(atencion){
		//pagarCita

		var message = { 
				accion : 'pagarCita',
				mensaje : '',
				valores : {
							"idCita" : atencion.id
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Citas/accionCitas',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	if(data.exito == 1){
		        		toastr.success("Pago registrado con éxito")
		        		cargarAtencionesPagar();
		        	}
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });
	}

})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}