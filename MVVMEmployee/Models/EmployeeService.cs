﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMEmployee.Models
{
    public class EmployeeService
    {
        private static List<Employee> ObjEmployeesList;
        public EmployeeService()
        {
            ObjEmployeesList = new List<Employee>()
            {
                new Employee{ Id=111, Name="Herve", Age=25 }
            };
        }

        public List<Employee> GetAll()
        {
            return ObjEmployeesList;
        }

        public bool Add(Employee ObjNewEmployee)
        {
            ObjEmployeesList.Add(ObjNewEmployee);
            return true;
        }

        public bool Update(Employee ObjEmployeeToUpdate)
        {
            bool IsUpdated = false;

            for(int index = 0; index < ObjEmployeesList.Count; index++)
            {
                if(ObjEmployeesList[index].Id == ObjEmployeeToUpdate.Id)
                {
                    ObjEmployeesList[index].Name = ObjEmployeeToUpdate.Name;
                    ObjEmployeesList[index].Age = ObjEmployeeToUpdate.Age;
                    IsUpdated = true;
                    break;
                }
            }

            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted = false;

            for(int index=0; index<=ObjEmployeesList.Count; index++)
            {
                if (ObjEmployeesList[index].Id == id)
                {
                    ObjEmployeesList.Remove(ObjEmployeesList[index]);
                    IsDeleted = true;
                    break;
                }
            }
            return IsDeleted;
        }

        public Employee Search(int id)
        {
            return ObjEmployeesList.FirstOrDefault(e => e.Id == id);
        }
    }
}
