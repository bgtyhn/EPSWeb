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

        public List<Multa> multasPendientes(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.multasPendientes";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
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

        public List<Multa> multasPagas(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.multasPagas";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
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
                        descripcion = lectorDatos.GetString(2)
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

        public void calificarAtencion(string idCita, string calificacionCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.calificarAtencion";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idCitaP.Value = idCita;
            OracleParameter calificacionCitaP = new OracleParameter("calificacionCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            calificacionCitaP.Value = int.Parse(calificacionCita);

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
                        fecha = lectorDatos.GetString(1),
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
                        fecha = lectorDatos.GetString(1),
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


        public void crearAfiliado(string idAfiliado,string passwordAfiliado,string nombreAfiliado,string apellidosAfiliado,string edadAfiliado,string cedulaAfiliado,string fecha_afiliacionAfiliado, string estadoAfiliado ,string categoriaAfiliado,string correoAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.crearAfiliado";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter passwordAfiliadoP = new OracleParameter("passwordAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            passwordAfiliadoP.Value = passwordAfiliado;

            OracleParameter nombreAfiliadoP = new OracleParameter("nombreAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreAfiliadoP.Value = nombreAfiliado;

            OracleParameter apellidosAfiliadoP = new OracleParameter("apellidosAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            apellidosAfiliadoP.Value = apellidosAfiliado;

            OracleParameter edadAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            edadAfiliadoP.Value = int.Parse(edadAfiliado);

            OracleParameter cedulaAfiliadoP = new OracleParameter("cedulaAfiliadoP", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            cedulaAfiliadoP.Value = cedulaAfiliado;

            OracleParameter fecha_afiliacionAfiliadoP = new OracleParameter("fecha_afiliacionAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fecha_afiliacionAfiliadoP.Value = fecha_afiliacionAfiliado;

            OracleParameter estadoAfiliadoP = new OracleParameter("estadoAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            estadoAfiliadoP.Value = estadoAfiliado;

            OracleParameter categoriaAfiliadoP = new OracleParameter("categoriaAfiliado", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            categoriaAfiliadoP.Value = int.Parse(categoriaAfiliado);

            OracleParameter correoAfiliadoP = new OracleParameter("correoAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            correoAfiliadoP.Value = correoAfiliado;

            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, passwordAfiliadoP , nombreAfiliadoP, apellidosAfiliadoP, edadAfiliadoP, cedulaAfiliadoP, fecha_afiliacionAfiliadoP , estadoAfiliadoP, categoriaAfiliadoP, correoAfiliadoP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }

        public void editarAfiliado(string idAfiliado, string passwordAfiliado, string nombreAfiliado, string apellidosAfiliado, string edadAfiliado, string cedulaAfiliado, string fecha_afiliacionAfiliado, string estadoAfiliado, string categoriaAfiliado, string correoAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.editarAfiliado";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter passwordAfiliadoP = new OracleParameter("passwordAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            passwordAfiliadoP.Value = passwordAfiliado;

            OracleParameter nombreAfiliadoP = new OracleParameter("nombreAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreAfiliadoP.Value = nombreAfiliado;

            OracleParameter apellidosAfiliadoP = new OracleParameter("apellidosAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            apellidosAfiliadoP.Value = apellidosAfiliado;

            OracleParameter edadAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            edadAfiliadoP.Value = int.Parse(edadAfiliado);

            OracleParameter cedulaAfiliadoP = new OracleParameter("cedulaAfiliadoP", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            cedulaAfiliadoP.Value = cedulaAfiliado;

            OracleParameter fecha_afiliacionAfiliadoP = new OracleParameter("fecha_afiliacionAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fecha_afiliacionAfiliadoP.Value = fecha_afiliacionAfiliado;

            OracleParameter estadoAfiliadoP = new OracleParameter("estadoAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            estadoAfiliadoP.Value = estadoAfiliado;

            OracleParameter categoriaAfiliadoP = new OracleParameter("categoriaAfiliado", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            categoriaAfiliadoP.Value = int.Parse(categoriaAfiliado);

            OracleParameter correoAfiliadoP = new OracleParameter("correoAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            correoAfiliadoP.Value = correoAfiliado;

            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, passwordAfiliadoP, nombreAfiliadoP, apellidosAfiliadoP, edadAfiliadoP, cedulaAfiliadoP, fecha_afiliacionAfiliadoP, estadoAfiliadoP, categoriaAfiliadoP, correoAfiliadoP });
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            base.cerrarConexion();
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
                        fecha_afiliacion = lectorDatos.GetOracleDate(6).ToString(),
                        estado = lectorDatos.GetString(7),
                        categoria = lectorDatos.GetInt32(8),
                        correo = lectorDatos.GetString(9)
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

        public List<Afiliado> buscarAfiliado(string nombreAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.buscarAfiliado";

            OracleParameter nombreAfiliadoP = new OracleParameter("nombreAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreAfiliadoP.Value = nombreAfiliado;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { nombreAfiliadoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Afiliado> afiliadosConsulta = new List<Afiliado>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    afiliadosConsulta.Add(new Afiliado()
                    {
                        id = lectorDatos.GetString(0),
                        password = lectorDatos.GetString(1),
                        nombre = lectorDatos.GetString(2),
                        apellidos = lectorDatos.GetString(3),
                        edad = lectorDatos.GetInt32(4),
                        cedula = lectorDatos.GetString(5),
                        fecha_afiliacion = lectorDatos.GetOracleDate(6).ToString(),
                        estado = lectorDatos.GetString(7),
                        categoria = lectorDatos.GetInt32(8),
                        correo = lectorDatos.GetString(9)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return afiliadosConsulta;
        }

        public List<Afiliado> listarAfiliados()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.listarAfiliados";

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.Add(cursor_datos);

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Afiliado> afiliadosConsulta = new List<Afiliado>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    afiliadosConsulta.Add(new Afiliado()
                    {
                        id = lectorDatos.GetString(0),
                        password = lectorDatos.GetString(1),
                        nombre = lectorDatos.GetString(2),
                        apellidos = lectorDatos.GetString(3),
                        edad = lectorDatos.GetInt32(4),
                        cedula = lectorDatos.GetString(5),
                        fecha_afiliacion = lectorDatos.GetOracleDate(6).ToString(),
                        estado = lectorDatos.GetString(7),
                        categoria = lectorDatos.GetInt32(8),
                        correo = lectorDatos.GetString(9)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return afiliadosConsulta;
        }

        public void crearSancion(string idAfiliado, string montoSancion, string descripcionSancion)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.crearSancion";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter montoSancionP = new OracleParameter("montoSancion", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            montoSancionP.Value = int.Parse(montoSancion);

            OracleParameter descripcionSancionP = new OracleParameter("descripcionSancion", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            descripcionSancionP.Value = descripcionSancion;

            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, montoSancionP, descripcionSancionP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public void pagarSancion(string idSancion)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.pagarSancion";

            OracleParameter idSancionP = new OracleParameter("idSancion", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            idSancionP.Value = int.Parse(idSancion);

            cmd.Parameters.Add(idSancionP);

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }

        public List<Sancion> sancionesAfiliado(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.sancionesAfiliado";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Sancion> sancionesConsulta = new List<Sancion>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    sancionesConsulta.Add(new Sancion()
                    {
                        id = lectorDatos.GetInt16(0),
                        afiliado = lectorDatos.GetString(1),
                        fecha = lectorDatos.GetOracleDate(2).ToString(),
                        monto = lectorDatos.GetInt16(3),
                        estado = lectorDatos.GetString(4),
                        descripcion = lectorDatos.GetString(5)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return sancionesConsulta;
        }

        public List<Sancion> sancionesPendientesAfiliado(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.sancionesPendientesAfiliado";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Sancion> sancionesConsulta = new List<Sancion>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    sancionesConsulta.Add(new Sancion()
                    {
                        id = lectorDatos.GetInt16(0),
                        afiliado = lectorDatos.GetString(1),
                        fecha = lectorDatos.GetOracleDate(2).ToString(),
                        monto = lectorDatos.GetInt16(3),
                        estado = lectorDatos.GetString(4),
                        descripcion = lectorDatos.GetString(5)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return sancionesConsulta;


        }

        public List<Sancion> sancionesPagasAfiliado(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.sancionesPagasAfiliado";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Sancion> sancionesConsulta = new List<Sancion>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    sancionesConsulta.Add(new Sancion()
                    {
                        id = lectorDatos.GetInt16(0),
                        afiliado = lectorDatos.GetString(1),
                        fecha = lectorDatos.GetOracleDate(2).ToString(),
                        monto = lectorDatos.GetInt16(3),
                        estado = lectorDatos.GetString(4),
                        descripcion = lectorDatos.GetString(5)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return sancionesConsulta;
        }

        public void crearLlamadoAtencion(string idAfiliado, string descripcionLlamado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.crearLlamadoAtencion";

            OracleParameter idAfiliadoP = new OracleParameter("idafiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter descripcionLlamadoP = new OracleParameter("descripcionLlamado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            descripcionLlamadoP.Value = descripcionLlamado;

            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, descripcionLlamadoP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public List<LlamadoAtencion> mostrarLlamadosAtencion(string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "afiliados_paquete.mostrarLlamadosAtencion";

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idAfiliadoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();

            List<LlamadoAtencion> llamadosConsulta = new List<LlamadoAtencion>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    llamadosConsulta.Add(new LlamadoAtencion()
                    {
                        id = lectorDatos.GetInt16(0),
                        afiliado = lectorDatos.GetString(1),
                        fecha = lectorDatos.GetOracleDate(2).ToString(),
                        descripcion = lectorDatos.GetString(3)
                    });
                }
            }

            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return llamadosConsulta;
        }

    }
}