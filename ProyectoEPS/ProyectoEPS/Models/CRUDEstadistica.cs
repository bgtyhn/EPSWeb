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

            int cantidad = int.Parse(cmd.Parameters["cantidadAfiliado"].Value.ToString());
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
            cmd.CommandText = "estadisticas_paquete.afiliadosMes";

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

            return multasConsulta;
        }

        
        public 

    }
}