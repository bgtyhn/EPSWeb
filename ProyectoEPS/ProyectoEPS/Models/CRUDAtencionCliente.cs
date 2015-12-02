using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace ProyectoEPS.Models
{
    public class CRUDAtencionCliente : Conexion
    {
        public CRUDAtencionCliente() : base()
        {

        }

        public void agregarAtencionCliente()
        {
            //cuando se sepa como ingresar una imagen
        }

        public void editarAtencionCliente()
        {
            //cuando la imagen
        }

        public void eliminarAtencionCliente(string idAC)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "atencion_cliente_paquete.eliminarAtencionCliente";

            OracleParameter idACP = new OracleParameter("idAC", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idACP.Value = idAC;

            cmd.Parameters.Add(idACP);

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }

        public List<AtencionCliente> verAtencionCliente()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "atencion_cliente_paquete.verAtencionCliente";

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.Add(cursor_datos);
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<AtencionCliente> acConsultas = new List<AtencionCliente>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    acConsultas.Add(new AtencionCliente()
                    {
                        id = lectorDatos.GetString(0),
                        password = lectorDatos.GetString(1),
                        nombre = lectorDatos.GetString(2),
                        apellidos = lectorDatos.GetString(3),
                        cedula = lectorDatos.GetString(4)
                    });
                }
            }
            return acConsultas;
        }

        public AtencionCliente verDetalleAtencionCliente(string idAC)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "atencion_cliente_paquete.verAtencionCliente";

            OracleParameter idACP = new OracleParameter("idAC", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idACP.Value = idAC;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idACP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            AtencionCliente acConsulta = new AtencionCliente();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    acConsulta = new AtencionCliente()
                    {
                        id = lectorDatos.GetString(0),
                        password = lectorDatos.GetString(1),
                        nombre = lectorDatos.GetString(2),
                        apellidos = lectorDatos.GetString(3),
                        cedula = lectorDatos.GetString(4)
                    };
                }
            }
            return acConsulta;
        }
    }
}