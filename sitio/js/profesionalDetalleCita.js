$(function(){

	$("form").trigger("reset")


	$("#cita-med-agregar").select2();

	var idVar = $.getUrlVar("id");
	console.log(idVar)

	
	var cita = cargarCita(idVar);


	function cargarCita(idVar){
		if(idVar != undefined){

			//Stub
			var cita = {
				id : idVar,
				paciente : "Carlos paciente", 
				categoria : "Carlos categoria",
				fecha : new Date("2015/12/03"),
				tipo : "control",
				estado : "atendida"
			}

			console.log(cita.fecha);

			$("#cita-nombre-pac").html(cita.paciente);
			$("#cita-cat-pac").html(cita.categoria);
			$("#cita-fecha").html(cita.fecha.toDateString());
			$("#cita-tipo").html(cita.tipo);
			$("#cita-estado").html(cita.estado);

			if(cita.estado == "incumplida"){
				$("#modificables-cita").empty();
				$("#botones-estado-cita").empty();
			} else if (cita.estado == "pendiente"){
				cargarDiagnosticos();

				cargarMedicamentos();

				$("#cita-med-form").on("submit", function(event){
					event.preventDefault();
					agregarMedicamento();
				})

				$("#cita-diag-form").on("submit", function(event){
					event.preventDefault();
					agregarDiagnostico();
				})

				$("#btn-atendida").click(function(){
					darCitaAtendida();
				})

				$("#btn-incumplida").click(function(){
					darCitaIncumplida();
				})

			} else if (cita.estado == "atendida"){
				cargarDiagnosticos();

				cargarMedicamentos();

				$(".form-control").prop("disabled", true)

				$(".btn").prop("disabled", true)

				$("#botones-estado-cita").empty();

			}

		}
	}

	function cargarDiagnosticos(){
		//Conseguir diagnosticos de cita

		//stub
		var diagnosticos = [
			"dolor de cabeza "+idVar,
			"dolor de cabeza "+idVar,
			"dolor de cabeza "+idVar
		]

		$.each(diagnosticos, function(i, diagnostico){
					$("#cita-diag-lista").append(
						$("<li>"+diagnostico+"</li>").addClass("list-group-item")
						)
				});

	}

	function cargarMedicamentos(){
		//Conseguir medicamentos de cita

		//stub
		var medicamentos = [
			"ibuprofeno",
			"acetaminofen"
		]

		$.each(medicamentos, function(i, medicamento){
			$("#cita-med-lista").append(
				$("<li>"+medicamento+"</li>").addClass("list-group-item")
				)
		});

	}

	function agregarDiagnostico(){
		var diag = $("#cita-diag").val()
		console.log("agregarDiagnostico "+diag+" cita "+idVar);

		cargarDiagnosticos();
	}

	function agregarMedicamento(){
		var idMed = $("#cita-med-agregar option:selected").val()
		console.log("agregarMedicamento "+idMed+" cita "+idVar);


		cargarMedicamentos();
	}

	function darCitaAtendida(){
		console.log("cita "+idVar+" atendida");
	}


	function darCitaIncumplida(){
		console.log("cita "+idVar+" incumplida");
	}
})