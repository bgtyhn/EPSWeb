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
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return medicamentosConsulta;
        }


        public void agregarMedicamento(string idMedicamento, string nombreMedicamento, string descripcionMedicamento, string categorias)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.agregarMedicamento";

            OracleParameter idMedicamentoP = new OracleParameter("idMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idMedicamentoP.Value = idMedicamento;

            OracleParameter nombreMedicamentoP = new OracleParameter("nombreMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreMedicamentoP.Value = nombreMedicamento;

            OracleParameter descripcionMedicamentoP = new OracleParameter("descripcionMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            descripcionMedicamentoP.Value = descripcionMedicamento;

            cmd.Parameters.AddRange(new OracleParameter[] { idMedicamentoP, nombreMedicamentoP, descripcionMedicamentoP });

            cmd.ExecuteNonQuery();

            //cmd.Dispose();



            string[] datosCategorias = categorias.Split(new char[] { ',' });
            System.Diagnostics.Debug.WriteLine(datosCategorias);
            registrarCategoriaMedicamento(datosCategorias, idMedicamento);

            base.cerrarConexion();


        }

        private void registrarCategoriaMedicamento(string[] categorias, string idMedicamento)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.registrarCategoriaMedicamento";
            cmd.Parameters.Clear();
            foreach (string categoria in categorias)
            {
                cmd.Parameters.Clear();
                System.Diagnostics.Debug.WriteLine(cmd.Parameters.Count);
                OracleParameter idCategoriaP = new OracleParameter("idCategoriaN", OracleDbType.Int16, System.Data.ParameterDirection.Input);
                idCategoriaP.Value = int.Parse(categoria);

                OracleParameter idMedicamentoP = new OracleParameter("idMedicamentoN", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
                idMedicamentoP.Value = idMedicamento;

                cmd.Parameters.AddRange(new OracleParameter[] { idCategoriaP, idMedicamentoP });

                cmd.ExecuteNonQuery();
            }
        }

        public void editarMedicamento(string idMedicamento, string nombreMedicamento, string descripcionMedicamento)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.editarMedicamento";

            OracleParameter idMedicamentoP = new OracleParameter("idMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idMedicamentoP.Value = idMedicamento;

            OracleParameter nombreMedicamentoP = new OracleParameter("nombreMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreMedicamentoP.Value = nombreMedicamento;

            OracleParameter descripcionMedicamentoP = new OracleParameter("descripcionMedicamento", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            descripcionMedicamentoP.Value = descripcionMedicamento;

            cmd.Parameters.AddRange(new OracleParameter[] { idMedicamentoP, nombreMedicamentoP, descripcionMedicamentoP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
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
                        descripcion = lectorDatos.GetString(2)
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
                        descripcion = lectorDatos.GetString(2)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return medicamentosConsulta;
        }

        public List<Medicamento> medicamentosCategoria(string nombreCategoria)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.medicamentosCategoria";

            OracleParameter nombreCategoriaP = new OracleParameter("nombreCategoria", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreCategoriaP.Value = nombreCategoria;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { nombreCategoriaP, cursor_datos });

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

        public Categoria detalleCategoria(string idCategoria)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.detalleCategoria";

            OracleParameter idCategoriaP = new OracleParameter("idCategoria", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            idCategoriaP.Value = int.Parse(idCategoria);

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idCategoriaP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            Categoria categoriaConsulta = new Categoria();

            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    categoriaConsulta = new Categoria()
                    {
                        id = lectorDatos.GetInt32(0),
                        nombre = lectorDatos.GetString(1),
                        cuota = lectorDatos.GetInt32(2)
                    };
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return categoriaConsulta;
        }

        public void cambiarCuotaCategoria(string idCategoria, string cuotaNueva)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "medicamentos_paquete.cambiarCuotaCategoria";

            OracleParameter idCategoriaP = new OracleParameter("idCategoria", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCategoriaP.Value = int.Parse(idCategoria);

            OracleParameter cuotaNuevaP = new OracleParameter("cuotaNueva", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            cuotaNuevaP.Value = int.Parse(cuotaNueva);

            cmd.Parameters.AddRange(new OracleParameter[] { idCategoriaP, cuotaNuevaP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();

        }

    }
}