$(function(){
	
	$("#citas-filtro-fecha").datepicker({
		format : "dd/mm/yyyy",
		startDate : "01/01/2013",
		todayBtn : "linked"
	});

	$("#citas-filtro-fecha").on("changeDate", function(){
		$("#citas-prof-lista").animate({ scrollTop: 0 }, "fast");
		$("#citas-fecha").val(
			$("#citas-filtro-fecha").datepicker("getFormattedDate")
		)
		var fecha = $("#citas-fecha").val()
		cargarCitasRecientes(fecha);
	})

	cargarCitasRecientes(new Date());

	function cargarCitasRecientes(fecha){

		$("#citas-prof-lista").empty();

		//stub

		var citas = []

		for(i = 0; i < 10; i++){
			citas.push({
				id : i,
				afiliado : "Afiliado "+i+" - "+fecha,
				fecha : fecha + " - "+i,
				tipo : "tipo "+i,
				estado : "estado "+i
			})
		}


		$.each(citas, function(i, cita){

			var panel = $("<div></div>").addClass("panel panel-default cita-panel");

			var pheading = $("<div></div>").addClass("panel-heading").append(
					$("<div></div>").addClass("panel-title").append(
							$("<h4>"+cita.afiliado+"</h4>")
						)
				)

			var pbody = $("<div></div>").addClass("panel-body cita-pbody").append([
					$("<span>"+cita.fecha+"</span>"),
					$("<br>"),
					$("<span>"+cita.tipo+"</span>"),
					$("<br>"),
					$("<span><strong>Estado: </strong>"+cita.estado+"</span>")
				])

			var pfooter = $("<div></div>").addClass("panel-footer").append([
					$("<button>Ver más</button>").addClass("btn btn-primary pull-right").click(function(event){
						irDetalleCita(cita.id)
					}),
					$("<div></div>").addClass('clearfix')
				])

			panel.append([pheading, pbody, pfooter]);

			$("#citas-prof-lista").append(panel);

		});

		/*
		<div class="panel panel-default cita-panel">
            <div class="panel-heading">
              <div class="panel-title">
                <h4>Nombre afiliado</h4>
              </div>
            </div>
            <div class="panel-body cita-pbody">
              <span>Fecha:<span><br>
              <span>Tipo:<span><br>
              <span><strong>Estado: </strong></span>
            </div>
            <div class="panel-footer">
              <a href="" class="btn btn-primary pull-right">
                Ver más
              </a>
              <div class="clearfix"></div>
            </div>
          </div>
		*/

	}

	function irDetalleCita(id){
		console.log("detalle: cita "+id)
	}

})