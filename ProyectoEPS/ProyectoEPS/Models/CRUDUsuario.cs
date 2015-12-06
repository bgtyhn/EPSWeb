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

        public SesionUsuario inicioSesion(string userCorreo, string userPassword, string tipoUsuario)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pk_usuarios.session_login";
            OracleParameter userCorreoP = new OracleParameter("correoLogin", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            userCorreoP.Value = userCorreo;
            OracleParameter userPasswordP = new OracleParameter("passwordLogin", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            userPasswordP.Value = userPassword;
            //cmd.Parameters.Add("co", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
            OracleParameter tipoUsuarioP = new OracleParameter("rol", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            tipoUsuarioP.Value = tipoUsuario;
            OracleParameter resultado = new OracleParameter("resultado", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { userCorreoP, userPasswordP, tipoUsuarioP, resultado });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            if (lectorDatos.HasRows)
            {
                lectorDatos.Read();
                return new SesionUsuario() { id = lectorDatos.GetString(0), operacionExitosa = true, rol = tipoUsuario };
            }
            else
                return new SesionUsuario() { id = string.Empty, operacionExitosa = false, rol = tipoUsuario };
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
                int respuesta = int.Parse(cmd.Parameters["resultado"].Value.ToString());
                cmd.Dispose();
                base.cerrarConexion();
                return respuesta;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw e;
            }
        }



    }
}