using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace ProyectoEPS.Models
{
    public class CRUDUsuario : Conexion
    {
        public CRUDUsuario() : base()
        {

        }

        public int inicioSesion(string userCorreo, string userPassword, string tipoUsuario)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pk_usuarios.usuarioEnSistema";
            OracleParameter userCorreoP = new OracleParameter("userCorreo", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            userCorreoP.Value = userCorreo;
            OracleParameter userPasswordP = new OracleParameter("userPassword", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            userPasswordP.Value = userPassword;
            //cmd.Parameters.Add("co", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
            OracleParameter tipoUsuarioP = new OracleParameter("tipoUsuario", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            tipoUsuarioP.Value = tipoUsuario;
            OracleParameter resultado = new OracleParameter("resultado", OracleDbType.Varchar2, System.Data.ParameterDirection.Output);
            resultado.Size = 1000;
            cmd.Parameters.AddRange(new OracleParameter[] { userCorreoP, userPasswordP, tipoUsuarioP, resultado });
            try
            {
                cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine(cmd.Parameters["resultado"].Value);
                return int.Parse(cmd.Parameters["resultado"].Value.ToString());
                
            }
            catch (OracleException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw e;
            }
        }

        public int sesionActiva(string toquenId)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pk_usuarios.usuarioEnSistema";
            OracleParameter toquenIdP = new OracleParameter("userTokenID", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            toquenIdP.Value = toquenId;
            OracleParameter resultado = new OracleParameter("resultado", OracleDbType.Varchar2, System.Data.ParameterDirection.Output);
            resultado.Size = 1024;
            cmd.Parameters.AddRange(new OracleParameter[] { toquenIdP, resultado });
            try
            {
                cmd.ExecuteNonQuery();
                return int.Parse(cmd.Parameters["resultado"].Value.ToString());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw e;
            }
        }

    }
}