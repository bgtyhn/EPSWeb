$(function(){
	if(sessionStorage.getItem('rol')!='afiliado'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	var idUsuario = sessionStorage.getItem('id');

	llenarTablaMultas();


	function llenarTablaMultas(){

		console.log(idUsuario);

		var message = { 
			accion : 'multasPendientes',
			mensaje : '',
			valores : {
						"idAfiliado" : idUsuario
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
	        	llenarFilas(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });
	}

	function llenarFilas(multas){

		var pendientes = 0;
		var anteriores = 0;

		//llenar tbody

		$.each(multas, function(i, multa){

			var tr = $("<tr></tr>").append([
				$("<td>"+multa.valor+"</td>"),
				$("<td>"+multa.estado+"</td>")
			])

			if(multa.estado == "pago"){
				$("#multas-anteriores-tbody").append(tr);
				anteriores++;
			}
			else{
				$("#multas-pendientes-tbody").append(tr);
				pendientes++;
			}

		});

		if(anteriores == 0)
			$("#multas-anteriores-tabla").after($("<h4>No tiene multas anteriores</h4>"))

		if(pendientes == 0)
			$("#multas-pendientes-tabla").after($("<h4>No tiene multas pendientes</h4>"))

	}

})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}