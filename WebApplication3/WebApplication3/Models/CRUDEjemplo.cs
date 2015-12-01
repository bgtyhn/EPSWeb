using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace WebApplication3.Models
{
    public class CRUDEjemplo : Conexion
    {
       public CRUDEjemplo() : base()
        {
            
        }

        public List<Employee> datos()
        {
            base.abrirConexion();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "myProcedure";
            cmd.Parameters.Add("co", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Employee> employees = new List<Employee>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string first_name = dr.GetString(1);
                    string last_name = dr.GetString(2);
                    employees.Add(new Employee(id, first_name, last_name));
                    Console.WriteLine(dr.GetString(1));
                }
            }
            else
            {
                Console.WriteLine("No data found");
            }
            dr.Close();
            cmd.Dispose();
            base.cerrarConexion();
            return employees;
        }
    }
}