using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Enums
{
    public enum EnumResponseStatus : int
    {
        [Display(Name = "Done")]
        OK = 200,

        [Display(Name = "Error ...")]
        Error = 500,

        [Display(Name = "Bad Request")]
        BadRequest = 400,

        [Display(Name = "Not Found")]
        NotFound = 404

    }
}
