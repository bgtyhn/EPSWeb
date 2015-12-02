using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;


namespace ProyectoEPS.Models
{
    public class CRUDCitas : Conexion
    {
        public CRUDCitas() : base()
        {

        }

        public void crearCita(string fechaCita, int duracionCita, int tipo_atencionCita, string estadoCita, int calificacionCita, string profesionalCita, string afiliadoCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.crearCita";
            OracleParameter fechaCitaP = new OracleParameter("fechaCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaCitaP.Value = fechaCita;
            OracleParameter duracionCitaP = new OracleParameter("duracionCita", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            duracionCitaP.Value = duracionCita;
            OracleParameter tipo_atencionCitaP = new OracleParameter("tipo_atencionCita", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            tipo_atencionCitaP.Value = tipo_atencionCita;
            OracleParameter estadoCitaP = new OracleParameter("estadoCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            duracionCitaP.Value = estadoCita;
            OracleParameter calificacionCitaP = new OracleParameter("calificacionCita", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            calificacionCitaP.Value = calificacionCita;
            OracleParameter profesionalCitaP = new OracleParameter("profesionalCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            calificacionCitaP.Value = calificacionCita;
            OracleParameter afiliadoCitaP = new OracleParameter("afiliadoCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            afiliadoCitaP.Value = afiliadoCita;
            cmd.Parameters.AddRange(new OracleParameter[] { fechaCitaP, duracionCitaP, tipo_atencionCitaP,
                                        estadoCitaP, calificacionCitaP, profesionalCitaP, afiliadoCitaP});
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw e;
            }
        }

        public List<Cita> citasIncumplidas(string idUsuario)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasIncumplidas";
            OracleParameter idUsuarioP = new OracleParameter("idUsuario", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idUsuarioP.Value = idUsuario;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idUsuarioP, cursor_datos });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Cita> citasConsulta = new List<Cita>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    citasConsulta.Add(new Cita()
                    {
                        id = lectorDatos.GetInt16(0),
                        fecha = lectorDatos.GetDateTime(1).ToString(),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();
                
            }
            return citasConsulta;
           
        }

        public List<Cita> citasProximas(string idUsuario)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasProximas";
            OracleParameter idUsuarioP = new OracleParameter("idUsuario", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idUsuarioP.Value = idUsuario;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idUsuarioP, cursor_datos });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Cita> citasConsulta = new List<Cita>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    citasConsulta.Add(new Cita()
                    {
                        id = lectorDatos.GetInt16(0),
                        fecha = lectorDatos.GetDateTime(1).ToString(),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;

        }

        public List<Cita> verHorariosCitas(string areaCita, string fechaSolicitada)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.verHorariosCitas";
            OracleParameter areaCitaP = new OracleParameter("areaCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            areaCitaP.Value = areaCita;
            OracleParameter fechaSolicitadaP = new OracleParameter("fechaSolicitada", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaSolicitadaP.Value = fechaSolicitada;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { areaCitaP, fechaSolicitadaP, cursor_datos });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Cita> citasConsulta = new List<Cita>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    citasConsulta.Add(new Cita()
                    {
                        id = lectorDatos.GetInt16(0),
                        fecha = lectorDatos.GetDateTime(1).ToString(),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;

        }

        public List<Profesional> verProfesionalesArea(string areaDoctor)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.verProfesionalesArea";
            OracleParameter areaDoctorP = new OracleParameter("areaDoctor", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            areaDoctorP.Value = areaDoctor;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { areaDoctorP, cursor_datos });
            List<Profesional> profesionalesConsulta = new List<Profesional>();
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    profesionalesConsulta.Add(new Profesional()
                    {
                        id = lectorDatos.GetString(0),
                        password = lectorDatos.GetString(1),
                        nombre = lectorDatos.GetString(2),
                        apellidos = lectorDatos.GetString(3),
                        cedula = lectorDatos.GetString(5),
                        correo = lectorDatos.GetString(6),
                        area = lectorDatos.GetString(7)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return profesionalesConsulta;
        }

        public int consultaPago(int idCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.consultarPago";
            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            idCitaP.Value = idCitaP;
            OracleParameter valorPago = new OracleParameter("valorPago", OracleDbType.Int32, System.Data.ParameterDirection.Output);
            valorPago.Size = 1024;
            cmd.Parameters.AddRange(new OracleParameter[] { idCitaP, valorPago });
            cmd.ExecuteNonQuery();
            int pago = int.Parse(cmd.Parameters["valorPago"].Value.ToString());
            cmd.Dispose();
            base.cerrarConexion();
            return pago;
        }

        public List<Cita> citasDia(string fechaDia) {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasDia";
            OracleParameter fechaDiaP = new OracleParameter("fechaDia", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaDiaP.Value = fechaDia;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { fechaDiaP, cursor_datos });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Cita> citasConsulta = new List<Cita>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    citasConsulta.Add(new Cita()
                    {
                        id = lectorDatos.GetInt16(0),
                        fecha = lectorDatos.GetDateTime(1).ToString(),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;

        } 

        public List<Cita> mostrarPendientes()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.mostrarPendientes";
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add(cursor_datos);
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Cita> citasConsulta = new List<Cita>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    citasConsulta.Add(new Cita()
                    {
                        id = lectorDatos.GetInt16(0),
                        fecha = lectorDatos.GetDateTime(1).ToString(),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;

        }



    }
}