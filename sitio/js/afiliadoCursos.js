$(function(){
	//Generar lista de cursos

	generarCursos();

	function generarCursos(){
		var cursos = [];

		var nEventos = [1,4,3,2];

		for(i = 0; i < 10; i++){

			var ev = [];

			for(j = 0; j < nEventos[i%nEventos.length]; j++){
				ev.push({
					fecha : "Fecha "+j+" de "+i,
					horaInicio : "hora de inicio "+j+" "+i,
					horaFin : "hora de fin "+j+" "+i
				});
			}

			var insc = "";
			var req = "";

			switch(i%3){
				case 0:
					insc = "si";
					req = "si";
					break;
				case 1:
					insc = "no";
					req = "no";
					break;
				case 2:
					insc = "no";
					req = "si";
					break;
			}

			cursos.push({
				id : i,
				nombre : "Curso " + i,
				descripcion : "Descripción " + i + "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nam laboriosam eius beatae harum hic rem? Lorem ipsum dolor sit amet, consectetur adipisicing elit. Corrupti, expedita.",
				cupos : i*10,
				sitio : "Sitio " + i,
				encargado : "Encargado " + i,
				eventos : ev,
				inscrito : insc,
				requisitos : req,
				completo : "no"
			});
		}

		var lista = $("#lista-cursos");

		$.each(cursos, function(i, c){



			var listaEventos = $("<ul></ul>").addClass('list-group');
			$.each(c.eventos, function(i, e){
				listaEventos.append(
					"<li class='list-group-item'><strong>Sesión "+i+": </strong>"+e.fecha+" - de "+e.horaInicio+" a "+e.horaFin+"</li>"
				);
			});

			var mensaje = "";

			if(c.requisitos == "no" || c.completo == "si"){
				mensaje = "<span class='curso-aviso-inscripcion pull-right'>No cumple con los requisitos o se agotaron los cupos</span>"
			}

			var boton = "";

			if(c.requisitos == "no"){
				boton = '<button class="btn btn-danger pull-right" disabled ="disabled">No puede inscribirse</button>'
			} else if (c.inscrito == "si") {
				boton = '<button class="btn btn-success pull-right" disabled ="disabled">Ya está inscrito</button>';
			} else {
				boton = $("<button>Inscribirse</button>").addClass("btn btn-primary pull-right").click(function(event) {
					mostrarModalInscripcion(c);
				});
			}

			lista.append(
				$("<div></div>").addClass("afiliado-cursos").append(
					$("<div></div>").addClass('panel panel-default').append(
						$("<div></div>").addClass("panel-heading").append(
							$("<h3>"+c.nombre+"</h3>").addClass("panel-title")
						)).append(
						$("<div></div>").addClass('panel-body').append(
							$("<span>"+c.descripcion+"</span>")
						).append($("<br>")).append($("<br>")).append(
							$("<span><strong>Cupos: </strong>"+c.cupos+"</span>")
						).append($("<br>")).append(
							$("<span><strong>Encargado: </strong>"+c.encargado+"</span>")
						).append(
							$("<hr>")
						).append(
							listaEventos
						).append(
							mensaje
						)).append(
						$("<div></div>").addClass("panel-footer").append(
							boton
						).append(
							$("<div></div>").addClass("clearfix")
						)
					)
				)
			)

		});

	}

	function mostrarModalInscripcion(curso){
		var nombre = curso.nombre;
		$("#modal-curso-nombre").html("Seguro que se va a inscribir al curso "+nombre+"?");
		$('#modal-curso').modal('show');
	}

})