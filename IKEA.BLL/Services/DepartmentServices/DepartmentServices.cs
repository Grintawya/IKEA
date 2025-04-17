using IKEA.BLL.Dto_s.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private IDepartmentRepository Repository;

        public DepartmentServices(IDepartmentRepository _repository)
        {
            Repository = _repository;

        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments = Repository.GetAll().Select(dept => new DepartmentDto()
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate,
            }).ToList();
            return Departments;

            //List<DepartmentDto> departmentDtos = new List<DepartmentDto>();

            //foreach(var dept in Departments)
            //{
            //    DepartmentDto departmentDto = new DepartmentDto()
            //    {
            //        Id = dept.Id,
            //        Name = dept.Name,
            //        Code = dept.Code,
            //        CreationDate = dept.CreationDate,
            //    };
            //    departmentDtos.Add(departmentDto);
            //}
            //return departmentDtos;
        }
        public DepartmnetDetailsDto GetDepartmentById(int id)
        {
            var Department = Repository.GetById(id);
            if (Department is not null) 
                return new DepartmnetDetailsDto()
                {
                    Id = Department.Id,
                    Name = Department.Name,
                    Code= Department.Code,
                    Description = Department.Description,
                    CreationDate = Department.CreationDate,
                    IsDeleted = Department.IsDeleted,
                    LastModifiedBy = Department.LastModifiedBy,
                    LastModifiedOn = Department.LastModifiedOn,
                    CreatedBy = Department.CreatedBy,
                    CreatedOn = Department.CreatedOn,
                };
            return null;
            
        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,    
                CreationDate= departmentDto.CreationDate,
                CreatedBy = 1,
                CreatedOn= DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            return Repository.Add(CreatedDepartment);
        }
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var UpdatedDepartment = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy= 1,
                LastModifiedOn= DateTime.Now,
            };
            return Repository.Update(UpdatedDepartment);
        }

        public bool DeleteDepartment(int id)
        {

            var department = Repository.GetById(id);
            if (department is not null)
                return Repository.Delete(department) > 0;
            else
                return false;    
        }
 
    }
}
