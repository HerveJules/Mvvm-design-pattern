using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel;
using MVVMEmployee.Models;
using MVVMEmployee.Commands;
namespace MVVMEmployee.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        EmployeeService ObjEmployeeService;
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            loadData();
            CurrentEmployee = new Employee();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            deleteCommand = new RelayCommand(Delete);
            updateCommand = new RelayCommand(Update);
        }
        #region Display Operations
        private ObservableCollection<Employee> employeesList;
        public ObservableCollection<Employee> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value; OnPropertyChanged("EmployeesList"); }
        }
        public void loadData()
        {
            EmployeesList = new ObservableCollection<Employee>(ObjEmployeeService.GetAll());
        }
        #endregion
        private Employee currentEmployee;
        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; OnPropertyChanged("CurrentEmployee"); }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }


        #region Save operations
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            try
            {
                var IsSaved = ObjEmployeeService.Add(CurrentEmployee);
                loadData();
                if (IsSaved)
                    Message = "Employee save successfully!";
                else
                    Message = "Failed to save Employee!";
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region Search Operations

        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        public void Search()
        {
            try
            {
                var ObjEmployee = ObjEmployeeService.Search(CurrentEmployee.Id);
                if(ObjEmployee != null)
                {
                    CurrentEmployee.Name = ObjEmployee.Name;
                    CurrentEmployee.Age = ObjEmployee.Age;
                    Message = "Employee Found!";
                }
                else
                {
                    Message = "Employee Not Found!";
                }
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region Delete Operations

        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        public void Delete()
        {
            try
            {
                var IsDeleted = ObjEmployeeService.Delete(CurrentEmployee.Id);
                if (IsDeleted)
                    Message = "Employee Deleted!";
                else
                    Message = "Failed to Delete1";
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
        }

        #endregion

        #region Update Operations

        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                var IsUpdated = ObjEmployeeService.Update(CurrentEmployee);
                if (IsUpdated)
                {
                    Message = "Employee is updated!";
                    loadData();
                }
                else
                {
                    Message = "Update Failed!";
                }
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
        }

        #endregion
    }
}
