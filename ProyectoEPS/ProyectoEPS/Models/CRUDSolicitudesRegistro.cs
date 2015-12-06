using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoEPS.Models;
using Oracle.DataAccess.Client;


namespace ProyectoEPS.Models
{
    public class CRUDSolicitudesRegistro : Conexion
    {
        public CRUDSolicitudesRegistro() : base()
        {

        }

        public void nuevaSolicitud(string idAfiliado,string passwordAfiliado, string nombreAfiliado, string apellidosAfiliado, string categoriaAfiliado,string edadAfiliado,string correoAfiliado,string cedulaAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "solicitudes_paquete.nuevaSolicitud";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter passwordAfiliadoP = new OracleParameter("passwordAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            passwordAfiliadoP.Value = passwordAfiliado;

            OracleParameter nombreAfiliadoP = new OracleParameter("nombreAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreAfiliadoP.Value = nombreAfiliado;

            OracleParameter apellidosAfiliadoP = new OracleParameter("apellidosAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            apellidosAfiliadoP.Value = apellidosAfiliado;

            OracleParameter categoriaAfiliadoP = new OracleParameter("categoriaAfiliado", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            categoriaAfiliadoP.Value = int.Parse(categoriaAfiliado);

            OracleParameter edadAfiliadoP = new OracleParameter("edadAfiliadoP", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            edadAfiliadoP.Value = int.Parse(edadAfiliado);

            OracleParameter correoAfiliadoP = new OracleParameter("correoAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            correoAfiliadoP.Value = correoAfiliado;

            OracleParameter cedulaAfiliadoP = new OracleParameter("cedulaAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            cedulaAfiliadoP.Value = cedulaAfiliado;

            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, passwordAfiliadoP, nombreAfiliadoP, apellidosAfiliadoP, categoriaAfiliadoP,
            edadAfiliadoP, correoAfiliadoP, cedulaAfiliadoP});

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public void aprobarSolicitud(string numeroSolicitud)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "solicitudes_paquete.aprobarSolicitud";

            OracleParameter numeroSolicitudP = new OracleParameter("numeroSolicitud", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            numeroSolicitudP.Value = int.Parse(numeroSolicitud);

            cmd.Parameters.Add(numeroSolicitudP);

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }

        public void rechazarSolicitud(string numeroSolicitud)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "solicitudes_paquete.rechazarSolicitud";

            OracleParameter numeroSolicitudP = new OracleParameter("numeroSolicitud", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            numeroSolicitudP.Value = int.Parse(numeroSolicitud);

            cmd.Parameters.Add(numeroSolicitudP);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }

        public List<SolicitudRegistro> verTodasLasSolicitudes()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "solicitudes_paquete.verTodasLasSolicitudes";

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add(cursor_datos);

            OracleDataReader lectorDatos = cmd.ExecuteReader();

            List<SolicitudRegistro> solicitudesConsulta = new List<SolicitudRegistro>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    solicitudesConsulta.Add(new SolicitudRegistro() {
                        numero = lectorDatos.GetInt16(0),
                        id = lectorDatos.GetString(1),
                        password = lectorDatos.GetString(2),
                        nombre = lectorDatos.GetString(3),
                        apellidos = lectorDatos.GetString(4),
                        categoria = lectorDatos.GetInt16(5),
                        edad = lectorDatos.GetInt16(6),
                        estado = lectorDatos.GetString(7),
                        correo = lectorDatos.GetString(8),
                        cedula = lectorDatos.GetString(9)
                    });
                }

            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return solicitudesConsulta;

        }

        public List<SolicitudRegistro> verSolicitudesAprobadas()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "solicitudes_paquete.verSolicitudesAprobadas";

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add(cursor_datos);

            OracleDataReader lectorDatos = cmd.ExecuteReader();

            List<SolicitudRegistro> solicitudesConsulta = new List<SolicitudRegistro>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    solicitudesConsulta.Add(new SolicitudRegistro()
                    {
                        numero = lectorDatos.GetInt16(0),
                        id = lectorDatos.GetString(1),
                        password = lectorDatos.GetString(2),
                        nombre = lectorDatos.GetString(3),
                        apellidos = lectorDatos.GetString(4),
                        categoria = lectorDatos.GetInt16(5),
                        edad = lectorDatos.GetInt16(6),
                        estado = lectorDatos.GetString(7),
                        correo = lectorDatos.GetString(8),
                        cedula = lectorDatos.GetString(9)
                    });
                }

            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return solicitudesConsulta;

        }

        public List<SolicitudRegistro> verSolicitudesRechazadas()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "solicitudes_paquete.verSolicitudesRechazadas";

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add(cursor_datos);

            OracleDataReader lectorDatos = cmd.ExecuteReader();

            List<SolicitudRegistro> solicitudesConsulta = new List<SolicitudRegistro>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    solicitudesConsulta.Add(new SolicitudRegistro()
                    {
                        numero = lectorDatos.GetInt16(0),
                        id = lectorDatos.GetString(1),
                        password = lectorDatos.GetString(2),
                        nombre = lectorDatos.GetString(3),
                        apellidos = lectorDatos.GetString(4),
                        categoria = lectorDatos.GetInt16(5),
                        edad = lectorDatos.GetInt16(6),
                        estado = lectorDatos.GetString(7),
                        correo = lectorDatos.GetString(8),
                        cedula = lectorDatos.GetString(9)
                    });
                }

            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return solicitudesConsulta;

        }


        public SolicitudRegistro detalleSolicitud(string numeroSolicitud)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "solicitudes_paquete.detalleSolicitud";

            OracleParameter numeroSolicitudP = new OracleParameter("numeroSolicitud", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            numeroSolicitudP.Value = int.Parse(numeroSolicitud);

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] {numeroSolicitudP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();

            SolicitudRegistro solicitudConsulta = new SolicitudRegistro();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    solicitudConsulta = new SolicitudRegistro()
                    {
                        numero = lectorDatos.GetInt16(0),
                        id = lectorDatos.GetString(1),
                        password = lectorDatos.GetString(2),
                        nombre = lectorDatos.GetString(3),
                        apellidos = lectorDatos.GetString(4),
                        categoria = lectorDatos.GetInt16(5),
                        edad = lectorDatos.GetInt16(6),
                        estado = lectorDatos.GetString(7),
                        correo = lectorDatos.GetString(8),
                        cedula = lectorDatos.GetString(9)
                    };
                }

            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return solicitudConsulta;

        }

        public List<SolicitudRegistro> verSolicitudesPendientes()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "solicitudes_paquete.verSolicitudesPendientes";

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add(cursor_datos);

            OracleDataReader lectorDatos = cmd.ExecuteReader();

            List<SolicitudRegistro> solicitudesConsulta = new List<SolicitudRegistro>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    solicitudesConsulta.Add(new SolicitudRegistro()
                    {
                        numero = lectorDatos.GetInt16(0),
                        id = lectorDatos.GetString(1),
                        password = lectorDatos.GetString(2),
                        nombre = lectorDatos.GetString(3),
                        apellidos = lectorDatos.GetString(4),
                        categoria = lectorDatos.GetInt16(5),
                        edad = lectorDatos.GetInt16(6),
                        estado = lectorDatos.GetString(7),
                        correo = lectorDatos.GetString(8),
                        cedula = lectorDatos.GetString(9)
                    });
                }

            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return solicitudesConsulta;
        }


    }
}