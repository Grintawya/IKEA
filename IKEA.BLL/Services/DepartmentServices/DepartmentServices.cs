﻿using IKEA.DAL.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    internal class DepartmentServices : IDepartmentServices
    {
        private IDepartmentRepository Repository;

        public DepartmentServices(IDepartmentRepository _repository)
        {
            Repository = _repository;
        }
    }
}
