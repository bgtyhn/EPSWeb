$(function(){
	
	var id = $.getUrlVar("id");


	cargarDetalles();


	function cargarDetalles(){
		//Traerse el afiliado con este id de base de datos
		
		//stub
		var afiliado = {
			nombre : "German andres",
			nombreUsuario : "grmnsito",
			cedula : "1289034",
			categoria : "B",
			fechaAfiliacion : new Date("11/13/2011")
		}

		$("#afiliado-nombre").html(afiliado.nombre);
		$("#afiliado-nombre-usuario").html(afiliado.nombreUsuario);
		$("#afiliado-cedula").html(afiliado.cedula);
		$("#afiliado-categoria").html(afiliado.categoria);
		$("#afiliado-fecha-afiliacion").html(afiliado.fechaAfiliacion)

		cargarAtencionesPagar();

		cargarMultasPagar();

	}

	function cargarMultasPagar(){
		$("#multas-pendientes-lista").empty();

		//traer de bd

		//stub
		var multasPagar = []
		for(i = 0; i < 4; i++){	
			multasPagar.push({
				id : i,
				fecha: new Date(),
				valor: "valor "+id
			})
		}


		$.each(multasPagar, function(i, multa){

			var panel = $("<div></div>").addClass("panel panel-default panel-pendiente");

			var pbody = $("<div></div>").addClass("panel-body").append([
					$("<h5><strong>Fecha: </strong>"+multa.fecha+"</h5>"),
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
		console.log(multa.id);
	}

	function cargarAtencionesPagar(){
		$("#atenciones-pendientes-lista").empty();

		//traer de bd

		//stub
		var atencionesPagar = []
		for(i = 0; i < 6; i++){	
			atencionesPagar.push({
				id : i,
				fecha: new Date(),
				tipo: "tipo "+id,
				doctor: "doctor "+id,
				valor: "valor "+id
			})
		}


		$.each(atencionesPagar, function(i, atencion){

			var panel = $("<div></div>").addClass("panel panel-default panel-pendiente");

			var pbody = $("<div></div>").addClass("panel-body").append([
					$("<h5><strong>Fecha: </strong>"+atencion.fecha+"</h5>"),
					$("<h5><strong>Tipo: </strong>"+atencion.tipo+"</h5>"),
					$("<h5><strong>Doctor: </strong>"+atencion.doctor+"</h5>"),
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
		console.log(atencion.id);
	}

})