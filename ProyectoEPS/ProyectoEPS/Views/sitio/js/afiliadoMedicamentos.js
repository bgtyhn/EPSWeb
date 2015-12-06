$(function(){

	if(sessionStorage.getItem('rol')!='afiliado'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	//stub
	var idSesion = sessionStorage.getItem('id');

	var categoriaUsuario;

	obtenerCategoriaUsuario();

	$("#categoria-med-sel").val(categoriaUsuario);

	cargarListaMedicamentos(categoriaUsuario);

	$("#categoria-med-sel").change(function(event) {
		categoriaSel = $("#categoria-med-sel option:selected").val();
		if(categoriaSel != ""){
			cargarListaMedicamentos(categoriaSel);
		}
	});

	function obtenerCategoriaUsuario(id){

		var message = { 
				accion : 'verDatosAfiliado',
				mensaje : '',
				valores : {
					"idAfiliado" : idSesion
				}
		}

		$.ajax({
			async : false,
	        type: 'POST',
	        url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	categoriaUsuario = data.datos.categoria
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

	}

	function cargarListaMedicamentos(categoria){

		var message = { 
				accion : 'posAfiliado',
				mensaje : '',
				valores : {
					"idCategoriaAfiliado" : categoria
				}
		}

		$.ajax({
			async : false,
	        type: 'POST',
	        url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	llenarLista(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });
		
	}
	
	function llenarLista(medicamentos){

		//Llenar lista

		var lista = $("#medicamentos-lista");

		lista.empty();

		$.each(medicamentos, function(i, m){

			var panelMed = $("<div></div>").addClass("panel panel-default medicamento-panel");

			var panelBody = $("<div></div>").addClass("panel-body");

			panelBody.append([
				$("<img src='http://placehold.it/150x150'>").addClass('afiliado-img-medicamento'),
				$("<h4>"+m.nombre+"</h4>"),
				$("<span>"+m.descripcion+"</span>")
			]);

			lista.append(panelMed.append(panelBody));

		});

	}

})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}