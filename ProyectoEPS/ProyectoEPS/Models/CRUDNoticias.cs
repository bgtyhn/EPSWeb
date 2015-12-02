﻿using System;
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
                        fecha_publicacion = lectorDatos.GetString(1),
                        fecha_expiracion = lectorDatos.GetString(2),
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
            cmd.Parameters.Add(new OracleParameter[] { tituloNoticiaP, contenidoNoticiaP, encabezadoNoticiaP });
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Noticia> noticiasConsulta = new List<Noticia>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    noticiasConsulta.Add(new Noticia()
                    {
                        id = lectorDatos.GetInt32(0),
                        fecha_publicacion = lectorDatos.GetString(1),
                        fecha_expiracion = lectorDatos.GetString(2),
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

        public void modificarNoticia()
        {
            //queda pendiente hasta que se sepa como ingresar imagen
        }

        public void agregarNoticia()
        {
            //queda pendiente hasta que se sepa como ingresar imagen
        }

        public Noticia detalleNoticia(int idNoticia)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "noticias_paquete.detalleNoticia";
            OracleParameter idNoticiaP = new OracleParameter("idNoticia", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idNoticiaP.Value = idNoticia;
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
                        fecha_publicacion = lectorDatos.GetString(1),
                        fecha_expiracion = lectorDatos.GetString(2),
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

        public void eliminarNoticia(int idNoticia)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "noticias_paquete.eliminarNoticia";
            OracleParameter idNoticiaP = new OracleParameter("idNoticia", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idNoticiaP.Value = idNoticia;
            cmd.Parameters.Add(idNoticiaP);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            base.cerrarConexion();
        }


    }
}