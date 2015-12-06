$(function(){
	if(sessionStorage.getItem('rol')!='afiliado'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));

	var idUsuario = "2";

	llenarTablaSanciones();


	function llenarTablaSanciones(){

		console.log(idUsuario);

		//Stub

		var sanciones = []

		var estados = ["pago", "pendiente"];

		for(i = 0; i < 15; i++){
			sanciones.push({
				fecha : "fecha "+i,
				monto : i*10000,
				estado : estados[i%2],
				descripcion : "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatibus, iusto possimus excepturi eveniet mollitia itaque! Numquam at aperiam, explicabo vitae?"
			})
		}


		var pendientes = 0;
		var anteriores = 0;

		//llenar tbody

		$.each(sanciones, function(i, sancion){

			var tr = $("<tr></tr>").append([
				$("<td>"+sancion.fecha+"</td>"),
				$("<td>"+sancion.monto+"</td>"),
				$("<td>"+sancion.estado+"</td>"),
				$("<td>"+sancion.descripcion+"</td>")
			])

			if(sancion.estado == "pago"){
				$("#sanciones-anteriores-tbody").append(tr);
				anteriores++;
			}
			else{
				$("#sanciones-pendientes-tbody").append(tr);
				pendientes++;
			}

		});

		if(anteriores == 0)
			$("#sanciones-anteriores-tabla").after($("<h4>No tiene sanciones anteriores</h4>"))

		if(pendientes == 0)
			$("#sanciones-pendientes-tabla").after($("<h4>No tiene sanciones pendientes</h4>"))

	}

})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}