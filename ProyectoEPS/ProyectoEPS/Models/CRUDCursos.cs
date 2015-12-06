using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoEPS.Models;
using Oracle.DataAccess.Client;

namespace ProyectoEPS.Models
{
    public class CRUDCursos : Conexion
    {
        public CRUDCursos() : base()
        {

        }

        public List<Curso> mostrarInfoCursos(string idUsuario)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.mostrarInfoCursos";

            OracleParameter idUsuarioP = new OracleParameter("idUsuario", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idUsuarioP.Value = idUsuario;

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idUsuarioP, cursor_datos });
            List<Curso> cursosConsulta = new List<Curso>();
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    cursosConsulta.Add(new Curso()
                    {
                        id = lectorDatos.GetInt32(0),
                        nombre = lectorDatos.GetString(1),
                        descripcion = lectorDatos.GetString(2),
                        sitio = lectorDatos.GetString(3),
                        maximo_personas = lectorDatos.GetInt32(4),
                        profesional = lectorDatos.GetString(5),
                        inscrito = lectorDatos.GetString(6),
                        lleno = lectorDatos.GetString(7)
                    });
                }
            }

            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return cursosConsulta;
        }

        public List<Curso> verTodosLosCursos()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.verTodosLosCursos";

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.Add(cursor_datos);

            List<Curso> cursosConsulta = new List<Curso>();
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    cursosConsulta.Add(new Curso()
                    {
                        id = lectorDatos.GetInt32(0),
                        nombre = lectorDatos.GetString(1),
                        descripcion = lectorDatos.GetString(2),
                        sitio = lectorDatos.GetString(3),
                        maximo_personas = lectorDatos.GetInt32(4),
                        profesional = lectorDatos.GetString(5),
                        lleno = lectorDatos.GetString(6)
                    });
                }
            }

            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return cursosConsulta;

        }

        public List<Evento> verEventosCurso(string idCurso)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.verEventosCurso";

            OracleParameter idCursoP = new OracleParameter("idCurso", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCursoP.Value = int.Parse(idCurso);

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idCursoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            List<Evento> eventosConsulta = new List<Evento>();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    eventosConsulta.Add(new Evento()
                    {
                        id = lectorDatos.GetInt32(0),
                        fecha = lectorDatos.GetString(1),
                        curso = lectorDatos.GetInt32(2),
                        duracion_minutos = lectorDatos.GetInt32(3)

                    });
                }
            }

            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return eventosConsulta;
        }


        public void crearUnCurso(string nombreCurso, string descripcionCurso, string sitioCurso, string maximosPersonasCurso, string profesionalCurso)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.crearUnCurso";

            OracleParameter nombreCursoP = new OracleParameter("nombreCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreCursoP.Value = nombreCurso;

            OracleParameter descripcionCursoP = new OracleParameter("descripcionCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            descripcionCursoP.Value = descripcionCurso;

            OracleParameter sitioCursoP = new OracleParameter("sitioCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            sitioCursoP.Value = sitioCurso;

            OracleParameter maximosPersonasCursoP = new OracleParameter("maximosPersonasCurso", OracleDbType.Int16, System.Data.ParameterDirection.Input);
            maximosPersonasCursoP.Value = int.Parse(maximosPersonasCurso);

            OracleParameter profesionalCursoP = new OracleParameter("profesionalCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            profesionalCursoP.Value = profesionalCurso;

            cmd.Parameters.AddRange(new OracleParameter[] { nombreCursoP, descripcionCursoP, sitioCursoP, maximosPersonasCursoP, profesionalCursoP });
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }

        public void crearEvento(string fechaCurso, string idCurso, string duracionEvento)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.crearEvento";
            OracleParameter fechaCursoP = new OracleParameter("fechaCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaCursoP.Value = fechaCurso;
            OracleParameter idCursoP = new OracleParameter("idCurso", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCursoP.Value = int.Parse(idCurso);
            OracleParameter duracionEventoP = new OracleParameter("duracionEvento", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            duracionEventoP.Value = int.Parse(duracionEvento);

            cmd.Parameters.AddRange(new OracleParameter[] { fechaCursoP, idCursoP, duracionEventoP });
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();
        }

        public void editarCurso(string idCurso, string nombreCurso, string descripcionCurso, string sitioCurso, string maximosPersonasCurso, string profesionalCurso)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.editarCurso";

            OracleParameter idCursoP = new OracleParameter("idCurso", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCursoP.Value = int.Parse(idCurso);

            OracleParameter nombreCursoP = new OracleParameter("nombreCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            nombreCursoP.Value = nombreCurso;

            OracleParameter descripcionCursoP = new OracleParameter("descripcionCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            descripcionCursoP.Value = descripcionCurso;

            OracleParameter sitioCursoP = new OracleParameter("sitioCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            sitioCursoP.Value = sitioCurso;

            OracleParameter maximosPersonasCursoP = new OracleParameter("maximosPersonasCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            maximosPersonasCursoP.Value = int.Parse(maximosPersonasCurso);

            OracleParameter profesionalCursoP = new OracleParameter("profesionalCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            profesionalCursoP.Value = profesionalCurso;

            cmd.Parameters.AddRange(new OracleParameter[] { idCursoP, nombreCursoP, descripcionCursoP, sitioCursoP, maximosPersonasCursoP, profesionalCursoP });

            cmd.ExecuteNonQuery();


            cmd.Dispose();

            base.cerrarConexion();
        }

        public void editarEvento(string idEvento, string fechaCurso, string idCurso, string duracionEvento)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.editarEvento";

            OracleParameter idEventoP = new OracleParameter("idEvento", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idEventoP.Value = int.Parse(idEvento);
            OracleParameter fechaCursoP = new OracleParameter("fechaCurso", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            fechaCursoP.Value = fechaCurso;
            OracleParameter idCursoP = new OracleParameter("idCurso", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCursoP.Value = int.Parse(idCurso);
            OracleParameter duracionEventoP = new OracleParameter("duracionEvento", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            duracionEventoP.Value = int.Parse(duracionEvento);

            cmd.Parameters.AddRange(new OracleParameter[] { idEventoP, fechaCursoP, idCursoP, duracionEventoP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

        public void inscribirPersonaCurso(string idCurso, string idAfiliado)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.inscribirPersonaCurso";

            OracleParameter idCursoP = new OracleParameter("idCurso", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCursoP.Value = int.Parse(idCurso);

            OracleParameter idAfiliadoP = new OracleParameter("idAfiliado", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
            idAfiliadoP.Value = idAfiliado;

            cmd.Parameters.AddRange(new OracleParameter[] { idCursoP, idAfiliadoP });

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            base.cerrarConexion();


        }

        public void eliminarCurso(string idCurso)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.eliminarCurso";

            OracleParameter idCursoP = new OracleParameter("idCurso", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCursoP.Value = int.Parse(idCurso);

            cmd.Parameters.Add(idCursoP);

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();

        }

        public Curso detalleCurso(string idCurso)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.detalleCurso";
            OracleParameter idCursoP = new OracleParameter("idCursp", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idCursoP.Value = int.Parse(idCurso);
            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.Parameters.AddRange(new OracleParameter[] { idCursoP, cursor_datos });

            Curso cursoConsulta = new Curso();
            OracleDataReader lectorDatos = cmd.ExecuteReader();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    cursoConsulta = new Curso()
                    {
                        id = lectorDatos.GetInt32(0),
                        nombre = lectorDatos.GetString(1),
                        descripcion = lectorDatos.GetString(2),
                        sitio = lectorDatos.GetString(3),
                        maximo_personas = lectorDatos.GetInt32(4),
                        profesional = lectorDatos.GetString(5),
                        lleno = lectorDatos.GetString(6)
                    };
                }
            }

            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return cursoConsulta;
        }

        public Evento detalleEvento(string idEvento)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.detalleEvento";

            OracleParameter idEventoP = new OracleParameter("idEvento", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idEventoP.Value = int.Parse(idEvento);

            OracleParameter cursor_datos = new OracleParameter("cursor_datos", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            cmd.Parameters.AddRange(new OracleParameter[] { idEventoP, cursor_datos });

            OracleDataReader lectorDatos = cmd.ExecuteReader();
            Evento eventoConsulta = new Evento();
            if (lectorDatos.HasRows)
            {
                while (lectorDatos.Read())
                {
                    eventoConsulta = new Evento()
                    {
                        id = lectorDatos.GetInt32(0),
                        fecha = lectorDatos.GetString(1),
                        curso = lectorDatos.GetInt32(2),
                        duracion_minutos = lectorDatos.GetInt32(3)

                    };
                }
            }

            lectorDatos.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return eventoConsulta;

        }

        public void eliminarEvento(string idEvento)
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "cursos_paquete.eliminarEvento";

            OracleParameter idEventoP = new OracleParameter("idEvento", OracleDbType.Int32, System.Data.ParameterDirection.Input);
            idEventoP.Value = int.Parse(idEvento);

            cmd.Parameters.Add(idEventoP);

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            base.cerrarConexion();
        }

    }
}