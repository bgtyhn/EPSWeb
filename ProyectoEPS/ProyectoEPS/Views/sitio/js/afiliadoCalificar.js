$(function(){
	if(sessionStorage.getItem('rol')!='afiliado'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));

	var idSesion = sessionStorage.getItem('id');

	$("#cita-calificar-area-sel").val("todas");

	var area = $("#cita-calificar-area-sel option:selected").val();

	cargarCitasCalificar(area);

	$("#cita-calificar-area-sel").change(function(event) {
		cargarCitas()
	});

	function cargarCitas(){
		var areaSel = $("#cita-calificar-area-sel option:selected").val();
		if(areaSel == "todas"){
			cargarCitasCalificar();
		} else {
			cargarCitasArea(areaSel)
		}
	}

	function calificarCita(cita, calificacion){

		$("#aviso-"+cita.id).remove();

		console.log("cita: "+cita.id+", calificacion "+calificacion);

		if(calificacion == '')
			$("#cita-calificar-sel-"+cita.id).after($("<span id='aviso-"+cita.id+"'><br>Debe seleccionar</span>").addClass("curso-aviso-inscripcion"));
		else{

			var message = { 
				accion : 'calificarAtencion',
				mensaje : '',
				valores : {
							"idCita" : cita.id,
							"calificacionCita" : calificacion
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
		        	if(data.exito == 1){
		        		toastr.success("Cita calificada con exito")
		        	}
		        	cargarCitas();
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });


		}
	}

	function cargarCitasArea(area){

		$("#citas-lista").empty();

		var message = { 
				accion : 'citasAtentididasCalificarArea',
				mensaje : '',
				valores : {
							"idUsuario" : idSesion,
							"areaCita" : area
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
	        	llenarCitas(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });


	}

	function cargarCitasCalificar(){

		$("#citas-lista").empty();

		var message = { 
				accion : 'citasAtendidasAfiliado',
				mensaje : '',
				valores : {
							"idUsuario" : idSesion
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
	        	llenarCitas(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });


	}

	function llenarCitas(citas){

		

		$.each(citas, function(i, cita){

			var panelBody = $("<div></div>").addClass("panel-body");

			var row = $("<div></div>").addClass("row").append([
					$("<h4>"+cita.fecha+"</h4>").addClass("col-xs-4 h4"),
					$("<div></div>").addClass("col-xs-5 col-xs-offset-3").append(
						$("<h4>"+cita.tipo_atencion+"</h4>").addClass("pull-right h4")
					)
				]);

			var formCalificacion = $("<div></div>").addClass("form-inline pull-right");

			var fgCalificacion = $("<div></div>").addClass("form-group");

			fgCalificacion.append([

				$("<button>Calificar!</button>").addClass("btn btn-success").click(function(event){
					var calificacion = $("#cita-calificar-sel-"+cita.id+" option:selected").val();
					calificarCita(cita, calificacion);
				}),
				$("<select id='cita-calificar-sel-"+cita.id+"' required></select>").addClass("form-control").append([
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
						$("<h4>Pago: "+cita.pago+"</h4>"),
						formCalificacion,
						$("<br>")
					  ]

			var ulDiag = $("<ul></ul>").addClass("list-group");

			llenarDiagnosticosCita(cita, ulDiag)

			var ulMeds = $("<ul></ul>").addClass("list-group");

			llenarMedicamentosCita(cita, ulMeds)

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

	function llenarDiagnosticosCita(cita, ulDiag){

		var message = { 
				accion : 'diagnosticosCita',
				mensaje : '',
				valores : {
							"idCita" : cita.id
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

	        	$.each(data.datos, function(i, d){
					ulDiag.append($("<li>"+d.descripcion+"</li>").addClass("list-group-item"))
				})

	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

		
	}

	function llenarMedicamentosCita(cita, ulMeds){
		var message = { 
				accion : 'medicamentosCita',
				mensaje : '',
				valores : {
					"idCita" : cita.id
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

	        	$.each(data.datos, function(i, d){
					ulMeds.append($("<li>"+d.nombre+"</li>").addClass("list-group-item"))
				})

	        },
	        error: function (info) {
	            alert("error");
	        }
	    });
	}


	$("#diag-collapse-btn").click(function(event) {
		$("#diagnostico-collapse").collapse('toggle');
	});

})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}