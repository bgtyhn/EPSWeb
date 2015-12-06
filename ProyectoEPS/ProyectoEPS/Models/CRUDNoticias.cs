using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace ProyectoEPS.Models
{
    public class CRUDNoticias : Conexion
    {
        public CRUDNoticias() : base()
        {

        }

        public List<Noticia> mostrarNoticiasTodas()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "noticias_paquete.mostrarNoticiasTodas";
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add(cursor_datos);
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Noticia> noticiasConsulta = new List<Noticia>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    noticiasConsulta.Add(new Noticia()
                    {
                        id = lectorDatos.GetInt32(0),
                        fecha_publicacion = lectorDatos.GetOracleDate(1).ToString(),
                        fecha_expiracion = lectorDatos.GetOracleDate(2).ToString(),
                        contenido = lectorDatos.GetString(4),
                        encabezado = lectorDatos.GetString(5),
                        titulo = lectorDatos.GetString(6)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return noticiasConsulta;
        }

        public List<Noticia> buscarNoticia(string tituloNoticia, string contenidoNoticia, string encabezadoNoticia)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "noticias_paquete.buscarNoticia";
            OracleParameter tituloNoticiaP = new OracleParameter("tituloNoticiaP", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            tituloNoticiaP.Value = tituloNoticia;
            OracleParameter contenidoNoticiaP = new OracleParameter("contenidoNoticia", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            contenidoNoticiaP.Value = contenidoNoticia;
            OracleParameter encabezadoNoticiaP = new OracleParameter("encabezadoNoticia", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            encabezadoNoticiaP.Value = encabezadoNoticia;
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { tituloNoticiaP, contenidoNoticiaP, encabezadoNoticiaP, cursor_datos });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Noticia> noticiasConsulta = new List<Noticia>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    noticiasConsulta.Add(new Noticia()
                    {
                        id = lectorDatos.GetInt32(0),
                        fecha_publicacion = lectorDatos.GetOracleDate(1).ToString(),
                        fecha_expiracion = lectorDatos.GetOracleDate(2).ToString(),
                        contenido = lectorDatos.GetString(4),
                        encabezado = lectorDatos.GetString(5),
                        titulo = lectorDatos.GetString(6)
                    });
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return noticiasConsulta;
        }

        public void modificarNoticia(string idN,string fecha_expiracionN,string contenidoN,string encabezadoN ,string tituloN)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "noticias_paquete.modificarNoticia";

            OracleParameter idP = new OracleParameter("idN", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            idP.Value = int.Parse(idN);

            OracleParameter fechaExpiracionP = new OracleParameter("fecha_expiracionN", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaExpiracionP.Value = fecha_expiracionN;

            OracleParameter contenidoP = new OracleParameter("contenidoN", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            contenidoP.Value = contenidoN;

            OracleParameter encabezadoP = new OracleParameter("encabezadoN", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            encabezadoP.Value = encabezadoN;

            OracleParameter tituloP = new OracleParameter("tituloN", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            tituloP.Value = tituloN;

            cmd.Parameters.AddRange(new OracleParameter[] { idP, fechaExpiracionP, contenidoP, encabezadoP, tituloP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public void agregarNoticia(string fecha_expiracionN, string contenidoN,string encabezadoN,string tituloN)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "noticias_paquete.agregarNoticia";

            OracleParameter fechaExpiracionP = new OracleParameter("fecha_expiracionN", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaExpiracionP.Value = fecha_expiracionN;

            OracleParameter contenidoP = new OracleParameter("contenidoN", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            contenidoP.Value = contenidoN;

            OracleParameter encabezadoP = new OracleParameter("encabezadoN", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            encabezadoP.Value = encabezadoN;

            OracleParameter tituloP = new OracleParameter("tituloN", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            tituloP.Value = tituloN;

            cmd.Parameters.AddRange(new OracleParameter[] { fechaExpiracionP, contenidoP, encabezadoP, tituloP});

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();

        }

        public Noticia detalleNoticia(string idNoticia)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "noticias_paquete.detalleNoticia";
            OracleParameter idNoticiaP = new OracleParameter("idNoticia", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idNoticiaP.Value = int.Parse(idNoticia);
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idNoticiaP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            Noticia noticiaConsulta = new Noticia();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    noticiaConsulta = new Noticia()
                    {
                        id = lectorDatos.GetInt32(0),
                        fecha_publicacion = lectorDatos.GetOracleDate(1).ToString(),
                        fecha_expiracion = lectorDatos.GetOracleDate(2).ToString(),
                        contenido = lectorDatos.GetString(4),
                        encabezado = lectorDatos.GetString(5),
                        titulo = lectorDatos.GetString(6)
                    };
                }
            }
            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return noticiaConsulta;
        }

        public void eliminarNoticia(string idNoticia)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "noticias_paquete.eliminarNoticia";
            OracleParameter idNoticiaP = new OracleParameter("idNoticia", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idNoticiaP.Value = int.Parse(idNoticia);
            cmd.Parameters.Add(idNoticiaP);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            base.cerrarConexion();
        }


    }
}