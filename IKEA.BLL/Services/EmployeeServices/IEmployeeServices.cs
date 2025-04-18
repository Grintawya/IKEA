using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeeDto> GetAllEmployee();
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
