using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;


namespace ProyectoEPS.Models
{
    public class Conexion
    {
        public OracleConnection conexion { get; set; }

        public Conexion()
        {
            string oradb = "Data Source=MyDB;User Id=EPS;Password=1234;";
            conexion = new OracleConnection(oradb);
        }

        public void abrirConexion()
        {
            conexion.Open();
        }

        public void cerrarConexion()
        {
            conexion.Close();
        }
    }
}

