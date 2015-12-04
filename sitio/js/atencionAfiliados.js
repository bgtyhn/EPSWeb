$(function(){
	
	cargarAfiliados("");

	$("#atencion-busqueda-afiliado").on("input", function(){
		cargarAfiliados($("#atencion-busqueda-afiliado").val());
	})

	function cargarAfiliados(entrada){

		$("#afiliados-atencion-lista").empty();

		//Traer afiliados de base de datos

		//Stub

		var afiliados = []
		var x = "";

		if(entrada == ""){
			//traer de bd
			x = "todos";
		} else {
		 	x = entrada;

		}

		//Traer de bd
		for(i = 0; i < entrada.length; i++){
			afiliados.push({
				id : "id " + x,
				nombre : "Usuario " + x,
				apellido : "Apellido " + x,
				cedula : "1237 " + x,
				edad : "7642 " + x,
				categoria : "Categoria " + x
			})
		}

		$.each(afiliados, function(i, afiliado){

			var panel = $("<div></div>").addClass("panel panel-default cita-panel");

			var pheading = $("<div></div>").addClass("panel-heading").append(
					$("<div></div>").addClass("panel-title").append(
							$("<h4>"+afiliado.nombre+" "+afiliado.apellido+"</h4>")
						)
				)

			var pbody = $("<div></div>").addClass("panel-body cita-pbody").append([
					$("<span>Cédula: "+afiliado.cedula+"</span>"),
					$("<br>"),
					$("<span>Edad: "+afiliado.edad+"</span>"),
					$("<br>"),
					$("<span><strong>Categoría: </strong>"+afiliado.categoria+"</span>")
				])

			var pfooter = $("<div></div>").addClass("panel-footer").append([
					$("<button>Ver más</button>").addClass("btn btn-primary pull-right").click(function(event){
						irDetalleAfiliado(afiliado.id)
					}),
					$("<div></div>").addClass('clearfix')
				])

			panel.append([pheading, pbody, pfooter]);

			$("#afiliados-atencion-lista").append(panel);

		});

	}

	function irDetalleAfiliado(id){
		console.log("detalle Afiliado "+id);
	}

})