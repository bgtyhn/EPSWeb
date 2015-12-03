$(function(){

	$("#cita-calificar-area-sel").val("todas");

	var area = $("#cita-calificar-area-sel option:selected").val();

	cargarCitasCalificar(area);

	$("#cita-calificar-area-sel").change(function(event) {
		areaSel = $("#cita-calificar-area-sel option:selected").val();
		if(areaSel != ""){
			cargarHistorialCitas(areaSel);
		}
	});

	function calificarCita(cita, calificacion){

		$("#aviso-"+cita.id).remove();

		if(calificacion == '')
			$("#cita-calificar-sel-"+cita.id).after($("<span id='aviso-"+cita.id+"'><br>Debe seleccionar</span>").addClass("curso-aviso-inscripcion"));

		console.log("cita: "+cita.id+", calificacion "+calificacion);
	}

	function cargarCitasCalificar(area){

		$("#citas-lista").empty();

		//stub

		var citas = [];

		for(i = 0; i < 4; i++){
			citas.push({
				id : i,
				fecha : "fecha "+i+" - "+area,
				tipo : "tipo "+i+" - "+area,
				profesional : "profesional "+i+" - "+area,
				area : "area "+i+" - "+area,
				pago : "no",
				calificacion : "5",
				diagnosticos : [
									"diag0 "+i,
									"diag1 "+i,
									"diag2 "+i
								],
				medicamentos : [
									"med0 "+i,
									"med1 "+i,
									"med2 "+i
								]

			});
		}

		$.each(citas, function(i, cita){

			var panelBody = $("<div></div>").addClass("panel-body");

			var row = $("<div></div>").addClass("row").append([
					$("<h4>"+cita.fecha+"</h4>").addClass("col-xs-4 h4"),
					$("<div></div>").addClass("col-xs-5 col-xs-offset-3").append(
						$("<h4>"+cita.tipo+"</h4>").addClass("pull-right h4")
					)
				]);

			var formCalificacion = $("<div></div>").addClass("form-inline pull-right");

			var fgCalificacion = $("<div></div>").addClass("form-group");

			fgCalificacion.append([

				$("<button>Calificar!</button>").addClass("btn btn-success").click(function(event){
					var calificacion = $("#cita-calificar-sel-"+i+" option:selected").val();
					calificarCita(cita, calificacion);
				}),
				$("<select id='cita-calificar-sel-"+i+"' required></select>").addClass("form-control").append([
					$("<option value=''> </option>"),
					$("<option value='1'>1 - Muy malo</option>"),
					$("<option value='2'>2 - Malo</option>"),
					$("<option value='3'>3 - Regular</option>"),
					$("<option value='4'>4 - Bueno</option>"),
					$("<option value='5'>5 - Muy Bueno</option>")
				])
			]);

			formCalificacion.append(fgCalificacion);

			/*

			<div class="form-inline pull-right">
              <div class="form-group">
                <label for="cita-historial-area-sel"><h4>Calificar:</h4></label>
                &nbsp&nbsp&nbsp
                <select class="form-control" id="cita-historial-area-sel">
                  <option value="1">1 - Muy malo</option>
                  <option value="2">2 - Malo</option>
                  <option value="3">3 - Regular</option>
                  <option value="4">4 - Bueno</option>
                  <option value="5">5 - Muy bueno</option>
                </select>
                <button class="btn btn-success">
                  Calificar!
                </button>
              </div>
            </div>
			*/

			var inf = [
						$("<hr>"),
						$("<h4>Profesional: "+cita.profesional+"</h4>"),
						$("<h4>Area: "+cita.area+"</h4>"),
						$("<h4>Pago: "+cita.pago+"</h4>"),
						formCalificacion,
						$("<br>")
					  ]

			var ulDiag = $("<ul></ul>").addClass("list-group");

			$.each(cita.diagnosticos, function(i, d){
				ulDiag.append($("<li>"+d+"</li>").addClass("list-group-item"))
			})

			var ulMeds = $("<ul></ul>").addClass("list-group");

			$.each(cita.medicamentos, function(i, m){
				ulMeds.append($("<li>"+m+"</li>").addClass("list-group-item"))
			})

			var coll = [
						$("<button>Ver diagnostico y medicamentos</button>").addClass("btn btn-default btn-diagnostico").click(function(){
							$("#diagnostico-collapse-"+i).collapse('toggle');
						}),
						$("<div id='diagnostico-collapse-"+i+"'></div>").addClass("collapse collapse-diagnostico").append(
							$("<div></div>").addClass("well").append([
								$("<h4>Diagnosticos y notas</h4>"),
								ulDiag,
								$("<br>"),
								$("<h4>Medicamentos</h4>"),
								ulMeds
							])
						)
					   ]

			panelBody.append(row).append(inf).append(coll);

			$("#citas-lista").append($("<div></div>").addClass("panel panel-default cita-afiliado-panel").append(panelBody));

		});

		/*
		<div class="panel panel-default cita-afiliado-panel">
          <div class="panel-body">
            <div class="row">
              <h4 class="col-xs-4 h4">
                Fecha
              </h4>
              <div class="col-xs-5 col-xs-offset-3">
                <h4 class="pull-right h4">
                  Tipo
                </h4>
              </div>
            </div>
            <hr>
            <h4>Profesional</h4>
            <h4>Area</h4>
            <h4>Pago: (si, cuanto?/no)</h4>
            <br>
            <button class="btn btn-default btn-diagnostico" type="button" id="diag-collapse-btn">
              Ver diagnostico y medicamentos
            </button>
            <div class="collapse collapse-diagnostico" id="diagnostico-collapse">
              <div class="well">
                <h4>Diagnósticos y notas</h4>
                <ul class="list-group">
                  <li class="list-group-item">
                    Diag 1
                  </li>
                  <li class="list-group-item">
                    Diag 2
                  </li>
                  <li class="list-group-item">
                    Diag 3
                  </li>
                </ul>
                <br>
                <h4>Medicamentos</h4>
                <ul class="list-group">
                  <li class="list-group-item">
                    Med 1
                  </li>
                  <li class="list-group-item">
                    Med 2
                  </li>
                  <li class="list-group-item">
                    Med 3
                  </li>
                </ul>
              </div>
            </div>

          </div>
        </div> 
		*/
	}


	$("#diag-collapse-btn").click(function(event) {
		$("#diagnostico-collapse").collapse('toggle');
	});

})