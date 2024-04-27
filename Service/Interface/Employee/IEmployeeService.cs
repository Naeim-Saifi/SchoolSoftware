using AIS.Data.BaseClass;
using AIS.Model;
using AIS.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.Employee
{
    public interface IEmployeeService
    {
        Task<APIReturnModel> AddUpdateEmployee(EmployeeApiModel employeeModel);

        Task<IEnumerable<EmployeeDetailsModel>> GetemployeeDetails(DefaultInputParameterModel defaultInputParameterModel);
    }
}
