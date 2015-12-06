$(function(){

	if(sessionStorage.getItem('rol')!='atencioncliente'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	cargarAfiliados("");

	$("#atencion-busqueda-afiliado").on("input", function(){
		cargarAfiliados($("#atencion-busqueda-afiliado").val());
	})

	function cargarAfiliados(entrada){

		if(entrada == ""){
			ajaxTodosAfiliados();
		} else {
			ajaxBuscarAfiliados(entrada);
		}

	}

	function ajaxTodosAfiliados(){
		var message = { 
				accion : 'listarAfiliados',
				mensaje : '',
				valores : {
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

	        	llenarListaAfiliados(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });
	}

	function ajaxBuscarAfiliados(nombre){
		var message = { 
				accion : 'buscarAfiliado',
				mensaje : '',
				valores : {
							"nombreAfiliado" : nombre
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

		        	llenarListaAfiliados(data.datos)
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });
	}
		
	function llenarListaAfiliados(afiliados){

		$("#afiliados-atencion-lista").empty();

		$.each(afiliados, function(i, afiliado){

			var panel = $("<div></div>").addClass("panel panel-default cita-panel");

			var pheading = $("<div></div>").addClass("panel-heading").append(
					$("<div></div>").addClass("panel-title").append(
							$("<h4>"+afiliado.nombre+" "+afiliado.apellidos+"</h4>")
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
		window.location.href = './detalle-afiliado.html?id='+id;
	}

})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}