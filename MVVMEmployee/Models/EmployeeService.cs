using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
namespace MVVMEmployee.Models
{
    public class EmployeeService
    {
        SqlConnection ObjSqlConnection;
        SqlCommand ObjSqlCommand;
        public EmployeeService()
        {
            ObjSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnEmployee"].ConnectionString);
            ObjSqlCommand = new SqlCommand();
            ObjSqlCommand.Connection = ObjSqlConnection;
            ObjSqlCommand.CommandType = CommandType.StoredProcedure;
        }

        public List<Employee> GetAll()
        {
            List<Employee> ObjEmployeeList = new List<Employee>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectAllEmployees";

                ObjSqlConnection.Open();
                var ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows)
                {
                    Employee ObjEmployee = null;
                    while (ObjSqlDataReader.Read())
                    {
                        ObjEmployee = new Employee();
                        ObjEmployee.Id = ObjSqlDataReader.GetInt32(0);
                        ObjEmployee.Name = ObjSqlDataReader.GetString(1);
                        ObjEmployee.Age = ObjSqlDataReader.GetInt32(2);

                        ObjEmployeeList.Add(ObjEmployee);
                    }
                }
                ObjSqlDataReader.Close();
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return ObjEmployeeList;
        }

        public bool Add(Employee ObjNewEmployee)
        {
            bool IsAdded = false;
            if (ObjNewEmployee.Age < 20 || ObjNewEmployee.Age > 40)
                throw new ArgumentException("Invalid limit of age");
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_AddNewEmployee";
                ObjSqlCommand.Parameters.AddWithValue("@Id", ObjNewEmployee.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", ObjNewEmployee.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Age", ObjNewEmployee.Age);

                ObjSqlConnection.Open();

                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsAdded = NoOfRowsAffected > 0;
                
                
            }
            catch(SqlException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return IsAdded;
        }

        public bool Update(Employee ObjEmployeeToUpdate)
        {
            bool IsUpdated = false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_UpdateEmployee";
                ObjSqlCommand.Parameters.AddWithValue("@Id", ObjEmployeeToUpdate.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", ObjEmployeeToUpdate.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Age", ObjEmployeeToUpdate.Age);

                ObjSqlConnection.Open();

                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsUpdated = NoOfRowsAffected > 0;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted = false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_DeleteEmployee";
                ObjSqlCommand.Parameters.AddWithValue("@Id", id);

                ObjSqlConnection.Open();

                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsDeleted = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return IsDeleted;
        }

        public Employee Search(int id)
        {
            Employee ObjEmployee = null;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SearchEmployeeById";
                ObjSqlCommand.Parameters.AddWithValue("@Id", id);

                ObjSqlConnection.Open();

                var ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows)
                {
                    ObjSqlDataReader.Read();
                    ObjEmployee = new Employee();
                    ObjEmployee.Id = ObjSqlDataReader.GetInt32(0);
                    ObjEmployee.Name = ObjSqlDataReader.GetString(1);
                    ObjEmployee.Age = ObjSqlDataReader.GetInt32(2);
                }
                ObjSqlDataReader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return ObjEmployee;
        }
    }
}
