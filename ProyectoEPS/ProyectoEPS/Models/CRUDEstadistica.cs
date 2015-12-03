using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace ProyectoEPS.Models
{
    public class CRUDEstadistica : Conexion
    {

        public CRUDEstadistica () : base()
        {

        }

        public int afiliadosMes(string mes)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "estadisticas_paquete.afiliadosMes";

            OracleParameter mesP = new OracleParameter("mes", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            mesP.Value = mes;

            OracleParameter cantidadAfiliados = new OracleParameter("cantidadAfiliados", OracleDbType.Int32, System.Data.ParameterDirection.Output);
            cantidadAfiliados.Size = 1024;

            cmd.Parameters.AddRange(new OracleParameter[] { mesP, cantidadAfiliados });

            cmd.ExecuteNonQuery();

            int cantidad = int.Parse(cmd.Parameters["cantidadAfiliados"].Value.ToString());
            cmd.Dispose();
            base.cerrarConexion();
            return cantidad;
        }

        public List<Multa> listarMultas()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "estadisticas_paquete.listarMultas";

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.Add(cursor_datos);

            OracleDataReader lectorDatos = cmd.ExecuteReader();

            List<Multa> multasConsulta = new List<Multa>();

            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    multasConsulta.Add(new Multa() {
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

        
        public List<Dictionary<string, object>> afiliadosPorProfesional(string mes)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "estadisticas_paquete.afiliadosPorProfesional";

            OracleParameter mesP = new OracleParameter("mes", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            mesP.Value = mes;

            OracleParameter cursor_datos = new OracleParameter("cusor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { mesP, cursor_datos });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Dictionary<string, object>> datos = new List<Dictionary<string, object>>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    Dictionary<string, object> valores = new Dictionary<string, object>();
                    valores.Add("profesionalId", lectorDatos.GetString(0));
                    valores.Add("profesionalNombre", lectorDatos.GetString(1));
                    valores.Add("profesionalApellidos", lectorDatos.GetString(2));
                    valores.Add("profesionalCedula", lectorDatos.GetString(3));
                    valores.Add("cantidadPacientes", lectorDatos.GetString(4));
                    datos.Add(valores);

                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return datos;
        }

        public List<Afiliado> afiliadosAtendidosPorMedico(string idMedico)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "estadisticas_paquete.afiliadosAtentidosPorMedidco";

            OracleParameter idMedicoP = new OracleParameter("idMedico", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idMedicoP.Value = idMedico;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idMedicoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Afiliado> afiliadosConsulta = new List<Afiliado>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    afiliadosConsulta.Add(new Afiliado() {
                        id = lectorDatos.GetString(0),
                        password = lectorDatos.GetString(1),
                        nombre = lectorDatos.GetString(2),
                        apellidos = lectorDatos.GetString(3),
                        edad = lectorDatos.GetInt32(4),
                        cedula = lectorDatos.GetString(5),
                        fecha_afiliacion = lectorDatos.GetString(6),
                        estado = lectorDatos.GetString(7),
                        categoria = lectorDatos.GetInt32(9)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return afiliadosConsulta;
        }

    }
}