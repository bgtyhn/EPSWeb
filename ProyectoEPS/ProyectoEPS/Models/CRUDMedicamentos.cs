using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace ProyectoEPS.Models
{
    public class CRUDMedicamentos:Conexion
    {
        public CRUDMedicamentos() : base()
        {

        }

        public List<Medicamento> listarMedicamentos()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.listarMedicamentos";
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add(cursor_datos);
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Medicamento> medicamentosConsulta = new List<Medicamento>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    medicamentosConsulta.Add(new Medicamento{
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


        public void agregarMedicamento()
        {
            //hasta que se sepa como subir una imagen
        }

        public void editarMedicamento()
        {
            //hasta que se sepa como subir la imagen
        }

        public void eliminarMedicamento(string idMedicamento)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.eliminarMedicamento";
            OracleParameter idMedicamentoP = new OracleParameter("idMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idMedicamentoP.Value = idMedicamento;
            cmd.Parameters.Add(idMedicamentoP);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            base.cerrarConexion();
        }

        public Medicamento deltalleMedicamento(string idMedicamento)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.detalleMedicamento";
            OracleParameter idMedicamentoP = new OracleParameter("idMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idMedicamentoP.Value = idMedicamento;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idMedicamentoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            Medicamento medicamentoConsulta = new Medicamento();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    medicamentoConsulta = new Medicamento
                    {
                        id = lectorDatos.GetString(0),
                        nombre = lectorDatos.GetString(1),
                        descripcion = lectorDatos.GetString(2),
                        categoria = lectorDatos.GetInt32(4)
                    };
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return medicamentoConsulta;
        }

        public List<Medicamento> buscarMedicamento(string nombreMedicamento)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.buscarMedicamento";

            OracleParameter nombreMedicamentoP = new OracleParameter("nombreMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreMedicamentoP.Value = nombreMedicamento;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { nombreMedicamentoP, cursor_datos });

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
    }
}