$(document).ready(function () {

    $('#btn1').click(function () {
        texto = $('#txt1').val();
        alert(texto);
        //var diccionario = { parametro1: 'p1', parametro2: 'p2' };
        var diccionario = { 'idAfiliado': '1' };
        var message = { accion: 'cuotafiliado', mensaje: 'holi', valores: diccionario };
        $.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                console.log(data)
                mostrarDatos(data);
            },
            error: function (info) {
                alert("error");
            }
        });
    });

    $('#btnupload').click(function () {
        alert("sube");
        var data = new FormData();
        data.append("file", $("#uploadFile")[0].files[0]);
        data.append("myParameter", "test"); // with this param i get 404
        data.append("idUsuario", "awe");

        $.ajax({
            url: 'http://localhost:6570/api/Profesional/Upload',
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data) {
                console.log(data);
            }
        });
    });

    $('#mostrarI').click(function () {
        var options = {};
        options.url = "http://localhost:6570/api/Profesional/imagenProfesional";
        options.type = "POST";
        options.dataType = "json";
        options.contentType = "application/json";
        options.success = function (results) {
            console.log(results);
            $("#imgContainer").empty();
            for (var i = 0; i < results.length; i++) {
                $("#imgContainer").append("<img src='" + "api/images/" + results[i] + "' /> <br />");
            }
        };
        options.error = function (err) { alert("falla"); alert(err.statusText); };
        $.ajax(options);

    });

    
});



function mostrarDatos(datos) {
    $.each(datos.datos, function () {
        console.log(this.first_name);
    });
}