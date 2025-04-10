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

        public DepartmentController(IDepartmentServices _departmentServices , ILogger<DepartmentController> _logger , IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            logger = _logger;
            this.environment = environment;
        }
        #region Index

        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion

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

                if (Result >  0)
                return RedirectToAction(nameof(Index));
                else
                {
                    Massage = "Department Is Not Created";
                    ModelState.AddModelError(string.Empty, Massage);
                    return View(departmentDto);
                }

            }catch (Exception ex)
            {
                logger.LogError(ex , ex.Message);

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
         
        
    }
}
