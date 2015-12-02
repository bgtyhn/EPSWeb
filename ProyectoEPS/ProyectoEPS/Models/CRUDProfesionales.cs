using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace ProyectoEPS.Models
{
    public class CRUDProfesionales : Conexion
    {

        public CRUDProfesionales(): base()
        {

        }

        public List<Cita> citasDelDia(string idProfesional, string fechaDia)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "profesionales_paquete.citasDelDia";
            OracleParameter idProfesionalP = new OracleParameter("idProfesional", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idProfesionalP.Value = idProfesional;
            OracleParameter fechaDiaP = new OracleParameter("fechaDia", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaDiaP.Value = fechaDia;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.Varchar2, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idProfesionalP, fechaDiaP, cursor_datos });
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

        public Profesional verDetalleProfesional(string idProfesional)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "profesionales_paquete.verDetalleProfesional";
            OracleParameter idProfesionalP = new OracleParameter("idProfesional", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idProfesionalP.Value = idProfesional;
            cmd.Parameters.Add(idProfesionalP);

            Profesional profesionalConsulta = new Profesional();
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    profesionalConsulta = new Profesional()
                    {
                        id = lectorDatos.GetString(0),
                        password = lectorDatos.GetString(1),
                        nombre = lectorDatos.GetString(2),
                        apellidos = lectorDatos.GetString(3),
                        cedula = lectorDatos.GetString(5),
                        correo = lectorDatos.GetString(6),
                        area = lectorDatos.GetString(7)
                    };
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return profesionalConsulta;

        }

        public void editarInformacionProfesional()
        {
            //falta hasta que se sepa como montar imagenes
        }

        public void eliminarProfesional(string idProfesional)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "profesionales_paquete.eliminarProfesional";
            OracleParameter idProfesionalP = new OracleParameter("idProfesional", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idProfesionalP.Value = idProfesional;
            cmd.Parameters.Add(idProfesionalP);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            base.cerrarConexion();
        }

        public List<Profesional> listarProfesionales()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "profesionales_paquete.listarProfesionales";
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add(cursor_datos);
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

        public List<Profesional> buscarProfesional(string valorBusqueda)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "profesionales_paquete.buscarProfesional";
            OracleParameter valorBusquedaP = new OracleParameter("valorBusqueda", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            valorBusquedaP.Value = valorBusqueda;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { valorBusquedaP, cursor_datos});
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

        public List<Profesional> historialCitas(string idProfesional)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "profesionales_paquete.historialCitas";
            OracleParameter idProfesionalP = new OracleParameter("idProfesional", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idProfesionalP.Value = idProfesional;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idProfesionalP, cursor_datos});

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

        public List<Cita> citasAtendidasProfesional(string idProfesional)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "profesionales_paquete.citasAtendidasProfesional";
            OracleParameter idProfesionalP = new OracleParameter("idProfesional", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idProfesionalP.Value = idProfesional;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.Varchar2, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idProfesionalP, cursor_datos });

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


        public List<Cita> citasPendientesProfesional(string idProfesional)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "profesionales_paquete.citasPendientesProfesional";
            OracleParameter idProfesionalP = new OracleParameter("idProfesional", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idProfesionalP.Value = idProfesional;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.Varchar2, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idProfesionalP, cursor_datos});

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