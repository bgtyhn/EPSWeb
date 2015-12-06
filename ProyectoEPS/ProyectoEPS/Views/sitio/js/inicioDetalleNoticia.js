$(function(){

	var id = $.getUrlVar("id");

	$("#login-form").submit(function(event) {
		event.preventDefault();
		console.log("submit login");
	});

	$("#solicitud-form").submit(function(event) {
		event.preventDefault();
		console.log("submit solicitud");
	});

	$("#a-solicitar-registro").click(function(event) {
		$("#modal-solicitud-registro").modal('show');
	});

	$("#solicitud-cancelar").click(function(event) {
		$("#solicitud-form").trigger("reset");
		$("#modal-solicitud-registro").modal('hide');
	});

	pedirNoticia()

	otrasNoticias()

	function pedirNoticia(){

		var message = { 
			accion : 'detalleNoticia',
			mensaje : '',
			valores : { "idNoticia" : id }
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
                cargarNoticia(data.datos);
            },
            error: function (info) {
                alert("error");
            }
        });

	}

	function cargarNoticia(noticia){

		$("#noticia-titulo").html(noticia.titulo);
		$("#noticia-encabezado").html(noticia.encabezado);
		$("#noticia-fecha").html(noticia.fecha_publicacion);
		$("#noticia-contenido").html(noticia.contenido);
	}

	function otrasNoticias(){

		var message = { 
			accion : 'mostrarNoticiasTodas',
			mensaje : '',
			valores : {}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Noticia/accionNoticias',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data)
                crearNoticias(data.datos);
            },
            error: function (info) {
                alert("error");
            }
        });


	}

	function crearNoticias(noticias){

		$.each(noticias, function(i, noticia){
		
			if(noticia.id != id){
				var a = '<a href="noticias.html?id='+noticia.id+'"><div class="main-evento"><span class="main-tit-evento">'+noticia.titulo+'</span><br><br><p class="text-justify">'+noticia.encabezado+'</p></div></a><hr>'
				$("#main-news").append(a);
			}

		})
	}

});