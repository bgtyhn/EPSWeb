$(function(){
	
	$("#cita-pendiente-area-sel").val("general");

	var area = $("#cita-pendiente-area-sel option:selected").val();

	cargarCitasPendientes(area);

	$("#cita-pendiente-area-sel").change(function(event) {
		areaSel = $("#cita-pendiente-area-sel option:selected").val();
		if(areaSel != ""){
			cargarCitasPendientes(areaSel);
		}
	});

	function cargarCitasPendientes(area){
		$("#citas-lista").empty();

		//stub

		var citas = []

		for(i = 0; i < 10; i++){
			citas.push({
				fecha : "Fecha "+i+" - "+area,
				tipo : "Tipo "+i+" - "+area,
				profesional : "Profesional "+i+" - "+area,
			})
		}

		//poner citas en paneles

		$.each(citas, function(i, c){

			var panel = $("<div></div>").addClass("panel panel-default cita-afiliado-panel");

			var panelBody = $("<div></div>").addClass("panel-body");

			var row = $("<div></div>").addClass("row");

			row.append([
				$("<h4>"+c.fecha+"</h4>").addClass("h4 col-xs-4"),
				$("<div></div>").addClass("col-xs-5 col-xs-offset-3").append(
					$("<h4>"+c.tipo+"</h4>").addClass("h4 pull-right")	
				)
			]);

			var doc = $("<h4>"+c.profesional+"</h4>");

			panel.append(panelBody.append([row, $("<hr>")]).append(doc));

			$("#citas-lista").append(panel);

		})

	}

})