using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Dto.Autho
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is Required")]

        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Number is Required")]
        [Phone]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "password is Required")]

        public string Password { get; set; }
    }
}
