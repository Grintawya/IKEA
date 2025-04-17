using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.Departments
{
    public class UpdatedDepartmentDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="The Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Code Is Required")]
        public string Code { get; set; }
        public string? Description { get; set; }
        [Display(Name ="Date of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
