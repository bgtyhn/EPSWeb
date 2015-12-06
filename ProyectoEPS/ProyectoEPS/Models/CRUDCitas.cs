using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace ProyectoEPS.Models
{
    public class CRUDCitas : Conexion
    {
        public CRUDCitas() : base()
        {

        }

        public List<Cita> citasPendientesUsuario(string idUsuario)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasPendientesUsuario";

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
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;

        }

        public List<Cita> citasPendientesArea(string idUsuario, string areaProfesional)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasPendientesArea";

            OracleParameter idUsuarioP = new OracleParameter("idUsuario", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idUsuarioP.Value = idUsuario;

            OracleParameter areaProfesionalP = new OracleParameter("areaProfesional", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            areaProfesionalP.Value = areaProfesional;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idUsuarioP, areaProfesionalP, cursor_datos });

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
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;
        }

        public List<Cita> citasAtendidasCalificadas(string idUsuario)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasAtendidasCalificadas";

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
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;
        }

        public List<Cita> citasAtendidasAfiliado(string idUsuario)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasAtendidasAfiliado";

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
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;
        }

        public void crearCita(string fechaCita, string duracionCita, string tipo_atencionCita, string estadoCita, string calificacionCita, string profesionalCita, string afiliadoCita)
        {
            System.Diagnostics.Debug.WriteLine(fechaCita);
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.nuevaCita";
            OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
            dataAdapter.ReturnProviderSpecificTypes = false;


            OracleParameter fechaCitaP = new OracleParameter("fechaCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaCitaP.Value = fechaCita;
            
            OracleParameter duracionCitaP = new OracleParameter("duracionCita", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            duracionCitaP.Value = int.Parse(duracionCita);
            OracleParameter tipo_atencionCitaP = new OracleParameter("tipo_atencionCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            tipo_atencionCitaP.Value = tipo_atencionCita;
            OracleParameter estadoCitaP = new OracleParameter("estadoCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            duracionCitaP.Value = estadoCita;
            OracleParameter calificacionCitaP = new OracleParameter("calificacionCita", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            calificacionCitaP.Value = int.Parse(calificacionCita);
            OracleParameter profesionalCitaP = new OracleParameter("profesionalCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            profesionalCitaP.Value = profesionalCita;
            OracleParameter afiliadoCitaP = new OracleParameter("afiliadoCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            afiliadoCitaP.Value = afiliadoCita;
            cmd.Parameters.AddRange(new OracleParameter[] { fechaCitaP, duracionCitaP, tipo_atencionCitaP,
                                        estadoCitaP, calificacionCitaP, profesionalCitaP, afiliadoCitaP});
            System.Diagnostics.Debug.WriteLine(cmd.Parameters["fechaCita"].Value.ToString());
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw e;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            cmd.Dispose();
            base.cerrarConexion();
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
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    });
                }
                
                
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
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
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
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
            cmd.CommandText = "citas_paquete.verHorariosCita";
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
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    });
                }

            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
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
                        area = lectorDatos.GetString(7),

                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return profesionalesConsulta;
        }

        public int consultaPago(string idCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.consultarPago";
            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            idCitaP.Value = int.Parse(idCita);
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
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
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
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;

        }

        public void nuevaCita(string fechaCita, string duracionCita, string tipoAtencionCita, string calificacionCita, string profesionalCita, string afiliadoCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.nuevaCita";

            OracleParameter fechaCitaP = new OracleParameter("fechaCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaCitaP.Value = fechaCita;

            OracleParameter duracionCitaP = new OracleParameter("duracionCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            duracionCitaP.Value = int.Parse(duracionCita);

            OracleParameter tipo_atencionCitaP = new OracleParameter("tipo_atencionCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            tipo_atencionCitaP.Value = tipoAtencionCita;

            OracleParameter calificacionCitaP = new OracleParameter("calificacionCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            calificacionCitaP.Value = int.Parse(calificacionCita);

            OracleParameter profesionalCitaP = new OracleParameter("profesionalCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            profesionalCitaP.Value = profesionalCita;

            OracleParameter afiliadoCitaP = new OracleParameter("afiliadoCitaP", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            afiliadoCitaP.Value = afiliadoCita;

            cmd.Parameters.AddRange(new OracleParameter[] { fechaCitaP, duracionCitaP, tipo_atencionCitaP, calificacionCitaP, profesionalCitaP, afiliadoCitaP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public void cambiarEstadoCita(string idCita , string estadoCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.cambiarEstadoCita";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCitaP.Value = int.Parse(idCita);

            OracleParameter estadoCitaP = new OracleParameter("estadoCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            estadoCitaP.Value = estadoCita;

            cmd.Parameters.AddRange(new OracleParameter[] { idCitaP, estadoCitaP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }

        public void generarMulta(string idCita,string idAfiliado)
        {
            System.Diagnostics.Debug.WriteLine(idCita + " - " +idAfiliado);
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.generarMulta";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCitaP.Value = int.Parse(idCita);

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            cmd.Parameters.AddRange(new OracleParameter[] { idCitaP, idAfiliadoP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public List<Medicamento> medicamentosCita(string idCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.medicamentosCita";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCitaP.Value = int.Parse(idCita);

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idCitaP, cursor_datos });

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
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return medicamentosConsulta;
        }

        public List<Diagnostico> diagnosticosCita(string idCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.diagnosticosCita";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCitaP.Value = int.Parse(idCita);

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idCitaP, cursor_datos });
            OracleDataReader lectorDatos = cmd.ExecuteReader();

            List<Diagnostico> listaDiagnostico = new List<Diagnostico>();
            if (lectorDatos.HasRows) {
                while (lectorDatos.Read())
                {
                    listaDiagnostico.Add(new Diagnostico() {
                        id = lectorDatos.GetInt32(0),
                        descripcion = lectorDatos.GetString(1),
                        cita = lectorDatos.GetInt32(2)
                    });
                }
            }

            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return listaDiagnostico;
        }

        public void agregarMedicamentoCita(string idCita,string idMedicamento)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.agregarMedicamentoCita";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCitaP.Value = int.Parse(idCita);

            OracleParameter idMedicamentoP = new OracleParameter("idMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idMedicamentoP.Value = idMedicamento;

            cmd.Parameters.AddRange(new OracleParameter[] { idCitaP, idMedicamentoP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public void agregarDiagnosticoCita(string idCita,  string descripcionDiagnostico)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.agregarDiagnosticoCita";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCitaP.Value = int.Parse(idCita);

            OracleParameter descripcionDiagnosticoP = new OracleParameter("descripcionDiagnostico", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            descripcionDiagnosticoP.Value = descripcionDiagnostico;

            cmd.Parameters.AddRange(new OracleParameter[] { idCitaP, descripcionDiagnosticoP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }


        public List<Cita> citasNoPagas(string idUsuario)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasNoPagas";

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
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8),
                        valor = lectorDatos.GetInt32(9)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;
        }

        public void pagarCita(string idCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.pagarCita";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            idCitaP.Value = int.Parse(idCita);

            cmd.Parameters.Add(idCitaP);

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public void pagarMulta(string idMulta)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.pagarMulta";

            OracleParameter idMultaP = new OracleParameter("idMulta", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            idMultaP.Value = int.Parse(idMulta);

            cmd.Parameters.Add(idMultaP);

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public List<Cita> citasAtendidasCalificadasArea(string idUsuario, string areaCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasAtendidasCalificadasArea";

            OracleParameter idUsuarioP = new OracleParameter("idUsuario", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idUsuarioP.Value = idUsuario;

            OracleParameter areaCitaP = new OracleParameter("areaCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            areaCitaP.Value = areaCita;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idUsuarioP, areaCitaP, cursor_datos });

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
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;
        }

        public List<Cita> citasAtentididasCalificarArea(string idUsuario, string areaCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.citasAtentididasCalificarArea";

            OracleParameter idUsuarioP = new OracleParameter("idUsuario", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idUsuarioP.Value = idUsuario;

            OracleParameter areaCitaP = new OracleParameter("areaCita", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            areaCitaP.Value = areaCita;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idUsuarioP, areaCitaP, cursor_datos });

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
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    });
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citasConsulta;
        }

        public Cita detalleCita(string idCita)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "citas_paquete.detalleCita";

            OracleParameter idCitaP = new OracleParameter("idCita", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCitaP.Value = int.Parse(idCita);

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idCitaP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            Cita citaConsulta = new Cita();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    citaConsulta = new Cita()
                    {
                        id = lectorDatos.GetInt16(0),
                        fecha = lectorDatos.GetString(1),
                        duracion_minutos = lectorDatos.GetInt16(2),
                        tipo_atencion = lectorDatos.GetString(3),
                        estado = lectorDatos.GetString(4),
                        calificacion = lectorDatos.GetInt16(5),
                        profesional = lectorDatos.GetString(6),
                        afiliado = lectorDatos.GetString(7),
                        pago = lectorDatos.GetString(8)
                    };
                }
                lectorDatos.Close();
                cmd.Dispose();
                base.cerrarConexion();

            }
            return citaConsulta;
            
        }


    }
}