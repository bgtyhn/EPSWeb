$(function(){
	if(sessionStorage.getItem('rol')!='profesional'){
		window.location.href = '../../';
	}

	var idSesion =  sessionStorage.getItem('id');

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	$("#citas-filtro-fecha").datepicker({
		format : "yyyy/mm/dd",
		startDate : "2013/01/01",
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

	$("#citas-filtro-fecha").val(new Date());

	function cargarCitasRecientes(fecha){

		$("#citas-prof-lista").empty();

		var message = { 
			accion : 'citasDelDia',
			mensaje : '',
			valores : { 
				"idProfesional" : idSesion,
				"fechaDia" : fecha
			}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Profesional/accionProfesionales',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data);
                llenarLista(data.datos);
            },
            error: function (info) {
                alert("error");
            }
        });

		

	}

	function llenarLista(citas){

		$.each(citas, function(i, cita){

			var panel = $("<div></div>").addClass("panel panel-default cita-panel");

			var pheading = $("<div></div>").addClass("panel-heading").append(
					$("<div></div>").addClass("panel-title").append(
							$("<h4>"+cita.fecha+"</h4>")
						)
				)

			var pbody = $("<div></div>").addClass("panel-body cita-pbody").append([
					$("<span><strong>Paciente: </strong>"+cita.afiliado+"</span>"),
					$("<br>"),
					$("<span><strong>Tipo: </strong>"+cita.tipo_atencion+"</span>"),
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
		window.location.href = './detalleCita.html?id='+id;
	}

})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}