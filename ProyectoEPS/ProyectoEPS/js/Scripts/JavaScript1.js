$(document).ready(function () {

    $('#btn1').click(function () {
        texto = $('#txt1').val();
        alert(texto);
        //var diccionario = { parametro1: 'p1', parametro2: 'p2' };
        //var diccionario = { 'idN' : '2' ,'fecha_expiracionN': '2015/10/10', 'contenidoN' :'eL CONTENidoU ','encabezadoN' : 'EncabezaU', 'tituloNU' : 'titulozU'};
        var diccionario = { 'idNoticia' : '2'};
        //var diccionario = { 'areaCita' : 'med', 'fechaSolicitada' : '2015/10/10' }
        //var diccionario = {'idAfiliado' : 'any','passwordAfiliado': 'som', 'nombreAfiliado': 'name', 'apellidosAfiliado' : 'lastN', 'categoriaAfiliado' : '1','edadAfiliado' :'19','correoAfiliado' : 'myCorreo@', 'cedulaAfiliado' : '6754' };
        var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };
        $.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Noticia/accionNoticias',
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
        $.ajax({
            url: 'http://localhost:6570/api/Profesional/imagenProfesional',
            contentType: "application/json",
            dataType : "json",
            type: 'POST',
            success: function (data) {
                console.log(data);
                $("#imag").attr("src", data);
            }
        });
        

    });

    
});



function mostrarDatos(datos) {
    $.each(datos.datos, function () {
        console.log(this.first_name);
    });
}