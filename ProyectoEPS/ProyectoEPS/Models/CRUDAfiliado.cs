using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace ProyectoEPS.Models
{
    public class CRUDAfiliado : Conexion
    {
        public CRUDAfiliado() : base()
        {

        }

        public List<Medicamento> posAfiliado(string idCategoriaAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.posAfiliado";

            OracleParameter idCategoriaAfiliadoP = new OracleParameter("idCategoriaAfiliado", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCategoriaAfiliadoP.Value = int.Parse(idCategoriaAfiliado);

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idCategoriaAfiliadoP, cursor_datos });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Medicamento> medicamentosConsulta = new List<Medicamento>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    medicamentosConsulta.Add(new Medicamento
                    {
                        id = lectorDatos.GetString(0),
                        nombre = lectorDatos.GetString(1),
                        descripcion = lectorDatos.GetString(2),
                        categoria = lectorDatos.GetInt32(4)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return medicamentosConsulta;
        }

        public int cuotafiliado(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.cuotaAfiliado";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter cuotaAfiliadoP = new OracleParameter("cuotaAfiliado", OracleDbType.Int32, System.Data.ParameterDirection.Output);
            cuotaAfiliadoP.Size = 1024;

            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, cuotaAfiliadoP });
            cmd.ExecuteNonQuery();
            int resultado = int.Parse(cmd.Parameters["cuotaAfiliado"].Value.ToString());
            cmd.Dispose();
            base.cerrarConexion();
            return resultado;
        }

        public void calificarAtencion(string idCita, int calificacionCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.calificarAtencion";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idCitaP.Value = idCita;
            OracleParameter calificacionCitaP = new OracleParameter("calificacionCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            calificacionCitaP.Value = calificacionCita;

            cmd.Parameters.AddRange(new OracleParameter[] { idCitaP, calificacionCitaP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();


        }

        public List<Cita> historialDecitas(int idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.historialDeCitas";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;
            OracleParameter cursor_datos = new OracleParameter("idAfiliado", OracleDbType.Int32, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, cursor_datos });
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

            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return citasConsulta;
        }

        public List<Cita> mostrarPendienteAPagar(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.mostrarPendienteAPagar";

            OracleParameter idAfiliadoP = new OracleParameter("idAffiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, cursor_datos });
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

            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return citasConsulta;
        }

        //organizar en base de datos
        public List<Multa> multasPendientes(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.multasPendientes";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.Varchar2, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, cursor_datos });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Multa> multasConsulta = new List<Multa>();

            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    multasConsulta.Add(new Multa()
                    {
                        id = lectorDatos.GetInt32(0),
                        valor = lectorDatos.GetInt32(1),
                        cita = lectorDatos.GetInt32(2),
                        estado = lectorDatos.GetString(3)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return multasConsulta;

        }

        public void crearAfiliado()
        {
            //pendiente hasta imagen
        }

        public void editarAfiliado()
        {
            //pendiente hasta imagen
        }

        public void eliminarAfiliado(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.eliminarAfiliado";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            cmd.Parameters.Add(idAfiliadoP);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            base.cerrarConexion();
        }

        public Afiliado verDatosAfiliado(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.verDatosAfiliado";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            Afiliado afiliadoConsulta = new Afiliado();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    afiliadoConsulta = new Afiliado()
                    {
                        id = lectorDatos.GetString(0),
                        password = lectorDatos.GetString(1),
                        nombre = lectorDatos.GetString(2),
                        apellidos = lectorDatos.GetString(3),
                        edad = lectorDatos.GetInt32(4),
                        cedula = lectorDatos.GetString(5),
                        fecha_afiliacion = lectorDatos.GetString(6),
                        estado = lectorDatos.GetString(7),
                        categoria = lectorDatos.GetInt32(9)
                    };
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return afiliadoConsulta;
        }

        public void cambiarEstadoAfiliado(string idAfiliado, string nuevoEstado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.cambiarEstadoAfiliado";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter nuevoEstadoP = new OracleParameter("nuevoEstado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nuevoEstadoP.Value = nuevoEstado;

            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, nuevoEstadoP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }

    }
}