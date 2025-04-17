using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        #region Servises
        public DepartmentController(IDepartmentServices _departmentServices, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            logger = _logger;
            this.environment = environment;
        } 
        #endregion

        #region Index

        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = departmentServices.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();

            return View(department);
        }

        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return View(departmentDto);


            var Massage = string.Empty;

            try
            {
                var Result = departmentServices.CreateDepartment(departmentDto);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Massage = "Department Is Not Created";
                    ModelState.AddModelError(string.Empty, Massage);
                    return View(departmentDto);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                if (environment.IsDevelopment())
                {
                    Massage = ex.Message;
                    ModelState.AddModelError(string.Empty, Massage);
                    return View(departmentDto);
                }
                else
                {
                    Massage = "An Error Effect At The Creation Opreator";
                    ModelState.AddModelError(string.Empty, Massage);
                    return View(departmentDto);
                }
            }

        }

        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null)
                return BadRequest();

            var Department = departmentServices.GetDepartmentById(id.Value);

            if (Department is null)
                return NotFound();

            var MappedDepartment = new UpdatedDepartmentDto()
            {
                Id = Department.Id,
                Name = Department.Name,
                Code = Department.Code,
                CreationDate = Department.CreationDate,
                Description = Department.Description,
            };

            return View(MappedDepartment);

        } 

        [HttpPost]
        public IActionResult Edit(UpdatedDepartmentDto departmentDto)
        {
            if(!ModelState.IsValid)
                return View(departmentDto);

            var Massage = string.Empty;
            try
            {
                var Result = departmentServices.UpdateDepartment(departmentDto);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Massage = "Department is not found";
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                
                Massage = environment.IsDevelopment() ? ex.Message : "An error has been occured during update the Department";
            }

            ModelState.AddModelError(string.Empty, Massage);
            return View(departmentDto);
        }

        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var Department = departmentServices.GetDepartmentById(id.Value);

            if (Department == null)
                return NotFound();

            return View(Department);
        }

        [HttpPost]
        public IActionResult Delete(int DeptId )
        {
            var Massage = string.Empty ;
            try
            {
                var IsDeleted = departmentServices.DeleteDepartment(DeptId);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                Massage = "Department is not Deleted";

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Massage = environment.IsDevelopment() ? ex.Message : "An error has been occured during Delete the Department";
            }

            ModelState.AddModelError(string.Empty , Massage);
            return RedirectToAction(nameof(Delete), new {id = DeptId });


        }

        #endregion

    }
}
