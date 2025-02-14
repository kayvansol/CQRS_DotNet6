using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Enums
{
    public enum EnumResultCode : int
    {
        [Display(Name = "عملیات با موفقیت انجام گردید")]
        Success = 0,

        [Display(Name = "خطایی در انجام عملیات رخ داده")]
        Error = 400

    }
}
