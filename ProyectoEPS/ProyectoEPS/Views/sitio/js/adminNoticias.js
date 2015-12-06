$(function(){
	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}


	$("#sesion-nombre").html(sessionStorage.getItem('id'));

	
	var accion = ""
	var idNoticiaEditar = ""

	$("#lista-noticias").empty();

	cargarListaNoticias();

	$("#noticia-agregar").click(function(event) {
		$("#noticia-expiracion").datepicker({
			format : "yyyy/mm/dd",
			startDate : "today"
		});
		accion = "agregar"
		$("#titulo-modal-noticia").html("Registrar nueva noticia");
		$("#editar-noticia-form").trigger("reset");
		$("#modal-agregar-noticia").modal("show");
	});

	$("#edicion-cancelar").click(function(event){
		$("#modal-agregar-noticia").modal("hide");
	})

	$("#editar-noticia-form").on("submit", function(event){
		event.preventDefault();
		console.log(accion);
		
		if(accion == "agregar"){

			var message = { 
				accion : 'agregarNoticia',
				mensaje : '',
				valores : {
							"fecha_expiracionN" : $("#noticia-expiracion").val(),
							"contenidoN" : $("#noticia-contenido").val(),
							"encabezadoN" : $("#noticia-encabezado").val(),
							"tituloN" : $("#noticia-titulo").val()
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Noticia/accionNoticias',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	cargarListaNoticias();
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

		} else if (accion == "editar"){

			var message = { 
				accion : 'modificarNoticia',
				mensaje : '',
				valores : {
							"idN" : idNoticiaEditar,
							"fecha_expiracionN" : $("#noticia-expiracion").val(),
							"contenidoN" : $("#noticia-contenido").val(),
							"encabezadoN" : $("#noticia-encabezado").val(),
							"tituloN" : $("#noticia-titulo").val()
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Noticia/accionNoticias',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	cargarListaNoticias();
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

		}

		$("#modal-agregar-noticia").modal("hide");
		
	})


	function cargarListaNoticias(){

		$("#lista-noticias").empty();

		//Traer de bd

		var message = { 
			accion : 'mostrarNoticiasTodas',
			mensaje : '',
			valores : {}
		}

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Noticia/accionNoticias',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                crearNoticias(data.datos);
            },
            error: function (info) {
                alert("error");
            }
        });

	}

	function crearNoticias(noticias){

		$.each(noticias, function(i, noticia){

			/*

			<div class="main-noticia">
              <div class="panel panel-default">
                <div class="panel-heading">
                  <h3 class="panel-title title-noticia">
                    titulo Lorem ipsum dolor sit amet.
                  </h3>
                  <span class="label panel-title fecha-noticia pull-right">
                    fecha_publicacion
                  </span>
                </div>
                <div class="panel-body">
                  <img class="main-img-noticia" src="http://placehold.it/150x150">
                  <h5 class="h5 text-justify">
                    encabezado Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quisquam natus perspiciatis, aperiam, quidem nobis, maiores repudiandae numquam accusamus soluta ipsa officia delectus ratione reiciendis eveniet quam totam odit, tenetur voluptatibus perferendis ut at. Excepturi incidunt repudiandae, quisquam alias! Sed, cum iure soluta necessitatibus aspernatur amet unde, veritatis harum quisquam tenetur.
                  </h5>
                </div>
                <div class="panel-footer">
                  <button class="btn btn-danger pull-right">
                    <span class="glyphicon glyphicon-trash"></span>
                  </button>
                  <span class="pull-right">&nbsp;&nbsp;</span>
                  <button class="btn btn-warning pull-right">
                    <span class="glyphicon glyphicon-edit"></span>
                  </button>
                  <div class="clearfix"></div>
                </div>
              </div>
            </div>

			*/

			var div = $("<div></div>").addClass("main-noticia");

			var html = 	'<div class="panel panel-default">'+
							'<div class="panel-heading">'+
								'<h3 class="panel-title title-noticia">'+
									noticia.titulo+
								'</h3>'+
								'<span class="label panel-title fecha-noticia pull-right">'+
									noticia.fecha_publicacion+
								'</span>'+
							'</div>'+
							'<div class="panel-body">'+
								'<img class="main-img-noticia" src="http://placehold.it/150x150">'+
								'<h5 class="h5 text-justify">'+
									noticia.encabezado+
								'</h5>'+
							'</div>'+
							'<div class="panel-footer">'+
								'<button id = "borrar-noticia-'+noticia.id+'" class="btn btn-danger pull-right">'+
									'<span class="glyphicon glyphicon-trash"></span>'+
								'</button>'+
								'<span class="pull-right">&nbsp;&nbsp;</span>'+
								'<button id = "editar-noticia-'+noticia.id+'" class="btn btn-warning pull-right">'+
									'<span class="glyphicon glyphicon-edit"></span>'+
								'</button>'+
								'<div class="clearfix"></div>'+
							'</div>'+
						'</div>'

			div.append(html);

			$("#lista-noticias").append(div)

			$("#borrar-noticia-"+noticia.id).click(function(){
				eliminarNoticia(noticia);
			})

			$("#editar-noticia-"+noticia.id).click(function(){
				editarNoticia(noticia);
			})

		})

	}

	function eliminarNoticia(noticia){
		console.log("eliminar")
		console.log(noticia)

		var message = { 
			accion : 'eliminarNoticia',
			mensaje : '',
			valores : {
				"idNoticia" : noticia.id
			}
		}

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Noticia/accionNoticias',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                cargarListaNoticias();
            },
            error: function (info) {
                alert("error");
            }
        });
	}

	function editarNoticia(noticia){
		idNoticiaEditar = noticia.id;

		$("#noticia-expiracion").datepicker({
			format : "yyyy/mm/dd"
		});

		console.log(noticia);

		$("#titulo-modal-noticia").html("Editar noticia");
		accion = "editar";

		$("#editar-noticia-form").trigger("reset");

		$("#noticia-expiracion").val(noticia.fecha_expiracion);
		$("#noticia-titulo").val(noticia.titulo);
		$("#noticia-encabezado").val(noticia.encabezado);
		$("#noticia-contenido").val(noticia.contenido);

		$("#modal-agregar-noticia").modal("show");
	}



})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}