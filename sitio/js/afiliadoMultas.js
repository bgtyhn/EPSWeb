$(function(){
	
	var idUsuario = "2";

	llenarTablaMultas();


	function llenarTablaMultas(){

		console.log(idUsuario);

		//Stub

		var multas = []

		var estados = ["pago", "pendiente"];

		for(i = 0; i < 15; i++){
			multas.push({
				fecha : "fecha "+i,
				monto : i*10000,
				estado : estados[i%2]
			})
		}


		var pendientes = 0;
		var anteriores = 0;

		//llenar tbody

		$.each(multas, function(i, multa){

			var tr = $("<tr></tr>").append([
				$("<td>"+multa.fecha+"</td>"),
				$("<td>"+multa.monto+"</td>"),
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